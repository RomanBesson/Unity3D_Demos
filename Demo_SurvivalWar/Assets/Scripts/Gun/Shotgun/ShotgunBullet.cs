using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 霰弹枪的子弹弹头的管理类
/// </summary>
public class ShotgunBullet : BulletBase {
   
    private RaycastHit hit;

    public override void Init()
    {
        
    }

    public override void Shoot(Vector3 dir, int force, int damage)
    {
        this.M_Damage = damage;
        //枪痕生成用的射线
        Ray ray = new Ray(transform.position, dir);
        //返回射线事件 最后一个参数为只有该层触发射线检测
        if (Physics.Raycast(ray, out hit, 1000, 1 << 11)) { }
        //给一个向前的力
        M_Rigidbody.AddForce(dir * force);
        //延时销毁自身
        StartCoroutine("DestroyBullet");
    }

    public override void CollisionEnter(Collision coll)
    {
        //碰到物体后停止运动
        M_Rigidbody.Sleep();
        if (coll.collider.GetComponent<BulletMark>() != null)
        {
            //受击物体生成弹痕
            coll.collider.GetComponent<BulletMark>().CreateBulletMark(hit);
            coll.collider.GetComponent<BulletMark>().Hp -= M_Damage;
        }
    }
}
