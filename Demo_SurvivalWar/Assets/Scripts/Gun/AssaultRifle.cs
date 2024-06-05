using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class AssaultRifle : MonoBehaviour {

    private AssaultRifleView m_AssaultRifleView;

    //字段.
    private int id;
    private int damage; //伤害值
    private int durable; //耐久
    private GunType gunWeaponType; //类型

    private AudioClip audio; //音效
    private GameObject effect; //特效

    //枪口射线
    private Ray ray; 
    private RaycastHit raycastHit;

    #region 属性
    //属性.
    public int Id
    {
        get { return id; }
        set { id = value; }
    }
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }
    public int Durable
    {
        get { return durable; }
        set { durable = value; }
    }
    public GunType GunWeaponType
    {
        get { return gunWeaponType; }
        set { gunWeaponType = value; }
    }
    public AudioClip Audio
    {
        get { return audio; }
        set { audio = value; }
    }
    public GameObject Effect
    {
        get { return effect; }
        set { effect = value; }
    }
    #endregion

    void Start () {
        Init();
    }
	
    private void Init()
    {
        m_AssaultRifleView = gameObject.GetComponent<AssaultRifleView>();
    }

	void Update () {
        MouseControl();
        ShootReady();
        
    }


    /// <summary>
    /// 鼠标控制.
    /// </summary>
    private void MouseControl()
    {
        if (Input.GetMouseButtonDown(0))     //按下鼠标左键-->发射子弹.
        {
            m_AssaultRifleView.M_Animator.SetTrigger("Fire");
            Shoot();
            PlayAudio();
            PlayEffect();
        }

        if (Input.GetMouseButton(1))         //按住鼠标右键-->进入开镜状态.
        {
            //进入开镜状态
            m_AssaultRifleView.EnterHoldPose();
        }

        if (Input.GetMouseButtonUp(1))       //松开鼠标右键-->退出开镜状态.
        {
            //退出开镜状态
            m_AssaultRifleView.ExitHoldPose();
        }
    }

 

    /// <summary>
    /// 播放音效.
    /// </summary>
    private void PlayAudio()
    {
        AudioSource.PlayClipAtPoint(m_AssaultRifleView.M_Audio, m_AssaultRifleView.M_GunPoint.position);
    }

    /// <summary>
    /// 播放特效.
    /// </summary>
    private void PlayEffect()
    {
        //枪口火花特效
        GameObject.Instantiate<GameObject>(m_AssaultRifleView.M_Effect, m_AssaultRifleView.M_GunPoint.position, Quaternion.identity).GetComponent<ParticleSystem>().Play();
        //弹出弹壳
        Rigidbody shell = GameObject.Instantiate<GameObject>(m_AssaultRifleView.M_Shell, m_AssaultRifleView.M_EffectPos.position, Quaternion.identity).GetComponent<Rigidbody>();
        shell.AddForce(m_AssaultRifleView.M_EffectPos.up * 50);
    }

    /// <summary>
    /// 射击准备.
    /// </summary>
    private void ShootReady()
    {
        ray = new Ray(m_AssaultRifleView.M_GunPoint.position, m_AssaultRifleView.M_GunPoint.forward);
       // Debug.DrawLine(m_AssaultRifleView.M_GunPoint.position, m_AssaultRifleView.M_GunPoint.forward * 500, Color.red);
        if(Physics.Raycast(ray,out raycastHit))
        {
            Debug.Log("射线碰到了物体");
            //改变准星位置到射线结束点
            Vector2 cur = RectTransformUtility.WorldToScreenPoint(m_AssaultRifleView.M_EnvCamera, raycastHit.point);
            m_AssaultRifleView.M_GunStar.position = cur;
        }
        else
        {
            Debug.Log("射线没碰到物体");
            raycastHit.point = Vector3.zero;
        }

    }

    /// <summary>
    /// 射击.
    /// </summary>
    private void Shoot()
    {
        Debug.Log("射击.");
        if (raycastHit.point != Vector3.zero)
        {
            //生成子弹
            GameObject.Instantiate(m_AssaultRifleView.M_Bullet, raycastHit.point, Quaternion.identity);
        }
    }

}
