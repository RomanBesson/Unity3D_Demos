using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 布娃娃系统管理脚本
/// </summary>
public class AIRagdoll : MonoBehaviour {

    private Transform m_Transform;
    private BoxCollider m_BoxCollider_A;
    private BoxCollider m_BoxCollider_B;

	void Start () {
        m_Transform = gameObject.GetComponent<Transform>();
        m_BoxCollider_A = m_Transform.Find("Armature").GetComponent<BoxCollider>();
        m_BoxCollider_B = m_Transform.Find("Armature/Hips/Middle_Spine").GetComponent<BoxCollider>();
	}
    /// <summary>
    /// 布娃娃系统模拟死亡状态
    /// </summary>
    public void StartRagdoll()
    {
        m_BoxCollider_A.enabled = false;
        m_BoxCollider_B.enabled = false;
    }
}
