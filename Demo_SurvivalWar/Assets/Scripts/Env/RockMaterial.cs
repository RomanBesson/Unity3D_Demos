using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 石头掉落物的脚本
/// </summary>
public class RockMaterial : MonoBehaviour {

    private string name = "2";

    /// <summary>
    /// 对应json的索引名称
    /// </summary>
    public string Name
    {
        get { return name; }
    }


}
