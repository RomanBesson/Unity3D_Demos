using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 弹壳管理脚本
/// </summary>
public class Shell : MonoBehaviour {

    private Transform m_Transform;

	void Start () {
        m_Transform = gameObject.GetComponent<Transform>();
	}
	
	void Update () {
		//生成后旋转
        m_Transform.Rotate(Vector3.up * Random.Range(10, 30));
	}
}
