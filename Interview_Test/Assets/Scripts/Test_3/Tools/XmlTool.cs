using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public sealed class XmlTool
{


    /// <summary>
    /// xml_写入数据
    /// </summary>
    /// <param name="stats"></param>
    /// <param name="filePath"></param>
    public static void WriteGameStatsToXml<T>(T data, string filename)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, filename + ".xml");

        XmlSerializer serializer = new XmlSerializer(typeof(GameData));
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            serializer.Serialize(writer, data);
        }
    }

    /// <summary>
    /// xml_读取数据
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public static T ReadGameStatsFromXml<T>(string filename) where T : class
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, filename + ".xml");
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        using (StreamReader reader = new StreamReader(filePath))
        {
            return (T)serializer.Deserialize(reader);
        }
    }
}
