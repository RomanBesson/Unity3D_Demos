using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 霰弹枪的子弹弹头的管理类
/// </summary>
public class ShotgunBullet : MonoBehaviour {

    private Rigidbody m_Rigidbody;

    private RaycastHit hit;
    private int damage;         //单个弹头伤害

    void Awake () {
        m_Rigidbody = gameObject.GetComponent<Rigidbody>();
	}

    /// <summary>
    /// 发射弹头
    /// </summary>
    /// <param name="dir"></param>
    /// <param name="force"></param>
    /// <param name="damage"></param>
    public void Shoot(Vector3 dir, int force, int damage)
    {
        this.damage = damage;
        //枪痕生成用的射线
        Ray ray = new Ray(transform.position, dir);
        //返回射线事件 最后一个参数为只有该层触发射线检测
        if (Physics.Raycast(ray, out hit,1000, 1 << 11)) { }
        //给一个向前的力
        m_Rigidbody.AddForce(dir * force);
        //延时销毁自身
        StartCoroutine(DestroyBullet());
    }

    void OnCollisionEnter(Collision coll)
    {
        //碰到物体后停止运动
        m_Rigidbody.Sleep();
        if (coll.collider.GetComponent<BulletMark>() != null)
        {
            //受击物体生成弹痕
            coll.collider.GetComponent<BulletMark>().CreateBulletMark(hit);
            coll.collider.GetComponent<BulletMark>().Hp -= damage;
        }
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
}
