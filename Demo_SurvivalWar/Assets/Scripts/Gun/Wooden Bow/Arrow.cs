using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 弓箭箭矢的管理类
/// </summary>
public class Arrow : BulletBase {

    private BoxCollider m_BoxCollider;

    private Transform m_Pivot;


    public override void Init()
    {
        m_BoxCollider = gameObject.GetComponent<BoxCollider>();
        m_Pivot = transform.Find("Pivot").GetComponent<Transform>();
    }

    public override void Shoot(Vector3 dir, int force, int damage)
    {
        M_Rigidbody.AddForce(dir * force);
        this.M_Damage = damage;
    }

    public override void CollisionEnter(Collision coll)
    {
        if (coll.gameObject.name != "FPSController") M_Rigidbody.Sleep();

        //如果射中的环境层的物体
        if (coll.collider.gameObject.layer == LayerMask.NameToLayer("Env"))
        {
            //移除刚体和碰撞体，防止被玩家撞飞
            GameObject.Destroy(M_Rigidbody);
            GameObject.Destroy(m_BoxCollider);
            //被射中物体的伤害结算
            coll.collider.GetComponent<BulletMark>().Hp -= M_Damage;
            //将父物体设置为被射对象（依附在他身上）
            transform.SetParent(coll.collider.gameObject.transform);
            //这个写法可以使用父类的私有协程
            StartCoroutine("TailAnimation", m_Pivot);
        }
    }
}
