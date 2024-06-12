using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 场景中石头管理类的脚本
/// </summary>
public class StoneManager : MonoBehaviour {

    private Transform[] points;                 //石头的预生成点位
    private Transform stone_Transform;          //石头的父游戏对象

    private GameObject prefab_Stone;            //石头1的预制体
    private GameObject prefab_Stone_1;          //石头2的预制体

	void Start () {
        stone_Transform = transform.Find("Stones");
        points = transform.Find("StonePoints").GetComponentsInChildren<Transform>();
        prefab_Stone = Resources.Load<GameObject>("Env/Rock_Normal");
        prefab_Stone_1 = Resources.Load<GameObject>("Env/Rock_Metal");

        //隐藏石头的预生成点位模型
        for (int i = 1; i < points.Length; i++)
        {
            points[i].GetComponent<MeshRenderer>().enabled = false;
        }

        //在Stones父类下生成石头
        for (int i = 1; i < points.Length; i++)
        {
            //随机生成石头种类
            int index = Random.Range(0, 2);
            GameObject prefab;
            if (index == 0)
                prefab = prefab_Stone;
            else
                prefab = prefab_Stone_1;

            Transform stone = GameObject.Instantiate<GameObject>(prefab, points[i].localPosition, Quaternion.identity, stone_Transform).GetComponent<Transform>();
            
            //随机生成石头大小
            float size = Random.Range(0.5f, 2.5f);
            stone.localScale = stone.localScale * size;

            //随机石头旋转（用四元数，避免万向锁）
            Vector3 rot = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
            stone.localRotation = Quaternion.Euler(rot);

        }

	}
	

}
