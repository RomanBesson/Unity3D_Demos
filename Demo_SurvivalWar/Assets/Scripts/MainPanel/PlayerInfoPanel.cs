using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 血条体力条页面显示控制脚本
/// </summary>
public class PlayerInfoPanel : MonoBehaviour {

    private Image hp_Bar;                  //血条
    private Image vit_Bar;                 //体力条

	void Start () {
        hp_Bar = transform.Find("HP/Bar").GetComponent<Image>();
        vit_Bar = transform.Find("VIT/Bar").GetComponent<Image>();
	}

    /// <summary>
    /// 修改血量
    /// </summary>
    /// <param name="hp"></param>
    public void SetHP(int hp)
    {
        hp_Bar.fillAmount = hp * 0.001f;
    }

    /// <summary>
    /// 修改体力
    /// </summary>
    /// <param name="vit"></param>
    public void SetVIT(int vit)
    {
        vit_Bar.fillAmount = vit * 0.01f;
    }
}
