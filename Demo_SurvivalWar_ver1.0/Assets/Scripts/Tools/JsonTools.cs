using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
/// <summary>
/// 加载json的工具类
/// </summary>
public sealed class JsonTools {

    /// <summary>
    /// 通过文件名称加载Json文件[Resources]
    /// </summary>
    public static List<T> LoadJsonFile<T>(string fileName)
    {
        List<T> tempList = new List<T>();
        
        string tempJsonStr = Resources.Load<TextAsset>("JsonData/" + fileName).text;

        //解析JSON.
        JsonData jsonData = JsonMapper.ToObject(tempJsonStr);
        for (int i = 0; i < jsonData.Count; i++)
        {
            T ii = JsonMapper.ToObject<T>(jsonData[i].ToJson());
            tempList.Add(ii);
        }

        return tempList;
    }

    /// <summary>
    /// 通过文件名称加载Json文件[IO]
    /// </summary>
    public static List<T> LoadJsonFileByIO<T>(string fileName)
    {
        List<T> tempList = new List<T>();

        //string path = Application.isEditor ? Path.Combine(Application.dataPath, "Resources", "JsonData", fileName + ".txt") : Path.Combine(Application.streamingAssetsPath, fileName + ".txt");

        string path = Path.Combine(Application.streamingAssetsPath, fileName + ".txt");

        string tempJsonStr = File.ReadAllText(path);


        //解析JSON.
        JsonData jsonData = JsonMapper.ToObject(tempJsonStr);
        for (int i = 0; i < jsonData.Count; i++)
        {
            T ii = JsonMapper.ToObject<T>(jsonData[i].ToJson());
            tempList.Add(ii);
        }

        return tempList;
    }

}
