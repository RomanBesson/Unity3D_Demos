using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : GunWeaponBase
{
    private ShotgunView m_ShotgunView;

    protected override void Init()
    {
        m_ShotgunView = (ShotgunView)M_GunViewBase;
    }

    protected override void LoadAudioAsset()
    {
        Audio = Resources.Load<AudioClip>("Audios/Gun/Shotgun_Fire");
    }

    protected override void LoadEffectAsset()
    {
        Effect = Resources.Load<GameObject>("Effects/Gun/Shotgun_GunPoint_Effect");
    }

    protected override void Shoot()
    {
        StartCoroutine(CreateBullet());
        //消耗耐久
        Durable--;
    }

    protected override void PlayEffect()
    {
        //枪口特效
        GameObject tempEffect = GameObject.Instantiate<GameObject>(Effect, M_GunViewBase.M_GunPoint.position, Quaternion.identity);
        tempEffect.GetComponent<ParticleSystem>().Play();
        StartCoroutine(DelayDestroy(tempEffect, 2));

        //弹壳弹出特效.
        GameObject tempShell = GameObject.Instantiate<GameObject>(m_ShotgunView.M_Shell, m_ShotgunView.M_EffectPos.position, Quaternion.identity);
        tempShell.GetComponent<Rigidbody>().AddForce(m_ShotgunView.M_EffectPos.up * 70);
        StartCoroutine(DelayDestroy(tempShell, 5));
    }

    /// <summary>
    /// 播放换弹特效
    /// </summary>
    private void PlayEffectAudio()
    {
        AudioSource.PlayClipAtPoint(m_ShotgunView.M_EffectAudio, m_ShotgunView.M_EffectPos.position);
    }

    /// <summary>
    /// 延时销毁物体
    /// </summary>
    /// <param name="go"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    IEnumerator DelayDestroy(GameObject go, float time)
    {
        yield return new WaitForSeconds(time);
        GameObject.Destroy(go);
    }

    /// <summary>
    /// 生成不同方向的五发散弹枪子弹
    /// </summary>
    /// <returns></returns>
    private IEnumerator CreateBullet()
    {
        for(int i = 0; i < 5; i++)
        {
            //每发方向上的随机偏移
            Vector3 offset = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0);
            //每0.03s生成一颗子弹
            GameObject tempBullet = GameObject.Instantiate<GameObject>(m_ShotgunView.M_Bullet, m_ShotgunView.M_GunPoint.position, Quaternion.identity);
            tempBullet.GetComponent<ShotgunBullet>().Shoot(m_ShotgunView.M_GunPoint.forward + offset, 3000, Damage/5);
            yield return new WaitForSeconds(0.03f);
        }
    }
}
