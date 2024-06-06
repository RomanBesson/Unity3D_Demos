using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// 步枪的V层
/// </summary>
public class AssaultRifleView : MonoBehaviour
{
    private Animator m_Animator;
    private Camera m_EnvCamera; //环境摄像机的位置
    private Camera m_PersonCamera;

    //开镜时用的位置参数
    private Vector3 startPos;
    private Vector3 startRot;
    private Vector3 endPos;
    private Vector3 endRot;

    //位置.
    private Transform gunPoint; //枪口.
    private Transform gunStar;  //准星UI.
    private Transform effectPos;//特效挂点.

    private GameObject bullet;  //临时子弹模型.

    private AudioClip audio; //射击音效
    private GameObject effect; //枪口特效

    private GameObject shell;   //弹壳.

    //管理特效和弹壳的临时父物体.
    private Transform effectParent; //特效父物体.
    private Transform shellParent;  //弹壳父物体.

    //组件属性.
    public Animator M_Animator { get { return m_Animator; } }
    public Camera M_EnvCamera { get { return m_EnvCamera; } }
    public Transform M_GunPoint { get { return gunPoint; } }
    public GameObject M_Bullet { get { return bullet; } }
    public Transform M_GunStar { get { return gunStar; } }
    public Transform M_EffectPos { get { return effectPos; } }
    public GameObject M_Shell { get { return shell; } }
    public AudioClip M_Audio { get { return audio; } }
    public GameObject M_Effect { get { return effect; } }
    public Transform M_EffectParent { get{ return effectParent; } }
    public Transform M_ShellParent { get { return shellParent; } }

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        m_EnvCamera = GameObject.Find("EnvCamera").GetComponent<Camera>();
        m_PersonCamera = GameObject.Find("PersonCamera").GetComponent<Camera>();
        startPos = transform.localPosition;
        startRot = transform.localRotation.eulerAngles; //四元数转换欧拉角
        endPos = new Vector3(-0.065f, -1.85f, 0.25f);
        endRot = new Vector3(2.8f, 1.3f, 0.08f);
        gunPoint = transform.Find("Assault_Rifle/EffectPos_A");
        effectPos = transform.Find("Assault_Rifle/EffectPos_B");
        bullet = Resources.Load<GameObject>("Gun/Bullet");
        gunStar = GameObject.Find("GunStar").GetComponent<Transform>();
        audio = Resources.Load<AudioClip>("Audios/Gun/AssaultRifle_Fire");
        effect = Resources.Load<GameObject>("Effects/Gun/AssaultRifle_GunPoint_Effect");
        shell = Resources.Load<GameObject>("Gun/Shell");
        effectParent = GameObject.Find("TempObject/AssaultRifle_Effect_Parent").GetComponent<Transform>();
        shellParent = GameObject.Find("TempObject/AssaultRifle_Shell_Parent").GetComponent<Transform>();
    }

    /// <summary>
    /// 进入开镜状态
    /// </summary>
    public void EnterHoldPose()
    {
        //调高环境摄像机的fov，把枪械位置放到中央
        transform.DOLocalMove(endPos, 0.2f);
        transform.DOLocalRotate(endRot, 0.2f);
        m_EnvCamera.DOFieldOfView(40, 0.2f);
        //m_PersonCamera.DOFieldOfView(20, 0.2f);

        m_Animator.SetBool("HoldPose", true);
        //隐藏准星
        gunStar.gameObject.SetActive(false);
    }

    /// <summary>
    /// 退出开镜状态
    /// </summary>
    public void ExitHoldPose()
    {
        transform.DOLocalMove(startPos, 0.2f);
        transform.DOLocalRotate(startRot, 0.2f);
        m_EnvCamera.DOFieldOfView(60, 0.2f);
       // m_PersonCamera.DOFieldOfView(40, 0.2f);

        m_Animator.SetBool("HoldPose", false);
        //显示准星
        gunStar.gameObject.SetActive(true);
    }
}
