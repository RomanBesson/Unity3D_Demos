using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 环形工具栏管理类
/// </summary>
public class BuildPanelController : MonoBehaviour {

    #region 字段和属性
    private BuildPanelView m_BuildPanelView;        //V层引用.

    /// <summary>
    /// 临时建造材料模型.
    /// </summary>
    private GameObject tempBuildModel = null;
    /// <summary>
    /// 实例化生成后的模型.
    /// </summary>
    private GameObject BuildModel = null;                                     

    /// <summary>
    /// 环形每项的脚本集合
    /// </summary>
    private List<Item> itemList = new List<Item>();

    //主菜单相关字段
    /// <summary>
    /// 累加记录当前滚轮的滚动值
    /// </summary>
    private float scrollNum = 90000.0f;
    /// <summary>
    /// 当前环形项索引（需要取余）
    /// </summary>
    private int index = 0;
    /// <summary>
    /// 当前激活环形项
    /// </summary>
    private Item currentItem = null;
    /// <summary>
    /// 即将激活环形项
    /// </summary>
    private Item targetItem = null;


    //二级菜单滚动相关字段.--Material.
    /// <summary>
    /// 二级菜单累加记录当前滚轮的滚动值
    /// </summary>
    private float scrollNum_Material = 0.0f;
    /// <summary>
    /// 二级菜单索引
    /// </summary>
    private int index_Material = 0;
    /// <summary>
    /// 二级菜单当前激活项
    /// </summary>
    private MaterialItem currentMaterial = null;
    /// <summary>
    /// 二级菜单即将激活项
    /// </summary>
    private MaterialItem targetMaterial = null;


    /// <summary>
    /// 环形UI面板是否显示标志位.  true:显示环形UI, false:隐藏环形UI
    /// </summary>
    private bool isUIShow = true;
    /// <summary>
    /// 标识当前滚轮操作的是主菜单,还是二级菜单 [true: 主菜单 | false: 二级菜单]
    /// </summary>
    private bool isItemCtr = true;        

    private string[] itemNames = new string[] { "", "杂项", "屋顶", "楼梯", "窗户", "门", "墙壁", "地板", "地基" };

    /// <summary>
    /// 射线检测的层数.
    /// </summary>
    private int layerNum = 0;              

    //切换到建筑图纸时同时显示UI
    void OnEnable()
    {
        isUIShow = true;
        if (m_BuildPanelView != null)
        {
            m_BuildPanelView.M_BG_Transform.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// 材料UI初始旋转角度.
    /// </summary>
    private int zIndex = 20;
        

    private Ray ray;
    private RaycastHit hit;

    #endregion

    void Start () {
        m_BuildPanelView = gameObject.GetComponent<BuildPanelView>();
        CreateItems();
    }


    void Update()
    {
        MouseRight();
        MouseScroll();
        MouseLeft();
        //如果要实例化的模型不为空
        if (BuildModel != null) SetModelPosition();

    }

    /// <summary>
    /// TAB键逻辑.
    /// </summary>
    private void MouseRight()
    {
        //Tab键控制环形UI显示和隐藏
        if (Input.GetMouseButtonDown(1))
        {
            //如果此时是子菜单
            if (isItemCtr == false)
            {
                //将当前子菜单项取消高亮
                currentMaterial.Normal();

                //设置标志位为主菜单
                isItemCtr = true;
            }
            //此时是主菜单，进行隐藏或者显示
            else
            {
                ShowOrHide();
            }
        }
    }

    /// <summary>
    /// 鼠标滚轮逻辑.
    /// </summary>
    private void MouseScroll()
    {
        //鼠标滚轮选择环形主菜单项逻辑.
        if (isUIShow && isItemCtr == true)
        {
            MouseScrollWheel();
        }

        //鼠标滚轮选择环形子菜单项逻辑.--Material.
        if (isUIShow && isItemCtr == false)
        {
            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                MouseScrollWheelMaterial();
            }
        }
    }

    /// <summary>
    /// 鼠标左键逻辑.
    /// </summary>
    private void MouseLeft()
    {

        //鼠标左键逻辑.
        if (Input.GetMouseButtonDown(0))
        {
            //如果主菜单组件为空，返回
            if (targetItem == null) return;

            //如果主菜单项对应的二级菜单为空
            if (targetItem.materialList.Count == 0)
            {
                SetLeftKeyNull();
                SetUIHide();
                return;
            }

            //如果此时没有选中建造模型，返回主菜单
            if (tempBuildModel == null) isItemCtr = false;

            //如果已经选中建造模型，以及主菜单处于显示状态
            if (tempBuildModel != null && isUIShow)
            {
                //主菜单隐藏，显示标志位设置为隐藏
                SetUIHide();

                //将当前子菜单项取消高亮
                currentMaterial.Normal();
            }

            //如果已经选中模型，但是当前位置不能摆放，停止执行
            if (BuildModel != null && BuildModel.GetComponent<MaterialModelBase>().IsCunPut == false) return;

            //如果已经存在可遥控的建造模型且可以摆放在当前位置
            if (BuildModel != null && BuildModel.GetComponent<MaterialModelBase>().IsCunPut)
            {
                //设置名字
                BuildModel.name = tempBuildModel.name;

                //将摆放完的建筑材料放在 BuildModelEnd (14)层上，让射线可以检测到
                BuildModel.layer = 14;

                //变回原来的颜色
                BuildModel.GetComponent<MaterialModelBase>().Normal();

                //把建筑相关的脚本销毁（该模型不再处于建筑模式）
                GameObject.Destroy(BuildModel.GetComponent<MaterialModelBase>());
            }


            //如果已经选中建造模型
            if (tempBuildModel != null)
            {
                //在前方实例化生成对应模型（将之前的BuildModel放置在射线位置）
                BuildModel = GameObject.Instantiate<GameObject>(tempBuildModel, m_BuildPanelView.M_Player_Transform.position + new Vector3(0, 0, 10), Quaternion.identity, m_BuildPanelView.M_Models_Parent);

                //滚轮操作还原为主菜单
                isItemCtr = true;
            }
        }
    }

    /// <summary>
    /// 实例化生成所有的Item.
    /// </summary>
    private void CreateItems()
    {
        for (int i = 0; i < 9; i++)
        {
            GameObject item = GameObject.Instantiate<GameObject>(m_BuildPanelView.M_Item_Prefab, m_BuildPanelView.M_BG_Transform);
            itemList.Add(item.GetComponent<Item>());

            if (m_BuildPanelView.Icons[i] == null)
            {
                item.GetComponent<Item>().Init("Item", Quaternion.Euler(new Vector3(0, 0, i * 40)), false, null, true);
            }
            else
            {
                item.GetComponent<Item>().Init("Item", Quaternion.Euler(new Vector3(0, 0, i * 40)), true, m_BuildPanelView.Icons[i], false);
                //完成具体分类当中具体材料的生成.
                for (int j = 0; j < m_BuildPanelView.MaterialIcons[i].Length; j++)
                {
                    //每添加一个旋转13度
                    zIndex += 13;

                    //如果对应材料图片不为空
                    if (m_BuildPanelView.MaterialIcons[i][j] != null)
                    {

                        GameObject material = GameObject.Instantiate<GameObject>(m_BuildPanelView.M_Material_Prefab, m_BuildPanelView.M_BG_Transform);
                        
                        //子项扇形旋转
                        material.GetComponent<Transform>().rotation = Quaternion.Euler(new Vector3(0, 0, zIndex));

                        //设置对应图片
                        material.GetComponent<Transform>().Find("Icon").GetComponent<Image>().sprite = m_BuildPanelView.MaterialIcons[i][j];

                        //内部图片不旋转
                        material.GetComponent<Transform>().Find("Icon").GetComponent<Transform>().rotation = Quaternion.Euler(Vector3.zero);

                        //设置环形UI项为父类
                        material.GetComponent<Transform>().SetParent(item.GetComponent<Transform>());

                        //添加到对应环形UI项的列表里
                        item.GetComponent<Item>().MateiralListAdd(material);
                    }
                }
                item.GetComponent<Item>().Hide();
            }
        }
        //设置当前环项
        currentItem = itemList[0];
        SetTextValue();
    }


    /// <summary>
    /// 设置环形UI显示与隐藏.
    /// </summary>
    private void ShowOrHide()
    {
        //如果当前是显示状态
        if (isUIShow)
        {
            //关闭面板，重置标志位
            m_BuildPanelView.M_BG_Transform.gameObject.SetActive(false);
            isUIShow = false;
        }
        //如果当前没有显示
        else
        {
            //显示环形UI，开启标志位
            m_BuildPanelView.M_BG_Transform.gameObject.SetActive(true);
            isUIShow = true;

            //重置
            if (targetMaterial != null) targetMaterial.Normal();
            Reset();
        }
    }

    /// <summary>
    /// 环形UI鼠标滚轮操作.
    /// </summary>
    private void MouseScrollWheel()
    {
        //如果滚轮滚动的话
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            //不断记录累加数值，将累加值的绝对值取整作为索引
            scrollNum += Input.GetAxis("Mouse ScrollWheel") * 5;
            index = Mathf.Abs((int)scrollNum);

            //取余，设置即将激活项
            targetItem = itemList[index % itemList.Count];

            //激活新的环形项
            if (targetItem != currentItem)
            {
                targetItem.Show();
                currentItem.Hide();
                currentItem = targetItem;
                SetTextValue();
            }
        }
    }

    /// <summary>
    /// 二级菜单鼠标滚轮操作.--Material.
    /// </summary>
    private void MouseScrollWheelMaterial()
    {
        //获取鼠标滚轮数值累加取余得到当前二级菜单索引
        scrollNum_Material += Input.GetAxis("Mouse ScrollWheel") * 5;
        index_Material = Mathf.Abs((int)scrollNum_Material);

        //获取二级菜单目标项
        targetItem = itemList[index % itemList.Count];
        targetMaterial = targetItem.materialList[index_Material % targetItem.materialList.Count].GetComponent<MaterialItem>();

        //切换显示状态
        if (targetMaterial != currentMaterial)
        {
            //将当前选中模型储存为临时模型
            tempBuildModel = m_BuildPanelView.MaterialModels[index % itemList.Count][index_Material % targetItem.materialList.Count];
            
            targetMaterial.Height();
            if (currentMaterial != null)
            {
                currentMaterial.Normal();
            }
            currentMaterial = targetMaterial;

            SetTextValueMaterial();
        }
    }

    /// <summary>
    /// 使用射线来确定模型的位置.
    /// </summary>
    private void SetModelPosition()
    {
        //如果是吊灯或者屋顶，忽略已经创建的建筑物体，让他们可以高处摆放
        if (BuildModel.name == "Roof(Clone)" || BuildModel.name == "Ceiling_Light(Clone)")
        {
            layerNum = ~(1 << 13);
        }

        //否则，不忽略
        else
        {
            layerNum = 1 << 0;
        }

        ray = m_BuildPanelView.M_EnvCamera.ScreenPointToRay(Input.mousePosition);

        //忽略建筑模型层
        if (Physics.Raycast(ray, out hit, 15, layerNum))
        {

                //如果不可吸附
                if (BuildModel.GetComponent<MaterialModelBase>().IsAttach == false)
                {
                    //模型跟随鼠标射线移动
                    BuildModel.GetComponent<Transform>().position = hit.point;
                }

                //距离太远，设置为不可吸附
                if (Vector3.Distance(hit.point, BuildModel.GetComponent<Transform>().position) > 3)
                {
                    BuildModel.GetComponent<MaterialModelBase>().IsAttach = false;
                }
        }
    }

    /// <summary>
    /// 鼠标左键清空临时模型和实例化模型
    /// </summary>
    private void SetLeftKeyNull()
    {
        //清空临时模型
        if (tempBuildModel != null) tempBuildModel = null;

        //清空实例化模型
        if (BuildModel != null)
        {
            GameObject.Destroy(BuildModel);
            BuildModel = null;
        }
    }

    /// <summary>
    /// 重置环形UI.
    /// </summary>
    public void Reset()
    {
        DestroyBuildModel();
        if (tempBuildModel != null) tempBuildModel = null;

        //重置子环形
        if (currentItem != null)
        {
            currentItem.Hide();
            currentItem = itemList[0];
            currentItem.Show();
        }

        index = 0;
        scrollNum = -90000.0f;

        scrollNum_Material = 0.0f;
        index_Material = 0;
        currentMaterial = null;
    }

    /// <summary>
    /// 单独设置UI面板隐藏.
    /// </summary>
    private void SetUIHide()
    {
        m_BuildPanelView.M_BG_Transform.gameObject.SetActive(false);
        isUIShow = false;
    }

    /// <summary>
    /// 设置Text组件内容-环形主菜单
    /// </summary>
    private void SetTextValue()
    {
        m_BuildPanelView.M_ItemName_Text.text = itemNames[index % itemNames.Length];
    }


    /// <summary>
    /// 设置Text组件内容.--环形子菜单.
    /// </summary>
    private void SetTextValueMaterial()
    {
        m_BuildPanelView.M_ItemName_Text.text = m_BuildPanelView.MaterialIconNames[index % itemList.Count][index_Material % targetItem.materialList.Count];
    }

   

    /// <summary>
    /// 销毁未固定位置的模型.
    /// </summary>
    public void DestroyBuildModel()
    {
        GameObject.Destroy(BuildModel);
    }
}
