using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 工具栏物品控制脚本
/// </summary>
public class ToolBarSlotController : MonoBehaviour
{
    private Button m_Button; 
    private Image m_Image; 
    private Text m_Text;

    private bool selfState = false;     //按钮处于的状态 是否被激活

    private void Awake()
    {
        Init();
    }

    /// <summary>
    /// 初始化组件
    /// </summary>
    private void Init()
    {
        m_Button = gameObject.GetComponent<Button>();
        m_Image = gameObject.GetComponent<Image>();

        m_Text = transform.Find("Key").GetComponent<Text>();
        //添加点击事件
        m_Button.onClick.AddListener(SlotClick);
    }

    /// <summary>
    /// 初始化具体参数
    /// </summary>
    public void InitInfo(string name, int keyNum)
    {
        m_Text.text = keyNum.ToString();
        gameObject.name = name;
    }

    /// <summary>
    /// 按钮的点击事件，切换激活和未激活状态
    /// </summary>
    public void SlotClick()
    {
        //如果按钮处于激活，熄灭它
        if (selfState) Normal();
        //如果按钮处于未激活，激活他
        else Active();
        //调用父类保持唯一激活的方法
        SendMessageUpwards("SaveActiveSlot", gameObject);
    }

    /// <summary>
    /// 变为未激活状态
    /// </summary>
    public void Normal()
    {
        m_Image.color = Color.white;
        selfState = false;
    }

    /// <summary>
    /// 变为激活状态
    /// </summary>
    private void Active()
    {
        m_Image.color = Color.red;
        selfState = true;
    }
}
