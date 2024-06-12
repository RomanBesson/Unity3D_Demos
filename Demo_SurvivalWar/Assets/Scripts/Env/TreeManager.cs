using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 场景中树管理类的脚本
/// </summary>
public class TreeManager : MonoBehaviour {

    private Transform tree_Transform;      //要生成的树的父游戏物体
    private Transform[] points;            //树的预生成点

    private GameObject prefab_Tree;        //要生成的树的预制体

	void Start () {
        tree_Transform = transform.Find("Trees");
        points = transform.Find("TreePoints").GetComponentsInChildren<Transform>();

        prefab_Tree = Resources.Load<GameObject>("Env/Conifer");

        //隐藏树的预生成点模型
        for (int i = 1; i < points.Length; i++)
        {
            points[i].GetComponent<MeshRenderer>().enabled = false;  
        }

        for (int i = 1; i < points.Length; i++)
        {
            //在 Trees 游戏物体下生成树
            Transform tree = GameObject.Instantiate<GameObject>(prefab_Tree, points[i].localPosition, Quaternion.identity, tree_Transform).GetComponent<Transform>();
            //随机高度
            float height = Random.Range(0.5f, 1.0f);
            tree.localScale = tree.localScale * height;
        }

	}
	

}
