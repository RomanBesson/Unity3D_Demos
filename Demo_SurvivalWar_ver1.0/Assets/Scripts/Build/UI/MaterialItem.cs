using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 环形UI项子项材料的脚本
/// </summary>
public class MaterialItem : MonoBehaviour {

    private Image icon_Image;

	void Start () {
        icon_Image = transform.Find("Icon").GetComponent<Image>();
	}
	
    public void Height()
    {
        icon_Image.color = Color.red;
    }

    public void Normal()
    {
        icon_Image.color = Color.white;
    }
}
