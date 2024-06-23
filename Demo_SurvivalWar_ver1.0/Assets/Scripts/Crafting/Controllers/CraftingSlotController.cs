using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 合成图谱物品格子
/// </summary>
public class CraftingSlotController : MonoBehaviour {

    private Transform m_Transform;
    private Image m_Image;
    private bool isOpen = false; //当前图谱块是否可以放置被拖拽的物品
    private int id = -1; //id和背包物品对应

    public int Id { get { return id; } }

    public bool IsOpen { get { return isOpen; } }

    void Awake()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        m_Image = m_Transform.Find("Item").GetComponent<Image>();
        //图片默认不显示
        m_Image.gameObject.SetActive(false);
    }

    public void Init(Sprite sprite ,int index)
    {
        //激活，更新合成图谱图片
        m_Image.gameObject.SetActive(true);
        m_Image.sprite = sprite;
        //关闭射线检测，让背包的物品可以拖拽到这个格子
        m_Image.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
        isOpen = true;
        id = index;
    }

    /// <summary>
    /// 重置图片
    /// </summary>
    public void ResetSprite()
    {
        m_Image.gameObject.SetActive(false);
    }

}
