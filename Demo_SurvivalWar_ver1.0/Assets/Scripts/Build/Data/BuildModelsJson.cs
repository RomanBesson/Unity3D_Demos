using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System;
using System.IO;

/// <summary>
/// 建筑模块_持久化数据的类
/// </summary>
public class BuildModelsJson : MonoBehaviour {

    /// <summary>
    /// 用于存储游戏数据的集合
    /// </summary>
    private List<Transform> allModelsTransform;
    /// <summary>
    /// 用于存进json的集合
    /// </summary>
    private List<BuildItem> ModelsList;
    /// <summary>
    /// 用于导出json数据的集合
    /// </summary>
    private List<BuildItem> JsonsList;

    /// <summary>
    /// json文件路径
    /// </summary>
    private string jsonPath = null;
    private GameObject prefab_Model;

    void Start()
    {
        allModelsTransform = new List<Transform>();
        ModelsList = new List<BuildItem>();
        JsonsList = new List<BuildItem>();

        jsonPath = Path.Combine(Application.streamingAssetsPath, "ModelsJson.txt");


        JsonToObject();
    }

    void OnDisable()
    {
        ObjectToJson();
    }


    /// <summary>
    /// 对象转换为Json数据.
    /// </summary>
    private void ObjectToJson()
    {
        //allModelsTransform = m_Transform.GetComponentsInChildren<Transform>();
        //把要保存的数据存进集合
        for (int i = 0; i < transform.childCount; i++)
        {
            allModelsTransform.Add(transform.GetChild(i));
        }

        //设置成实体类对象
        for (int i = 0; i < allModelsTransform.Count; i++)
        {
            Vector3 pos = allModelsTransform[i].position;
            Quaternion rot = allModelsTransform[i].rotation;
            BuildItem item = new BuildItem(allModelsTransform[i].name, Math.Round(pos.x, 2).ToString(), Math.Round(pos.y, 2).ToString(), Math.Round(pos.z, 2).ToString(),
                Math.Round(rot.x, 2).ToString(), Math.Round(rot.y, 2).ToString(), Math.Round(rot.z, 2).ToString(), Math.Round(rot.w, 2).ToString());
            ModelsList.Add(item);
        }

        //转换成json
        string str = JsonMapper.ToJson(ModelsList);
        //Debug.Log(str);

        //写入
        File.Delete(jsonPath);
        StreamWriter sw = new StreamWriter(jsonPath);
        sw.Write(str);
        sw.Close();
    }


    /// <summary>
    /// JSON转换为多个对象.
    /// </summary>
    private void JsonToObject()
    {
        //读取json数据
        string textAsset = File.ReadAllText(jsonPath);
        //Debug.Log(textAsset);
       
        //转化成对应实体类对象
        JsonData jsonData = JsonMapper.ToObject(textAsset);
        for (int i = 0; i < jsonData.Count; i++)
        {
            BuildItem item = JsonMapper.ToObject<BuildItem>(jsonData[i].ToJson());
            JsonsList.Add(item);
        }

        //生成游戏对象
        for (int i = 0; i < JsonsList.Count; i++)
        {
            Vector3 pos = new Vector3(float.Parse(JsonsList[i].PosX), float.Parse(JsonsList[i].PosY), float.Parse(JsonsList[i].PosZ));
            Quaternion rot = new Quaternion(float.Parse(JsonsList[i].RotX), float.Parse(JsonsList[i].RotY), float.Parse(JsonsList[i].RotZ), float.Parse(JsonsList[i].RotW));

            prefab_Model = Resources.Load<GameObject>(@"Build\Prefabs\" + JsonsList[i].Name);

            GameObject tempModel = GameObject.Instantiate<GameObject>(prefab_Model, pos, rot, transform);

            //初始化.
            tempModel.name = prefab_Model.name;
            tempModel.layer = 14;
            tempModel.GetComponent<MaterialModelBase>().Normal();
            GameObject.Destroy(tempModel.GetComponent<MaterialModelBase>());

        }
    }

}
