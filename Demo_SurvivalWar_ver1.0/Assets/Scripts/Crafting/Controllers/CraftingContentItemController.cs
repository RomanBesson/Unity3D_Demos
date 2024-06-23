using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 左标签页对应内容页的子项
/// </summary>
public class CraftingContentItemController : MonoBehaviour
{
    private Button m_Button;
    private Text m_Text;
    private GameObject m_BG;

    //合成物品本身的id和名字和实体类对应
    private int id = -1;
    private string name;

    public int Id { get { return id; } }

    void Awake()
    {
        m_Button = gameObject.GetComponent<Button>();
        m_Text = transform.Find("Text").GetComponent<Text>();
        m_BG = transform.Find("BG").gameObject;

        m_BG.SetActive(false);
        m_Button.onClick.AddListener(ItemButtonClick);
    }


    public void Init(CraftingContentItem item)
    {
        id = item.ItemID;
        name = item.ItemName;
        m_Text.text = "  " + name;
    }

    /// <summary>
    /// 默认状态.
    /// </summary>
    public void NormalItem()
    {
        m_BG.SetActive(false);
    }

    /// <summary>
    /// 激活状态.
    /// </summary>
    public void ActiveItem()
    {
        m_BG.SetActive(true);
    }

    private void ItemButtonClick()
    {
        SendMessageUpwards("ResetItemState", this);
    }

}
