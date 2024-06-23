using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 资源工具类.
/// </summary>
public sealed class ResourcesTools {

    /// <summary>
    /// 加载文件夹资源.
    /// </summary>
    public static Dictionary<string, Sprite> LoadFolderAssets(string folderName, Dictionary<string, Sprite> dic)
    {
        Sprite[] tempSprite = Resources.LoadAll<Sprite>(folderName);
        for (int i = 0; i < tempSprite.Length; i++)
        {
            dic.Add(tempSprite[i].name, tempSprite[i]);
        }
        return dic;
    }

    /// <summary>
    /// 通过名字获取资源.
    /// </summary>
    public static Sprite GetAsset(string fileName, Dictionary<string, Sprite> dic)
    {
        Sprite temp = null;
        dic.TryGetValue(fileName, out temp);
        return temp;
    }

}
