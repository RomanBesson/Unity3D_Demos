using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 武器_斧头脚本
/// </summary>
public class StoneHatchet : MonoBehaviour {

    [SerializeField]
    private int id;
    public int Id { get { return id; } set { id = value; } }

    [SerializeField]
    private int damage;                  //伤害值
    /// <summary>
    /// 伤害值
    /// </summary>
    public int Damage { get { return damage; } set { damage = value; } }

    [SerializeField]
    private int durable;                 //耐久
    /// <summary>
    /// 耐久
    /// </summary>
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
            }
        }
    }

    /// <summary>
    /// 初始耐久（满耐久的数值）
    /// </summary>
    private float durable_max;             
    
    [SerializeField]
    private GunType gunWeaponType;       //类型
    /// <summary>
    /// 类型
    /// </summary>
    public GunType GunWeaponType { get { return gunWeaponType; } set { gunWeaponType = value; } }

    //武器对应的Icon.
    private GameObject toolBarIcon;
    /// <summary>
    /// 武器对应的Icon.
    /// </summary>
    public GameObject ToolBarIcon { get { return toolBarIcon; } set { toolBarIcon = value; } }

    private Animator m_Animator;
    /// <summary>
    /// 斧头碰撞射线起始点位置
    /// </summary>
    private Transform point_Transform;

    //物理射线.
    private Ray ray;
    private RaycastHit hit;

    void Start()
    {
        durable_max = Durable;
        m_Animator = gameObject.GetComponent<Animator>();
        point_Transform = transform.Find("Point");
    }

    void Update()
    {
        AttackReady();

        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    /// <summary>
    /// 攻击
    /// </summary>
    private void Attack()
    {
        m_Animator.SetTrigger("Hit");
        Durable--;
        UpdateUI();
    }

    /// <summary>
    /// 调用石头受击方法
    /// </summary>
    private void AttackStone()
    {
        if (hit.collider != null && hit.collider.tag == "Stone")
        {
            hit.collider.GetComponent<BulletMark>().HatchetHit(hit, Damage);
        }
    }

    /// <summary>
    /// 修改耐久值UI
    /// </summary>
    private void UpdateUI()
    {
        //耐久值.
        toolBarIcon.GetComponent<InventoryItemController>().UpdateUI(Durable / durable_max);
    }

    /// <summary>
    /// 攻击准备.
    /// </summary>
    protected void AttackReady()
    {
        //记录下当前射线反馈
        ray = new Ray(point_Transform.position, point_Transform.forward);
        if (Physics.Raycast(ray, out hit, 2)) { }
    }

    /// <summary>
    /// 执行放下动作
    /// </summary>
    public void Holster()
    {
        m_Animator.SetTrigger("Holster");
    }


}
