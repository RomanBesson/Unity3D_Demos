using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 长矛逻辑层（c层）
/// </summary>
public class WoodenSpear : ThrowWeaponBase {

    private WoodenSpearView m_WoodenSpearView;

    protected override void Init()
    {
        m_WoodenSpearView = (WoodenSpearView)M_GunViewBase;
        //默认左键不射击
        CanShoot(0);
    }

    protected override void LoadAudioAsset()
    {
        Audio = Resources.Load<AudioClip>("Audios/Gun/Arrow Release");
    }

    protected override void Shoot()
    {
        //生成长矛
        GameObject spear = GameObject.Instantiate<GameObject>(m_WoodenSpearView.M_Spear, m_WoodenSpearView.M_GunPoint.position, m_WoodenSpearView.M_GunPoint.rotation);
        spear.GetComponent<Arrow>().Shoot(m_WoodenSpearView.M_GunPoint.forward, 2000, Damage, Hit);
        //消耗耐久
        Durable--;
    }

}
