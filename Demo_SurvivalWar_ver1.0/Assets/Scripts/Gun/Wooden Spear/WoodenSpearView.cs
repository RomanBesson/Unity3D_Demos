using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 长矛v层
/// </summary>
public class WoodenSpearView : GunViewBase {

    private GameObject spear;

    public GameObject M_Spear { get { return spear; } }

    protected override void Init()
    {
        spear = Resources.Load<GameObject>("Gun/Wooden_Spear");
    }

    protected override void InitHoldPoseValue()
    {
        M_StartPos = transform.localPosition;
        M_StartRot = transform.localRotation.eulerAngles;
        M_EndPos = new Vector3(0, -1.58f, 0.32f);
        M_EndRot = new Vector3(0, 4, 0.3f);  
    }

    protected override void FindGunPoint()
    {
        M_GunPoint = transform.Find("Armature/Arm_R/Forearm_R/Wrist_R/Weapon/EffectPos_A");
    }
}
