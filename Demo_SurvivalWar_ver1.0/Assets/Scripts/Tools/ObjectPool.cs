using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 对象池.
/// </summary>
public class ObjectPool : MonoBehaviour {

    private Queue<GameObject> pool = null;

    void Awake()
    {
        pool = new Queue<GameObject>();
    }

    /// <summary>
    /// 添加对象
    /// </summary>
    /// <param name="go"></param>
    public void AddObject(GameObject go)
    {
        go.SetActive(false);
        pool.Enqueue(go);
    }

    /// <summary>
    /// 获取对象
    /// </summary>
    /// <returns></returns>
    public GameObject GetObject()
    {
        GameObject temp = null;
        if(pool.Count > 0)
        {
            //剔除出队列
           temp = pool.Dequeue();
           temp.SetActive(true);
        }
        return temp;
    }

    /// <summary>
    /// 是否存在数据
    /// </summary>
    /// <returns></returns>
    public bool Data()
    {
        if (pool.Count > 0)
            return true;
        else
            return false;
    }
}
