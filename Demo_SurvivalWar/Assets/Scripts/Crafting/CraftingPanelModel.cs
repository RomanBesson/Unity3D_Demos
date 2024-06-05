using LitJson;
using System.Collections.Generic;
using UnityEngine;

public class CraftingPanelModel : MonoBehaviour
{
    //储存图谱数据
    Dictionary<int, CraftingMapItem> mapItemDic = null;

    private void Awake()
    {
        mapItemDic = LoadMapContents("CraftingMapJsonData");
    }

    /// <summary>
    /// 获取选项卡图标名称.
    /// </summary>
    /// <returns></returns>
    public string[] GetTabsIconName()
    {
        string[] names = new string[] { "Icon_House", "Icon_Weapon" };
        return names;
    }

    /// <summary>
    /// 通过ID获取对应的合成图谱.
    /// </summary>
    public CraftingMapItem GetItemById(int id)
    {
        CraftingMapItem temp = null;
        mapItemDic.TryGetValue(id, out temp);
        return temp;
    }

    /// <summary>
    /// 通过Json文件名实现CraftingContentItem数据的加载.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public List<List<CraftingContentItem>> ByNameGetJsonData(string name)
    {
        List<List<CraftingContentItem>> temp = new List<List<CraftingContentItem>>();
        string jsonStr = Resources.Load<TextAsset>("JsonData/" + name).text;

        JsonData jsonData = JsonMapper.ToObject(jsonStr);
        for (int i = 0; i < jsonData.Count; i++)
        {
            List<CraftingContentItem> tempList = new List<CraftingContentItem>();
            JsonData jd = jsonData[i]["Type"];
            for (int j = 0; j < jd.Count; j++)
            {
                tempList.Add(JsonMapper.ToObject<CraftingContentItem>(jd[j].ToJson()));
            }
            temp.Add(tempList);
        }
        return temp;
    }

    /// <summary>
    /// 加载合成图谱JSON数据.
    /// </summary>
    /// <param name="name">json文件名称</param>
    /// <returns></returns>
    public Dictionary<int, CraftingMapItem> LoadMapContents(string name)
    {
        Dictionary<int, CraftingMapItem> temp = new Dictionary<int, CraftingMapItem>();

        string jsonStr = Resources.Load<TextAsset>("JsonData/" + name).text;
        JsonData jsonData = JsonMapper.ToObject(jsonStr);

        for(int i = 0; i < jsonData.Count; i++)
        {
            int mapId = int.Parse(jsonData[i]["MapId"].ToString());
            //这个数据转换成字符数组
            string tempStr = jsonData[i]["MapContents"].ToString();
            string[] mapContents = tempStr.Split(",");
            int mapCount = int.Parse(jsonData[i]["MaterialsCount"].ToString());
            string mapName = jsonData[i]["MapName"].ToString();

            CraftingMapItem craftingMapItem = new CraftingMapItem(mapId, mapContents, mapCount, mapName);
            temp.Add(mapId, craftingMapItem);
        }

        return temp;
    }
}
