using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 合成模块单个选项卡控制脚本.
/// </summary>
public class CraftingTabItemController : MonoBehaviour
{
    private Button m_Button;
    private GameObject m_ButtonBG; //按钮背景
    private Image m_Icon;

    private int index = -1; //当前标签序号

    private void Awake()
    {
        m_Button = gameObject.GetComponent<Button>();
        m_ButtonBG = transform.Find("Center_BG").gameObject;
        m_Icon = transform.Find("Icon").GetComponent<Image>();
        m_Button.onClick.AddListener(ButtonOnClick);
    }

    /// <summary>
    /// 初始化Item.
    /// </summary>
    public void InitItem(int index, Sprite sprite)
    {
        this.index = index;
        gameObject.name = "标签" + index + 1; //给生成的起个名字（可有可无）
        m_Icon.sprite = sprite;
    }


    /// <summary>
    /// 选项卡默认状态.
    /// </summary>
    public void NoramlTab()
    {
        if (!m_ButtonBG.activeSelf)
        {
            m_ButtonBG.SetActive(true);
        }
    }

    /// <summary>
    /// 选项卡激活状态.
    /// </summary>
    public void ActiveTab()
    {
        m_ButtonBG.SetActive(false);
    }

    private void ButtonOnClick()
    {
        SendMessageUpwards("ResetTabsAndContents", index); //重置标签
    }
}
