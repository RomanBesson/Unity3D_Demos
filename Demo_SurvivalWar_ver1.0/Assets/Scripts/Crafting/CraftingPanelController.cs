using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 合成模块控制层，负责逻辑操作
/// </summary>
public class CraftingPanelController : MonoBehaviour
{
    //单例
    public static CraftingPanelController Instance;

    //视图层和模型层的示例获取
    private CraftingPanelView m_CraftingPanelView;
    private CraftingPanelModel m_CraftingPanelModel;

    private CraftingController m_CraftingController; //右面功能区控制脚本

    private int tabsNum = 2; //左侧标签的数量
    private int materialsCount = 0;         //物品合成需要的材料数
    private int dargMaterialsCount = 0;     //合成图谱槽已经存在的材料数
    private int slotsNum = 25; //合成台物品

    private List<GameObject> tabsList; //左侧标签的列表
    private List<GameObject> contentsList; //左侧标签页对应内容列表
    private List<GameObject> materialsList; //管理已经拖拽放入合成台的材料.
    private List<GameObject> slotsList; //合成台图谱槽列表

    private int current = -1; //当前的标签页id

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //初始化
        Init();
        //创建所有标签
        CreateAllTabs();
        CreateAllContents();
        CreateAllSlots();
        ResetTabsAndContents(0);
    }

    /// <summary>
    /// 初始化配置
    /// </summary>
    private void Init()
    {
        m_CraftingPanelModel = gameObject.GetComponent<CraftingPanelModel>();
        m_CraftingPanelView = gameObject.GetComponent<CraftingPanelView>();

        m_CraftingController =  transform.Find("Right").GetComponent<CraftingController>();

        tabsList = new List<GameObject>();
        contentsList = new List<GameObject>();
        slotsList = new List<GameObject>();
        materialsList = new List<GameObject>();
        //用V层的预制体 给合成台合成的物品 预制体 赋值
        m_CraftingController.Prefab_InventoryItem = m_CraftingPanelView.Prefab_InventoryItem;
    }

    /// <summary>
    /// 生成所有标签
    /// </summary>
    public void CreateAllTabs()
    {
        for (int i = 0; i < tabsNum; i++)
        {
            GameObject go = GameObject.Instantiate<GameObject>(m_CraftingPanelView.Prefab_TabsItem, m_CraftingPanelView.Tabs_Transform);
            //获取对应图片
            Sprite temp = m_CraftingPanelView.ByNameGetSprite(m_CraftingPanelModel.GetTabsIconName()[i]);
            //初始化
            go.GetComponent<CraftingTabItemController>().InitItem(i, temp);
            tabsList.Add(go);
        }
    }

    /// <summary>
    /// 生成全部内容区域.
    /// </summary>
    private void CreateAllContents()
    {
        List<List<CraftingContentItem>> tempList = m_CraftingPanelModel.ByNameGetJsonData("CraftingContentsJsonData");

        for (int i = 0; i < tabsNum; i++)
        {
            GameObject go = GameObject.Instantiate<GameObject>(m_CraftingPanelView.Prefab_Content, m_CraftingPanelView.Contents_Transform);
            go.GetComponent<CraftingContentController>().InitContent(i, m_CraftingPanelView.Prefab_ContentItem, tempList[i]);
            contentsList.Add(go);
        }
    }

    /// <summary>
    /// 重置标签页和对应的内容
    /// </summary>
    public void ResetTabsAndContents(int index)
    {
        //减少重复点击引起的消耗
        if (index == current) return;
        //所有的都设置激活
        for (int i = 0; i < tabsList.Count; i++)
        {
            tabsList[i].GetComponent<CraftingTabItemController>().NoramlTab();
            contentsList[i].SetActive(false);
        }
        //选中的变成不激活
        tabsList[index].GetComponent<CraftingTabItemController>().ActiveTab();
        contentsList[index].SetActive(true);
        current = index;
    }

    /// <summary>
    /// 生成所有的合成图谱槽.
    /// </summary>
    private void CreateAllSlots()
    {
        for (int i = 0; i < slotsNum; i++)
        {
            GameObject go = GameObject.Instantiate<GameObject>(m_CraftingPanelView.Prefab_Slot, m_CraftingPanelView.Center_Transform);
            go.name = "Slot" + i;
            slotsList.Add(go);
        }
    }

    /// <summary>
    /// 图谱槽数据填充.
    /// </summary>
    /// <param name="id">内容标签id</param>
    private void CreateSlotContents(int id)
    {
        CraftingMapItem temp = m_CraftingPanelModel.GetItemById(id);
        //如果图谱存在
       if (temp != null)
        {
            //设置右侧功能区图片
            m_CraftingController.InitImage(temp.MapId, temp.MapName);
          
            //重置图谱
            ResetSlotContents();

            //把剩余材料放到背包
            ResetMaterials();

            //设置图谱
            for (int j = 0; j < temp.MapContents.Length; j++)
            {
                //0表示没图片
                if (temp.MapContents[j] != "0")
                {
                    //从预加载的合成图谱图库里调取
                    Sprite sprite = m_CraftingPanelView.ByNameGetMaterialIconSprite(temp.MapContents[j]);
                    //把图谱图片和id传进去初始化
                    slotsList[j].GetComponent<CraftingSlotController>().Init(sprite, int.Parse(temp.MapContents[j]));
                }
            }
            // 初始化图谱需要的物品数量
            materialsCount = temp.MaterialsCount;
        }
    }

    /// <summary>
    /// 重置图谱
    /// </summary>
    private void ResetSlotContents()
    {
        for(int i = 0; i < slotsList.Count; i++)
        {
            slotsList[i].GetComponent<CraftingSlotController>().ResetSprite();
        }
    }

    /// <summary>
    /// 把剩余合成台上的材料放到背包
    /// </summary>
    private void ResetMaterials()
    {
        //存储合成台上的材料
        List<GameObject> materialList = new List<GameObject>();
        for(int i = 0; i < slotsList.Count; i++)
        {
            Transform temp = slotsList[i].transform.Find("InventoryItem");
            //合成台格子上存在物品
            if (temp != null)
            {
                materialList.Add(temp.gameObject);
            }
        }
        //调用背包页面的添加物品方法
        InventoryPanelController.Instance.AddItems(materialList);

    }

    /// <summary>
    /// 对拖入放入合成图谱内的材料进行管理.
    /// </summary>
    public void DargMaterilasItem(GameObject item)
    {
        dargMaterialsCount++;
        materialsList.Add(item);
        //满足合成条件，合成按钮激活
        if(dargMaterialsCount == materialsCount)
        {
            m_CraftingController.ActiveButton();
        }
       // Debug.Log("当前图谱内的物品数量为： " + dargMaterialsCount + "   列表数量为: " + materialsList.Count + "  图谱需要的物品数量为：" + materialsCount);
    }

    /// <summary>
    /// 合成完毕，消耗对应材料
    /// </summary>
    private void CraftingOK()
    {
        for(int i = 0;i< materialsList.Count; i++)
        {
            InventoryItemController iic = materialsList[i].GetComponent<InventoryItemController>();
            if (iic.Num == 1)
            {
                GameObject.Destroy(materialsList[i]);
            }
            else
            {
                iic.Num = iic.Num - 1;
            }
        }
        //剩余物品回归背包
        StartCoroutine("ResetMap");
    }

    /// <summary>
    /// 消耗完毕物品，定时让他们回归背包
    /// </summary>
    /// <returns></returns>
    private IEnumerator ResetMap()
    {
        yield return new WaitForSeconds(2);
        ResetMaterials();
        dargMaterialsCount = 0;
        materialsList.Clear();
    }

}
