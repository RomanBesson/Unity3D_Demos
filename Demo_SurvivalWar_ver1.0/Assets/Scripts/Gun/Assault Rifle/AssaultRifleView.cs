using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// 步枪的V层
/// </summary>
public class AssaultRifleView : GunViewBase
{
    //位置.

    private Transform effectPos;//特效挂点.

    private GameObject bullet;  //临时子弹模型.

    private AudioClip audio; //射击音效
    private GameObject effect; //枪口特效

    private GameObject shell;   //弹壳.

    //管理特效和弹壳的临时父物体.
    private Transform effectParent; //特效父物体.
    private Transform shellParent;  //弹壳父物体.

    //组件属性.
    public GameObject M_Bullet { get { return bullet; } }
    public Transform M_EffectPos { get { return effectPos; } }
    public GameObject M_Shell { get { return shell; } }
    public AudioClip M_Audio { get { return audio; } }
    public GameObject M_Effect { get { return effect; } }
    public Transform M_EffectParent { get{ return effectParent; } }
    public Transform M_ShellParent { get { return shellParent; } }

    protected override void FindGunPoint()
    {
        M_GunPoint = transform.Find("Assault_Rifle/EffectPos_A");
    }

    protected override void InitHoldPoseValue()
    {
        //优化开镜动作.
        M_StartPos = transform.localPosition;
        M_StartRot = transform.localRotation.eulerAngles;
        M_EndPos = new Vector3(-0.065f, -1.85f, 0.25f);
        M_EndRot = new Vector3(2.8f, 1.3f, 0.08f);
    }

    protected override void Init()
    {
        effectPos = transform.Find("Assault_Rifle/EffectPos_B");
        bullet = Resources.Load<GameObject>("Gun/Bullet");
        audio = Resources.Load<AudioClip>("Audios/Gun/AssaultRifle_Fire");
        effect = Resources.Load<GameObject>("Effects/Gun/AssaultRifle_GunPoint_Effect");
        shell = Resources.Load<GameObject>("Gun/Shell");
        effectParent = GameObject.Find("TempObject/AssaultRifle_Effect_Parent").GetComponent<Transform>();
        shellParent = GameObject.Find("TempObject/AssaultRifle_Shell_Parent").GetComponent<Transform>();
    }
}
