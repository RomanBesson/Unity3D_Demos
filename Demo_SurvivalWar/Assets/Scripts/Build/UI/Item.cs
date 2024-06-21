using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 环形工具栏下的项
/// </summary>
public class Item : MonoBehaviour {

    private Image icon_Image;                                           //该环形项的图片
    private Image item_BG;                                              //背景图片（用于显示是否选中）
    public List<GameObject> materialList = new List<GameObject>();     //该项下的子项材料对象

    void Awake () {
        icon_Image = transform.Find("Icon").GetComponent<Image>();
        item_BG = gameObject.GetComponent<Image>();
	}

    /// <summary>
    /// 初始化Item类.
    /// </summary>
    public void Init(string name, Quaternion quaternion, bool isIcon, Sprite sprite, bool isShow)
    {
        gameObject.name = name;
        transform.rotation = quaternion;
        //子图片旋转
        transform.Find("Icon").rotation = Quaternion.Euler(Vector3.zero);
        icon_Image.enabled = isIcon;
        icon_Image.sprite = sprite;
        item_BG.enabled = isShow;
    }

    /// <summary>
    /// 显示扇形背景.
    /// </summary>
    public void Show()
    {
        item_BG.enabled = true;
        ShowAndHide(true);
    }

    /// <summary>
    /// 隐藏扇形背景.
    /// </summary>
    public void Hide()
    {
        item_BG.enabled = false;
        ShowAndHide(false);
    }

    /// <summary>
    /// 往集合中添加具体的材料.
    /// </summary>
    public void MateiralListAdd(GameObject material)
    {
        materialList.Add(material);
    }

    /// <summary>
    /// 材料列表显示与隐藏.
    /// </summary>
    private void ShowAndHide(bool flag)
    {
        //没有材料子项
        if (materialList == null) return;

        //材料子项激活/隐藏
        for (int i = 0; i < materialList.Count; i++)
        {
            materialList[i].SetActive(flag);
        }
    }

}
