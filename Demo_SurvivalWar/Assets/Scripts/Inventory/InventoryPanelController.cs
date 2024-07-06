using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 背包页面控制器
/// </summary>
public class InventoryPanelController : MonoBehaviour,IUIPanelShowHide
{

    public static InventoryPanelController Instance;

    //背包页面的视图层和模型层
    private InventoryPanelModel inventoryPanelModel;
    private InventoryPanelView inventoryPanelView;

    //管理背包格子
    private int slotNum = 27;
    private List<GameObject> slotList = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        inventoryPanelView = gameObject.GetComponent<InventoryPanelView>();
        inventoryPanelModel = gameObject.GetComponent<InventoryPanelModel>();
        CreateAllSlot();
        CreateAllItem();
    }

    void OnDisable()
    {
        //不激活的时候保存数据
        inventoryPanelModel.ObjectToJson(slotList, "InventoryJsonData.txt");
    }

    /// <summary>
    /// 创建所有背包格子
    /// </summary>
    private void CreateAllSlot()
    {
        for (int i = 0; i < slotNum; i++)
        {
            GameObject tempSlot = GameObject.Instantiate(inventoryPanelView.Prefab_Slot, inventoryPanelView.GridTransform);
            tempSlot.name = "InventorySlot_" + i;
            slotList.Add(tempSlot);
        }
    }

    /// <summary>
    /// 生成所有背包物品
    /// </summary>
    private void CreateAllItem()
    {
        //向数据层获取物品数据
        List<InventoryItem> inventoryItems = inventoryPanelModel.GetJsonList("InventoryJsonData");

        //初始化有物品的格子
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i].ItemName != "")
            {
                GameObject temp = GameObject.Instantiate(inventoryPanelView.Prefab_Item, slotList[i].transform);
                temp.GetComponent<InventoryItemController>().InitItem(inventoryItems[i].ItemName, inventoryItems[i].ItemNum, inventoryItems[i].ItemId, inventoryItems[i].ItemBar, inventoryItems[i].BarValue);

            }
        }
    }

    /// <summary>
    /// 向背包添加对应名称的物品
    /// </summary>
    /// <param name="name">物品名称.</param>
    public void ForAllSlot(string name)
    {
        for (int i = 0; i < slotList.Count; i++)
        {
            Transform tempTransform = slotList[i].GetComponent<Transform>();
            if (tempTransform.childCount != 0) //说明当前物品曹内有物品.
            {
                InventoryItemController temp = tempTransform.Find("InventoryItem").GetComponent<InventoryItemController>();
                if (temp.GetImageName() == name) //说明是相同的物品.
                {
                    if (temp.Num != 64) //没有达到数量上限.
                    {
                        temp.Num++;     //数量增加
                        break;
                    }
                }
            }
        }
    }

    /// <summary>
    /// 添加物品方法
    /// </summary>
    /// <param name="items">添加的物品对象</param>
    public void AddItems(List<GameObject> items)
    {
        //待存储的物品序列号
        int itemIndex = 0;
        for(int i = 0; i < slotList.Count; i++)
        {
            Transform tempTransform = slotList[i].transform.Find("InventoryItem");
            //如果这个格子为空
            if(tempTransform == null && itemIndex < items.Count)
            {
                items[itemIndex].transform.SetParent(slotList[i].transform);
                //在是否在背包的字段属性里初始化
                items[itemIndex].GetComponent<InventoryItemController>().InInventory = true;
                itemIndex++;
            }
        }
    }

    /// <summary>
    /// 把拖拽进合成图谱的物品传递给合成模块控制方法
    /// </summary>
    /// <param name="item"></param>
    public void SendDargMaterilasItem(GameObject item)
    {
       // Debug.Log("获取到的物品名称为"+ item.name);
        CraftingPanelController.Instance.DargMaterilasItem(item);
    }

    #region 继承显示和隐藏的接口，实现对应的显示隐藏方法
    /// <summary>
    /// 显示UI页面
    /// </summary>
    public void UIPanelShow()
    {
        //移动回屏幕中间
        GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
    }

    /// <summary>
    /// 隐藏UI页面
    /// </summary>
    public void UIPanelHide()
    {
        //移出屏幕外
        GetComponent<RectTransform>().offsetMin = new Vector2(9999, 0);
    }
    #endregion

}
