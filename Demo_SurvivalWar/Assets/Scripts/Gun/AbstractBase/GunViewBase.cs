using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// 枪械View层抽象父类.
/// </summary>
public abstract class GunViewBase : MonoBehaviour {

    private Transform gunPoint;          //枪口.
    private Transform gunStar;           //准星UI.
    private GameObject prefab_Star;      //准星Prefab.
    //基础组件字段.
    private Animator m_Animator;
    private Camera m_EnvCamera;

    //优化开镜动作.
    private Vector3 startPos;
    private Vector3 startRot;
    private Vector3 endPos;
    private Vector3 endRot;


    //基础组件属性.
    public Animator M_Animator { get { return m_Animator; } }
    public Camera M_EnvCamera { get { return m_EnvCamera; } }

    public Transform M_GunStar { get { return gunStar; } }
    public Transform M_GunPoint { get { return gunPoint; } set { gunPoint = value; } }

    public Vector3 M_StartPos { get { return startPos; } set { startPos = value; } }
    public Vector3 M_StartRot { get { return startRot; } set { startRot = value; } }
    public Vector3 M_EndPos { get { return endPos; } set { endPos = value; } }
    public Vector3 M_EndRot { get { return endRot; } set { endRot = value; } }


    protected virtual void Awake()
    {
        //基础组件查找.
        m_Animator = gameObject.GetComponent<Animator>();
        m_EnvCamera = GameObject.Find("EnvCamera").GetComponent<Camera>();
        //准星的加载
        prefab_Star = Resources.Load<GameObject>("Gun/GunStar");
        gunStar = GameObject.Instantiate<GameObject>(prefab_Star, GameObject.Find("MainPanel").GetComponent<Transform>()).GetComponent<Transform>();
        InitHoldPoseValue();
        FindGunPoint();
        Init();
    }

    private void OnEnable()
    {
        ShowStar();
    }

    private void OnDisable()
    {
        HideStar();
    }

    /// <summary>
    /// 显示准星.
    /// </summary>
    private void ShowStar()
    {
        gunStar.gameObject.SetActive(true);
    }

    /// <summary>
    /// 隐藏准星.
    /// </summary>
    private void HideStar()
    {
        if (gunStar != null)
            gunStar.gameObject.SetActive(false);
    }

    /// <summary>
    /// 进入开镜状态.--动作优化.
    /// </summary>
    public void EnterHoldPose(float time = 0.2f, int fov = 40)
    {
        transform.DOLocalMove(M_EndPos, time);
        transform.DOLocalRotate(M_EndRot, time);
        M_EnvCamera.DOFieldOfView(fov, time);
    }

    /// <summary>
    /// 退出开镜状态.--动作优化.
    /// </summary>
    public void ExitHoldPose(float time = 0.2f, int fov = 60)
    {
        transform.DOLocalMove(M_StartPos, time);
        transform.DOLocalRotate(M_StartRot, time);
        M_EnvCamera.DOFieldOfView(fov, time);
    }

    /// <summary>
    /// 初始化
    /// </summary>
    protected abstract void Init();

    /// <summary>
    /// 初始化开镜动作相关的4个字段.
    /// </summary>
    protected abstract void InitHoldPoseValue();

    /// <summary>
    /// 查找枪口.
    /// </summary>
    protected abstract void FindGunPoint();
}
