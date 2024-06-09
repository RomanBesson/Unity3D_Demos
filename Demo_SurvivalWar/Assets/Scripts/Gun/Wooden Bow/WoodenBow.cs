using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 弓箭的c层
/// </summary>
public class WoodenBow : GunControllerBase
{
    //资源加载对象(v层）
    private WoodenBowView m_WoodenBowView;

    public override void Init()
    {
        //向下转型
        m_WoodenBowView = (WoodenBowView)M_GunViewBase;
        //常态下不允许射箭
        CanShoot(0);
    }

    public override void LoadAudioAsset()
    {
        Audio = Resources.Load<AudioClip>("Audios/Gun/Arrow Release");
    }

    public override void LoadEffectAsset()
    {
        //弓箭没有特效.
    }

    public override void Shoot()
    {
        //生成箭
        GameObject arrow = GameObject.Instantiate<GameObject>(m_WoodenBowView.M_Arrow, m_WoodenBowView.M_GunPoint.position, m_WoodenBowView.M_GunPoint.rotation);
        arrow.GetComponent<Arrow>().Shoot(m_WoodenBowView.M_GunPoint.forward, 1000, Damage);
    }

    public override void PlayEffect()
    {
        //弓箭没有特效.
    }
}
