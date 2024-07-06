using System.Collections;
using System.Collections.Generic;
using System;


public class MyQueue<T>
{
    /// <summary>
    /// 头节点
    /// </summary>
    private Node<T> frontNode;
    /// <summary>
    /// 当前节点
    /// </summary>
    private Node<T> curNode;
    /// <summary>
    /// 节点数量
    /// </summary>
    private int count;

    public MyQueue()
    {
        frontNode = null;
        curNode = null;
        count = 0;
    }

    /// <summary>
    /// 添加元素
    /// </summary>
    /// <param name="item"></param>
    public void Enqueue(T item)
    {
        Node<T> newNode = new Node<T>(item);
        if (curNode == null)
        {
            frontNode = newNode;
            curNode = frontNode;
        }
        else
        {
            curNode.Next = newNode;
            curNode = newNode;
        }
        count++;
    }

    /// <summary>
    /// 删除元素
    /// </summary>
    /// <returns></returns>
    public T Dequeue()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("队列为空");
        }

        T item = frontNode.Item;
        frontNode = frontNode.Next;
        if (frontNode == null)
        {
            curNode = null;
        }
        count--;
        return item;
    }

    /// <summary>
    /// 判断队列是否为空
    /// </summary>
    /// <returns></returns>
    public bool IsEmpty()
    {
        return count == 0;
    }

    /// <summary>
    /// 查看队列头节点的元素
    /// </summary>
    /// <returns></returns>
    public T Peek()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("Queue is empty.");
        }
        return frontNode.Item;
    }

 
}

