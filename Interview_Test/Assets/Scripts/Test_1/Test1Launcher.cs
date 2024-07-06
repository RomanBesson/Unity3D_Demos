using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test1Launcher : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MyQueue<int> queue = new MyQueue<int>();

        // 添加元素到队列
        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);

        // 查看头节点的元素
        Debug.Log("头节点为: " + queue.Peek()); 

        // 移除并返回队列前端的元素
        Debug.Log("已成功删除 " + queue.Dequeue());

        // 查看头节点的元素
        Debug.Log("头节点为: " + queue.Peek()); 

        // 检查队列是否为空
        Debug.Log("队列是否为空 " + queue.IsEmpty()); 

    }

}
