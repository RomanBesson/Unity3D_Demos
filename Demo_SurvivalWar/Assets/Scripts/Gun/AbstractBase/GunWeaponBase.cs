using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 枪械的抽象类
/// </summary>
public abstract class GunWeaponBase : GunControllerBase {

    protected override void Start()
    {
        base.Start();
        LoadEffectAsset();
    }

    protected override void MouseButtonLeftDown()
    {
        base.MouseButtonLeftDown();
        PlayEffect();
    }
    /// <summary>
    /// 加载枪火特效
    /// </summary>
    protected abstract void LoadEffectAsset();
    /// <summary>
    /// 播放特效
    /// </summary>
    protected abstract void PlayEffect();
}
