using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

/// <summary>
/// 背包模块模型层
/// </summary>
public class InventoryPanelModel : MonoBehaviour
{
    public List<InventoryItem> GetJsonList(string fileName)
    {
        List<InventoryItem> inventoryItems = new List<InventoryItem>();

        string jsonStr = Resources.Load<TextAsset>("JsonData/" + fileName).text;

        //json的解析
        JsonData jsonData = JsonMapper.ToObject(jsonStr);
        for(int i = 0; i < jsonData.Count; i++)
        {
            InventoryItem inventoryItem = JsonMapper.ToObject<InventoryItem>(jsonData[i].ToJson());
            inventoryItems.Add(inventoryItem);
        }

        return inventoryItems;
    }
}
