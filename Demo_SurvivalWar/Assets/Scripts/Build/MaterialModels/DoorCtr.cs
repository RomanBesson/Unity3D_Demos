using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 控制开关门的脚本
/// </summary>
public class DoorCtr : MonoBehaviour {

    /// <summary>
    /// 开门
    /// </summary>
    public void OpenDoor()
    {
        transform.Rotate(Vector3.up, -90);
    }

    /// <summary>
    /// 关门
    /// </summary>
    public void CloseDoor()
    {
        transform.Rotate(Vector3.up, 90);
    }
}
