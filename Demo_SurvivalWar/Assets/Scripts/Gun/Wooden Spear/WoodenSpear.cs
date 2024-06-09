using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 长矛逻辑层（c层）
/// </summary>
public class WoodenSpear : GunControllerBase {

    private WoodenSpearView m_WoodenSpearView;

    public override void Init()
    {
        m_WoodenSpearView = (WoodenSpearView)M_GunViewBase;
        //默认左键不射击
        CanShoot(0);
    }

    public override void LoadAudioAsset()
    {
        Audio = Resources.Load<AudioClip>("Audios/Gun/Arrow Release");
    }

    public override void LoadEffectAsset()
    {
        //木矛没有特效资源.
    }

    public override void Shoot()
    {
        //生成长矛
        GameObject spear = GameObject.Instantiate<GameObject>(m_WoodenSpearView.M_Spear, m_WoodenSpearView.M_GunPoint.position, m_WoodenSpearView.M_GunPoint.rotation);
        spear.GetComponent<Arrow>().Shoot(m_WoodenSpearView.M_GunPoint.forward, 2000, Damage);
    }

    public override void PlayEffect()
    {
        //木矛没有特效资源.
    }
}
