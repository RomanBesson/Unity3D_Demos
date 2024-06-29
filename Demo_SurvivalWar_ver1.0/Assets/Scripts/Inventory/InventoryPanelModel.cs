using LitJson;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// 背包模块模型层
/// </summary>
public class InventoryPanelModel : MonoBehaviour
{
    /// <summary>
    /// 通过Json文件名获取List对象.
    /// </summary>
    /// <param name="fileName">Json文件名</param>
    /// <returns>List对象</returns>
    public List<InventoryItem> GetJsonList(string fileName)
    {
        return JsonTools.LoadJsonFileByIO<InventoryItem>(fileName);
       //return JsonTools.LoadJsonFile<InventoryItem>(fileName);
    }

    /// <summary>
    /// 背包内的物品对象转换成Json数据,并且存储.
    /// </summary>
    /// <param name="list"></param>
    public void ObjectToJson(List<GameObject> list, string fileName)
    {
        List<InventoryItem> tempList = new List<InventoryItem>();
        
        //将背包数据转化为实体类数据
        for (int i = 0; i < list.Count; i++)
        {
            Transform tempTransform = list[i].GetComponent<Transform>();
            InventoryItem item = null;
            if (tempTransform.childCount != 0) //当前物品槽内有物品.
            {
                InventoryItemController iic = tempTransform.Find("InventoryItem").GetComponent<InventoryItemController>();
                item = new InventoryItem(iic.Id, iic.GetImageName(), iic.Num, iic.GetBar(), iic.GetBarValue());

            }
            else if (tempTransform.childCount == 0) //当前是一个空的物品槽.
            {
                item = new InventoryItem(0, "", 0, 0, "0");
            }
            tempList.Add(item);
        }

        //写入
        string str = JsonMapper.ToJson(tempList);

        File.Delete(Path.Combine(Application.streamingAssetsPath, fileName));
        StreamWriter sw = new StreamWriter(Path.Combine(Application.streamingAssetsPath, fileName));

        sw.Write(str);
        sw.Close();
    }
}
