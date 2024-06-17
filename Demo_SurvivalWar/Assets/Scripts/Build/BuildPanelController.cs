using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 环形工具栏管理类
/// </summary>
public class BuildPanelController : MonoBehaviour {

    /// <summary>
    /// 环形图片 
    /// </summary>
    private Transform BG_Transform;                            
    /// <summary>
    /// 环形每项的预制体
    /// </summary>
    private GameObject item_Prefab;
    /// <summary>
    /// 材料子项预制体
    /// </summary>
    private GameObject material_Prefab;
    /// <summary>
    /// 中心显示物品名称
    /// </summary>
    private Text itemName_Text;                                

    /// <summary>
    /// 环形每项对应的图片集合
    /// </summary>
    private List<Sprite> icons = new List<Sprite>();
    /// <summary>
    /// 环形每项的脚本集合
    /// </summary>
    private List<Item> itemList = new List<Item>();

    /// <summary>
    /// 用于记录滚轮的数组.[需要累加记录].
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

    /// <summary>
    /// 环形UI面板是否显示标志位.
    /// </summary>
    private bool isUIShow = true;                              

    private string[] itemNames = new string[] { "", "杂项", "屋顶", "楼梯", "窗户", "门", "墙壁", "地板", "地基" };

    /// <summary>
    /// 材料图片数组 
    /// </summary>
    private List<Sprite[]> materialIcons = new List<Sprite[]>();

    /// <summary>
    /// 材料UI初始旋转角度.
    /// </summary>
    private int zIndex = 20;                

    /// <summary>
    /// 环形UI项材料子项名称数组的集合
    /// </summary>
    private List<string[]> materialIconNames = new List<string[]>();

    void Start () {
        Init();
        LoadIcons();
        LoadMaterialIcons();
        SetMaterialIconNames();
        CreateItems();
    }


    void Update()
    {
        //Tab键控制环形UI显示和隐藏
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ShowOrHide();
        }

        //鼠标滚轮逻辑.
        if (isUIShow)
        {
            MouseScrollWheel();
        }

    }

    /// <summary>
    /// 基础初始化操作.
    /// </summary>
    private void Init()
    {
        BG_Transform = transform.Find("WheelBG");
        item_Prefab = Resources.Load<GameObject>("Build/Prefab/Item");
        itemName_Text = transform.Find("WheelBG/ItemName").GetComponent<Text>();
        material_Prefab = Resources.Load<GameObject>("Build/Prefab/MaterialBG");
    }

    /// <summary>
    /// 加载九个Icon图标.
    /// </summary>
    private void LoadIcons()
    {
        icons.Add(null);
        icons.Add(Resources.Load<Sprite>("Build/Icon/Question Mark"));
        icons.Add(Resources.Load<Sprite>("Build/Icon/Roof_Category"));
        icons.Add(Resources.Load<Sprite>("Build/Icon/Stairs_Category"));
        icons.Add(Resources.Load<Sprite>("Build/Icon/Window_Category"));
        icons.Add(Resources.Load<Sprite>("Build/Icon/Door_Category"));
        icons.Add(Resources.Load<Sprite>("Build/Icon/Wall_Category"));
        icons.Add(Resources.Load<Sprite>("Build/Icon/Floor_Category"));
        icons.Add(Resources.Load<Sprite>("Build/Icon/Foundation_Category"));
    }

    /// <summary>
    /// 加载具体建造材料的图标Icons.
    /// </summary>
    private void LoadMaterialIcons()
    {
        materialIcons.Add(null);
        materialIcons.Add(new Sprite[] { LoadIcon("Ceiling Light"), LoadIcon("Pillar_Wood"), LoadIcon("Wooden Ladder") });
        materialIcons.Add(new Sprite[] { null, LoadIcon("Roof_Metal"), null });
        materialIcons.Add(new Sprite[] { LoadIcon("Stairs_Wood"), LoadIcon("L Shaped Stairs_Wood"), null });
        materialIcons.Add(new Sprite[] { null, LoadIcon("Window_Wood"), null });
        materialIcons.Add(new Sprite[] { null, LoadIcon("Wooden Door"), null });
        materialIcons.Add(new Sprite[] { LoadIcon("Wall_Wood"), LoadIcon("Doorway_Wood"), LoadIcon("Window Frame_Wood") });
        materialIcons.Add(new Sprite[] { null, LoadIcon("Floor_Wood"), null });
        materialIcons.Add(new Sprite[] { null, LoadIcon("Platform_Wood"), null });
    }

    /// <summary>
    /// 设置材料Icons的名称.
    /// </summary>
    private void SetMaterialIconNames()
    {
        materialIconNames.Add(null);
        materialIconNames.Add(new string[] { "吊灯", "木柱", "梯子" });
        materialIconNames.Add(new string[] { null, "屋顶", null });
        materialIconNames.Add(new string[] { "直梯", "L型梯", null });
        materialIconNames.Add(new string[] { null, "窗户", null });
        materialIconNames.Add(new string[] { null, "门", null });
        materialIconNames.Add(new string[] { "普通墙壁", "门型墙壁", "窗型墙壁" });
        materialIconNames.Add(new string[] { null, "地板", null });
        materialIconNames.Add(new string[] { null, "地基", null });
    }

    /// <summary>
    /// 实例化生成所有的Item.
    /// </summary>
    private void CreateItems()
    {
        for (int i = 0; i < 9; i++)
        {
            GameObject item = GameObject.Instantiate<GameObject>(item_Prefab, BG_Transform);
            itemList.Add(item.GetComponent<Item>());

            if (icons[i] == null)
            {
                item.GetComponent<Item>().Init("Item", Quaternion.Euler(new Vector3(0, 0, i * 40)), false, null, true);
            }
            else
            {
                item.GetComponent<Item>().Init("Item", Quaternion.Euler(new Vector3(0, 0, i * 40)), true, icons[i], false);
                //完成具体分类当中具体材料的生成.
                for (int j = 0; j < materialIcons[i].Length; j++)
                {
                    //每添加一个旋转13度
                    zIndex += 13;

                    if (materialIcons[i][j] != null)
                    {
                        GameObject material = GameObject.Instantiate<GameObject>(material_Prefab, BG_Transform);
                        
                        //子项扇形旋转
                        material.GetComponent<Transform>().rotation = Quaternion.Euler(new Vector3(0, 0, zIndex));

                        //设置对应图片
                        material.GetComponent<Transform>().Find("Icon").GetComponent<Image>().sprite = materialIcons[i][j];

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
            BG_Transform.gameObject.SetActive(false);
            isUIShow = false;
        }
        //如果当前没有显示
        else
        {
            //显示环形UI，开启标志位
            BG_Transform.gameObject.SetActive(true);
            isUIShow = true;
        }
    }

    /// <summary>
    /// 鼠标滚轮操作.
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
    /// 设置Text组件内容.
    /// </summary>
    private void SetTextValue()
    {
        itemName_Text.text = itemNames[index % itemNames.Length];
    }


    /// <summary>
    /// 加载指定的材料icon图标.
    /// </summary>
    private Sprite LoadIcon(string name)
    {
        return Resources.Load<Sprite>("Build/MaterialIcon/" + name);
    }

}
