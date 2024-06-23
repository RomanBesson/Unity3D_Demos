using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 枪械c层最底端抽象父类.
/// </summary>
public abstract class GunControllerBase : MonoBehaviour {

    //数值字段.
    [SerializeField]
    private int id;
    [SerializeField]
    private int damage;                  //伤害值
    [SerializeField]
    private int durable;                 //耐久
    private float durable_max;             //初始耐久（满耐久的数值）
    [SerializeField]
    private GunType gunWeaponType;       //类型

    //组件字段.
    private GunViewBase m_GunViewBase;   //枪械V层父类
    private AudioClip audio;             //开枪音效
    private GameObject effect;           //枪口特效
    private GameObject toolBarIcon;      //武器在物品栏里对应的Icon游戏物体

    private Ray ray;                     //枪口射线
    private RaycastHit hit;              //枪口射线检测到的物体

    private bool canShoot = true;       //限制连续开枪
    public int Id { get { return id; } set { id = value; } }
    public int Damage { get { return damage; } set { damage = value; } }
    public GunType GunWeaponType { get { return gunWeaponType; } set { gunWeaponType = value; } }

    public GunViewBase M_GunViewBase { get { return m_GunViewBase; } set { m_GunViewBase = value; } }
    public AudioClip Audio { get { return audio; } set { audio = value; } }
    public GameObject Effect { get { return effect; } set { effect = value; } }
    /// <summary>
    /// 武器在物品栏里对应的Icon游戏物体
    /// </summary>
    public GameObject ToolBarIcon { get { return toolBarIcon; } set { toolBarIcon = value; } }

    public Ray MyRay { get { return ray; } set { ray = value; } }
    public RaycastHit Hit { get { return hit; } set { hit = value; } }


    public int Durable
    {
        get { return durable; }
        set
        {
            durable = value;
            if (durable <= 0)
            {
                //销毁自身.
                GameObject.Destroy(gameObject);
                //销毁准星UI.
                GameObject.Destroy(m_GunViewBase.M_GunStar.gameObject);
            }
        }
    }

    protected virtual void Start()
    {
        m_GunViewBase = gameObject.GetComponent<GunViewBase>();
        //保存满耐久数值
        durable_max = Durable;
        LoadAudioAsset();
        Init();
    }
    void Update()
    {
        ShootReady();
        MouseControl();
    }

    /// <summary>
    /// 执行放下武器的动作
    /// </summary>
    public void Holster()
    {
        M_GunViewBase.M_Animator.SetTrigger("Holster");
    }

    /// <summary>
    /// 播放音效.
    /// </summary>
    protected void PlayAudio()
    {
        AudioSource.PlayClipAtPoint(Audio, M_GunViewBase.M_GunPoint.position);
    }

    /// <summary>
    /// 射击准备.
    /// </summary>
    protected void ShootReady()
    {
        ray = new Ray(M_GunViewBase.M_GunPoint.position, M_GunViewBase.M_GunPoint.forward);
        if (Physics.Raycast(ray, out hit))
        {
            //改变准星位置到射线结束点
            Vector2 uiPos = RectTransformUtility.WorldToScreenPoint(M_GunViewBase.M_EnvCamera, hit.point);
            M_GunViewBase.M_GunStar.position = uiPos;
        }
        else
        {
            hit.point = Vector3.zero;
        }
    }

    /// <summary>
    /// 更新UI显示的该武器的耐久条
    /// </summary>
    private void UpdateUI()
    {
        //耐久值.
        toolBarIcon.GetComponent<InventoryItemController>().UpdateUI(Durable / durable_max);
    }

    /// <summary>
    /// 鼠标控制.
    /// </summary>
    private void MouseControl()
    {
        if (Input.GetMouseButtonDown(0) && canShoot)   //按下鼠标左键-->发射子弹.
        {
            MouseButtonLeftDown();
        }

        if (Input.GetMouseButton(1))         //按住鼠标右键-->进入开镜状态.
        {
            MouseButtonRight();
        }

        if (Input.GetMouseButtonUp(1))       //松开鼠标右键-->退出开镜状态.
        {
            MouseButtonRightUp();
        }
    }

    /// <summary>
    /// 鼠标左键点击事件
    /// </summary>
    protected virtual void MouseButtonLeftDown()
    {
        Shoot();
        UpdateUI();
        PlayAudio();
        M_GunViewBase.M_Animator.SetTrigger("Fire");
    }

    /// <summary>
    /// 鼠标右键按住事件
    /// </summary>
    private void MouseButtonRight()
    {
        //开镜
        M_GunViewBase.M_Animator.SetBool("HoldPose", true);
        M_GunViewBase.EnterHoldPose();
        //关闭准星
        M_GunViewBase.M_GunStar.gameObject.SetActive(false);
    }

    /// <summary>
    /// 鼠标右键抬起事件
    /// </summary>
    private void MouseButtonRightUp()
    {
        //结束开镜
        M_GunViewBase.M_Animator.SetBool("HoldPose", false);
        M_GunViewBase.ExitHoldPose();
        //鼠标准星回归
        M_GunViewBase.M_GunStar.gameObject.SetActive(true);
    }

    /// <summary>
    /// 对象延迟添加进对象池
    /// </summary>
    /// <param name="pool">对象池对象</param>
    /// <param name="go">要添加的对象</param>
    /// <param name="time">延迟添加时间</param>
    /// <returns></returns>
    protected IEnumerator Delay(ObjectPool pool, GameObject go, float time)
    {
        yield return new WaitForSeconds(time);
        pool.AddObject(go);
    }

    /// <summary>
    /// 切换是否可以射击
    /// </summary>
    /// <param name="state"></param>
    protected void CanShoot(int state)
    {
        if (state == 0)
            canShoot = false;
        else
            canShoot = true;
    }

    /// <summary>
    /// 初始化
    /// </summary>
    protected abstract void Init();

    /// <summary>
    /// 加载开枪特效
    /// </summary>
    protected abstract void LoadAudioAsset();

    /// <summary>
    /// 射击事件
    /// </summary>
    protected abstract void Shoot();
}
