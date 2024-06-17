using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 子弹抽象父类.
/// </summary>
public abstract class BulletBase : MonoBehaviour {

    //公共字段.
    private Rigidbody m_Rigidbody;
    private int damage;                       //伤害值

    //公共属性.
    public Rigidbody M_Rigidbody { get { return m_Rigidbody; } }
    public int M_Damage { get { return damage; } set { damage = value; } }

    void Awake()
    {
        m_Rigidbody = gameObject.GetComponent<Rigidbody>();
        Init();
    }
    void OnCollisionEnter(Collision coll)
    {
        CollisionEnter(coll);
    }

    /// <summary>
    /// 尾巴颤动动画.
    /// </summary>
    private IEnumerator TailAnimation(Transform m_Pivot)
    {
        //动画执行时长.
        float stopTime = Time.time + 1.0f;
        //动画的颤动范围.
        float range = 1.0f;
        float vel = 0;
        //长矛动画开始的角度.
        Quaternion startRot = Quaternion.Euler(new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f), 0));
        while (Time.time < stopTime)
        {
            //动画的核心.
            m_Pivot.localRotation = Quaternion.Euler(new Vector3(Random.Range(-range, range), Random.Range(-range, range), 0)) * startRot;
            //平滑阻尼.
            range = Mathf.SmoothDamp(range, 0, ref vel, stopTime - Time.time);
            yield return null;
        }
    }

    /// <summary>
    /// 销毁自身
    /// </summary>
    public void KillSelf()
    {
        GameObject.Destroy(gameObject);
    }

    /// <summary>
    /// 延迟销毁子弹头
    /// </summary>
    /// <returns></returns>
    private IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(2);
        GameObject.Destroy(gameObject);
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public abstract void Init();
    /// <summary>
    /// 发射炮弹
    /// </summary>
    /// <param name="dir"></param>
    /// <param name="force"></param>
    /// <param name="damage"></param>
    public abstract void Shoot(Vector3 dir, int force, int damageM, RaycastHit hit);
    /// <summary>
    /// 发生碰撞后
    /// </summary>
    /// <param name="coll"></param>
    public abstract void CollisionEnter(Collision coll);
}
