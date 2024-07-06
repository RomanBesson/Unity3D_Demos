using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class ToolBarPanelModel : MonoBehaviour {

    /// <summary>
    /// 通过Json文件名获取List对象.
    /// </summary>
    /// <param name="fileName">Json文件名</param>
    /// <returns>List对象</returns>
    public List<InventoryItem> GetJsonList(string fileName)
    {
        return JsonTools.LoadJsonFileByIO<InventoryItem>(fileName);
    }

    /// <summary>
    /// 工具栏的物品对象转换成Json数据,并且存储.
    /// </summary>
    /// <param name="list"></param>
    public void ObjectToJson(List<GameObject> list, string fileName)
    {
        List<InventoryItem> tempList = new List<InventoryItem>();

        //将工具栏数据转化为实体类数据
        for (int i = 0; i < list.Count; i++)
        {
            Transform tempTransform = list[i].GetComponent<Transform>();
            InventoryItem item = null;
            if (tempTransform.Find("InventoryItem") != null) //当前物品槽内有物品.
            {
                //把这个物品打包
                InventoryItemController iic = tempTransform.Find("InventoryItem").GetComponent<InventoryItemController>();
                item = new InventoryItem(iic.Id, iic.GetImageName(), iic.Num, iic.GetBar(), iic.GetBarValue());

            }
            else//当前是一个空的物品槽.
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
