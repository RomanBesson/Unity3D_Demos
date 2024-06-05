using LitJson;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 背包模块模型层
/// </summary>
public class InventoryPanelModel : MonoBehaviour
{
    public List<InventoryItem> GetJsonList(string fileName)
    {
        return JsonTools.LoadJsonFile<InventoryItem>(fileName);
    }
}
