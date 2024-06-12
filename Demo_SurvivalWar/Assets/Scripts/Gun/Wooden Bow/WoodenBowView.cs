using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 弓箭的资源加载层（v层）
/// </summary>
public class WoodenBowView : GunViewBase
{

    private GameObject arrow; //箭
    public GameObject M_Arrow { get { return arrow; } }

    protected override void Init()
    {
        arrow = Resources.Load<GameObject>("Gun/Arrow");
    }

    protected override void InitHoldPoseValue()
    {
        M_StartPos = transform.localPosition;
        M_StartRot = transform.localRotation.eulerAngles;
        M_EndPos = new Vector3(0.75f, -1.2f, 0.22f);
        M_EndRot = new Vector3(2.5f, -8, 35);
    }

    protected override void FindGunPoint()
    {
        M_GunPoint = transform.Find("Armature/Arm_L/Forearm_L/Wrist_L/Weapon/EffectPos_A");
    }
}
