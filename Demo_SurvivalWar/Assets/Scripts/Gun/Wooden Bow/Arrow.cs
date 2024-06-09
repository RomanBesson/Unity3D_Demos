using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 弓箭箭矢的管理类
/// </summary>
public class Arrow : MonoBehaviour {

    private Rigidbody m_Rigidbody;
    private BoxCollider m_BoxCollider;

    private int damage;                     //伤害值

	void Awake () {
        m_Rigidbody = gameObject.GetComponent<Rigidbody>();
        m_BoxCollider = gameObject.GetComponent<BoxCollider>();
	}

    public void Shoot(Vector3 dir, int force, int damage)
    {
        m_Rigidbody.AddForce(dir * force);
        this.damage = damage;
    }

    void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject.name!= "FPSController") m_Rigidbody.Sleep();

        //如果射中的环境层的物体
        if (coll.collider.gameObject.layer == LayerMask.NameToLayer("Env"))
        {
            //移除刚体和碰撞体，防止被玩家撞飞
            GameObject.Destroy(m_Rigidbody);
            GameObject.Destroy(m_BoxCollider);
            //被射中物体的伤害结算
            coll.collider.GetComponent<BulletMark>().Hp -= damage;
            //将父物体设置为被射对象（依附在他身上）
            transform.SetParent(coll.collider.gameObject.transform);
        }
    }
}
