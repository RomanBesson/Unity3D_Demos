using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 血屏显示脚本
/// </summary>
public class BloodScreenPanel : MonoBehaviour {

    private Image m_Image;

    private byte alpha = 0;
    public byte Alpha { get { return alpha; } }

	void Start () {
        m_Image = gameObject.GetComponent<Image>();
	}

    /// <summary>
    /// 改变血屏的不透明度.
    /// </summary>
    public void SetImageAlpha(bool isHited)
    {
        //扣血，血屏显示
        if (isHited && alpha < 255) alpha += 15;
        //回血，血屏消散
        if (!isHited && alpha > 0) alpha -= 15;
        Color32 color = new Color32(255, 255, 255, alpha);
        m_Image.color = color;
    }

}
