using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 背包页面控制器
/// </summary>
public class InventoryPanelController : MonoBehaviour
{
    //背包页面的视图层和模型层
    private InventoryPanelModel inventoryPanelModel;
    private InventoryPanelView inventoryPanelView;

    //管理背包格子
    private int slotNum = 24;
    private List<GameObject> slotList = new List<GameObject>();

    void Start()
    {
        inventoryPanelView = gameObject.GetComponent<InventoryPanelView>();
        inventoryPanelModel = gameObject.GetComponent<InventoryPanelModel>();
        CreateAllSlot();
        CreateAllItem();
    }

    /// <summary>
    /// 创建所有背包格子
    /// </summary>
    private void CreateAllSlot()
    {
        for(int i = 0; i < slotNum; i++)
        {
            GameObject tempSlot = GameObject.Instantiate(inventoryPanelView.Prefab_Slot, inventoryPanelView.GridTransform);
            slotList.Add(tempSlot);
        }
    }

    /// <summary>
    /// 生成所有背包物品
    /// </summary>
    private void CreateAllItem()
    {
        //获取物品数据
        List<InventoryItem> inventoryItems = inventoryPanelModel.GetJsonList("InventoryJsonData");
        for(int i = 0; i < inventoryItems.Count; i++)
        {
            GameObject temp = GameObject.Instantiate(inventoryPanelView.Prefab_Item, slotList[i].transform);
            temp.GetComponent<InventoryItemController>().InitInitItem(inventoryItems[i].ItemName, inventoryItems[i].ItemNum);
        }
    }

}
