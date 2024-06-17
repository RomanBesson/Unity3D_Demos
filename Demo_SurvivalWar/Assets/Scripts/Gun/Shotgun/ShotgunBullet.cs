using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 霰弹枪的子弹弹头的管理类
/// </summary>
public class ShotgunBullet : BulletBase {
   
    private RaycastHit hit;
    private Ray ray;

    public override void Init()
    {
        
    }

    public override void Shoot(Vector3 dir, int force, int damage, RaycastHit hit)
    {
        this.M_Damage = damage;
        //枪痕生成用的射线
        ray = new Ray(transform.position, dir);

        //给一个向前的力
        M_Rigidbody.AddForce(dir * force);
        //延时销毁自身
        StartCoroutine("DestroyBullet");
    }

    public override void CollisionEnter(Collision coll)
    {
        //碰到物体后停止运动
        M_Rigidbody.Sleep();
        //如果射中环境层
        if (coll.collider.GetComponent<BulletMark>() != null)
        {
            //返回射线事件 最后一个参数为只有该层触发射线检测
            if (Physics.Raycast(ray, out hit, 1000, 1 << 11)) { }
            //受击物体生成弹痕
            coll.collider.GetComponent<BulletMark>().CreateBulletMark(hit);
            coll.collider.GetComponent<BulletMark>().Hp -= M_Damage;
        }
        //如果射中AI层
        if (coll.collider.GetComponentInParent<AI>() != null)
        {
            //返回射线事件 最后一个参数为只有该层触发射线检测
            if (Physics.Raycast(ray, out hit, 1000, 1 << 12)) { }
            coll.collider.GetComponentInParent<AI>().PlayerEffect(hit);
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
        }
    }
}
