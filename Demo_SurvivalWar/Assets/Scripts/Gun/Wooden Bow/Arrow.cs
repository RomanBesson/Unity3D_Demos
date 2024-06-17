using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 弓箭箭矢的管理类
/// </summary>
public class Arrow : BulletBase {

    private BoxCollider m_BoxCollider;

    private Transform m_Pivot;
    private RaycastHit hit;                 


    public override void Init()
    {
        m_BoxCollider = gameObject.GetComponent<BoxCollider>();
        m_Pivot = transform.Find("Pivot").GetComponent<Transform>();
    }

    public override void Shoot(Vector3 dir, int force, int damage, RaycastHit hit)
    {
        M_Rigidbody.AddForce(dir * force);
        this.M_Damage = damage;
        this.hit = hit;
    }

    public override void CollisionEnter(Collision coll)
    {
        //安全检测：防止和自身碰撞器相撞导致中途停止
        if (coll.gameObject.name != "FPSController") M_Rigidbody.Sleep();

        //如果射中的环境层的物体
        if (coll.collider.gameObject.layer == LayerMask.NameToLayer("Env"))
        {
            //移除刚体和碰撞体，防止长矛被玩家撞飞
            GameObject.Destroy(M_Rigidbody);
            GameObject.Destroy(m_BoxCollider);

            //被射中物体的伤害结算
            coll.collider.GetComponent<BulletMark>().Hp -= M_Damage;

            //将父物体设置为被射对象（依附在他身上）
            transform.SetParent(coll.collider.gameObject.transform);

            //颤动效果（这个写法可以使用父类的私有协程）
            StartCoroutine("TailAnimation", m_Pivot);
        }
        //如果射中了AI层的物体
        if (coll.collider.gameObject.layer == LayerMask.NameToLayer("AI"))
        {
            //移除刚体和碰撞体，防止长矛被玩家撞飞
            GameObject.Destroy(M_Rigidbody);
            GameObject.Destroy(m_BoxCollider);

            //削减AI角色的生命值.（碰撞器在子物体的骨骼动画上，脚本在他父物体上，所以获取它父物体的脚本）
            //击中了头部
            if (coll.collider.gameObject.name == "Head")
            {
                coll.collider.GetComponentInParent<AI>().HeadHit(M_Damage * 2);
            }
            //击中其他位置
            else
            {
                coll.collider.GetComponentInParent<AI>().NormalHit(M_Damage);
            }

            //将父物体设置为被射对象（依附在他身上）
            transform.SetParent(coll.collider.gameObject.transform);

            //颤动效果（这个写法可以使用父类的私有协程）
            StartCoroutine("TailAnimation", m_Pivot);

            //播放特效
            coll.collider.GetComponentInParent<AI>().PlayerEffect(hit);
        }
    }
}
