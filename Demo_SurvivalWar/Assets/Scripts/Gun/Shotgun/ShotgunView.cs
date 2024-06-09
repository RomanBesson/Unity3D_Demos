using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunView : GunViewBase
{

    private Transform effectPos;    //特效挂点.
    private AudioClip effectAudio;  //换弹特效声音.
    private GameObject shell;       //弹壳模型.
    private GameObject bullet;      //弹头模型.

    public Transform M_EffectPos { get { return effectPos; } }
    public AudioClip M_EffectAudio { get { return effectAudio; } }
    public GameObject M_Shell { get { return shell; } }
    public GameObject M_Bullet { get { return bullet; } }

    public override void Init()
    {
        effectPos = transform.Find("Armature/Weapon/EffectPos_B");
        effectAudio = Resources.Load<AudioClip>("Audios/Gun/Shotgun_Pump");
        shell = Resources.Load<GameObject>("Gun/Shotgun_Shell");
        bullet = Resources.Load<GameObject>("Gun/Shotgun_Bullet");
    }

    public override void InitHoldPoseValue()
    {
        M_StartPos = transform.localPosition;
        M_StartRot = transform.localRotation.eulerAngles;
        M_EndPos = new Vector3(-0.14f, -1.78f, -0.03f);
        M_EndRot = new Vector3(0, 10, 0);
    }

    public override void FindGunPoint()
    {
        M_GunPoint = transform.Find("Armature/Weapon/EffectPos_A");
    }
}
