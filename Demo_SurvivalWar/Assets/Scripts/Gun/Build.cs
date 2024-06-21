using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 建造角色的脚本
/// </summary>
public class Build : MonoBehaviour {

    void OnEnable()
    {
        //显示建造UI
        InputManager.Instance.BuildState = true;
    }

    void OnDisable()
    {
        //隐藏建造UI
        InputManager.Instance.BuildState = false;
    }
}
