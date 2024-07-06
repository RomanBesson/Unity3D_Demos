using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 链表节点
/// </summary>
/// <typeparam name="T"></typeparam>
public class Node<T>
{
    public T Item { get; set; }
    public Node<T> Next { get; set; }

    public Node(T item)
    {
        Item = item;
        Next = null;
    }
}
