using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class AssaultRifle : GunWeaponBase
{

    private AssaultRifleView m_AssaultRifleView;

    private ObjectPool[] pools;           //枪口特效和弹壳的对象池 0：枪口特效 1：对象池

    /// <summary>
    /// 播放特效.
    /// </summary>
    protected override void PlayEffect()
    {
        GunEffect();
        ShellEffect();
    }

    /// <summary>
    /// 生成枪口特效
    /// </summary>
    private void GunEffect()
    {
        //加入对象池
        GameObject tempEffect = null;
        //如果池子不为空
        if (pools[0].Data())
        {
            tempEffect = pools[0].GetObject();
            tempEffect.transform.position = m_AssaultRifleView.M_GunPoint.position;
        }
        //池子为空
        else
        {
            //生成枪口火花特效
            tempEffect = GameObject.Instantiate<GameObject>(m_AssaultRifleView.M_Effect, m_AssaultRifleView.M_GunPoint.position, Quaternion.identity, m_AssaultRifleView.M_EffectParent);
        }
        //播放特效
        tempEffect.GetComponent<ParticleSystem>().Play();
        //延迟加入对象池
        StartCoroutine(Delay(pools[0], tempEffect, 1));
    }

    /// <summary>
    /// 生成弹壳
    /// </summary>
    private void ShellEffect()
    {
        //加入对象池
        GameObject tempShell = null;
        //如果池子不为空
        if (pools[1].Data())
        {
            tempShell = pools[1].GetObject();
            //开启弹壳的Kinematic属性，让他的位置可以设置
            tempShell.GetComponent<Rigidbody>().isKinematic = true;
            tempShell.transform.position = m_AssaultRifleView.M_EffectPos.position;
            tempShell.GetComponent<Rigidbody>().isKinematic = false;
        }
        //池子为空
        else
        {
            //生成弹出弹壳
            tempShell = GameObject.Instantiate<GameObject>(m_AssaultRifleView.M_Shell, m_AssaultRifleView.M_EffectPos.position, Quaternion.identity, m_AssaultRifleView.M_ShellParent);
        }
        //弹壳溅射效果
        tempShell.GetComponent<Rigidbody>().AddForce(m_AssaultRifleView.M_EffectPos.up * 50);
        //延迟添加进对象池
        StartCoroutine(Delay(pools[1], tempShell, 3));
    }

    /// <summary>
    /// 射击.
    /// </summary>
    protected override void Shoot()
    {
        if (Hit.point != Vector3.zero)
        {
            //生成子弹
            // GameObject.Instantiate(m_AssaultRifleView.M_Bullet, raycastHit.point, Quaternion.identity);

            //消耗耐久
            Durable--;

           //生成弹痕
            if (Hit.collider.GetComponent<BulletMark>() != null) 
            {
                Hit.collider.GetComponent<BulletMark>().CreateBulletMark(Hit);
                Hit.collider.GetComponent<BulletMark>().Hp -= Damage;
            }
        }
    }



    protected override void LoadAudioAsset()
    {
        Audio = Resources.Load<AudioClip>("Audios/Gun/AssaultRifle_Fire");
    }

    protected override void LoadEffectAsset()
    {
        Effect = Resources.Load<GameObject>("Effects/Gun/AssaultRifle_GunPoint_Effect");
    }

    protected override void Init()
    {
        m_AssaultRifleView = (AssaultRifleView)M_GunViewBase;
        pools = gameObject.GetComponents<ObjectPool>();
    }
}
