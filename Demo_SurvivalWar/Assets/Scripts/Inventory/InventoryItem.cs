using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 背包物品的数据实体类.
/// </summary>
public class InventoryItem
{

    private int itemId;
    private string itemName;
    private int itemNum;
    private int itemBar;
    private string barValue;

    /// <summary>
    /// 物品id
    /// </summary>
    public int ItemId
    {
        get { return itemId; }
        set { itemId = value; }
    }

    /// <summary>
    /// 该物品名称
    /// </summary>
    public string ItemName
    {
        get { return itemName; }
        set { itemName = value; }
    }

    /// <summary>
    /// 该物品数量
    /// </summary>
    public int ItemNum
    {
        get { return itemNum; }
        set { itemNum = value; }
    }

    /// <summary>
    /// 物品是否有耐久
    /// </summary>
    public int ItemBar
    {
        get { return itemBar; }
        set { itemBar = value; }
    }

    /// <summary>
    /// 物品剩余耐久值
    /// </summary>
    public string BarValue
    {
        get { return barValue; }
        set { barValue = value; }
    }

    public InventoryItem() { }
    public InventoryItem(int itemId, string itemName, int itemNum, int itemBar, string itemBarValue)
    {
        this.ItemId = itemId;
        this.ItemName = itemName;
        this.ItemNum = itemNum;
        this.ItemBar = itemBar;
        this.BarValue = itemBarValue;
    }

    public override string ToString()
    {
        return string.Format("物品的名称:{0}, 数量:{1}, Id:{2}, 是否有耐久{3}, 耐久值剩余{4}", this.itemName, this.itemNum, this.itemId, this.itemBar, this.barValue);
    }
}
