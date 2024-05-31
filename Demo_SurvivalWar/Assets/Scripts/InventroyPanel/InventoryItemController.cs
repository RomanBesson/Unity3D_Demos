using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    private Image m_Image; //物品图片
    private Text m_Text; //物品数量

    private void Awake()
    {
        m_Image = gameObject.GetComponent<Image>();
        m_Text = transform.Find("Num").GetComponent<Text>();
    }

    /// <summary>
    /// 初始化背包物品信息
    /// </summary>
    /// <param name="name">物品图片名称</param>
    /// <param name="num">物品个数</param>
    public void InitInitItem(string name, int num)
    {
        m_Image.sprite = Resources.Load<Sprite>("Item/" + name);
        m_Text.text = num.ToString();
    }
}
