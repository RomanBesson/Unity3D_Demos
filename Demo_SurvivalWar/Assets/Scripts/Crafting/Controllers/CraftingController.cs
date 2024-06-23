using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 右面功能区控制脚本
/// </summary>
public class CraftingController : MonoBehaviour {

    private Transform m_Transform;
    private Image m_Image;//展示要合成的物品图片
    private Button m_Craft_Btn;//单个合成按钮
    private Button m_CraftAll_Btn;//全部合成按钮
    private Transform bg_Transform; //合成的物品的父类

    private int tempId;//合成台当前要合成的物品id
    private string tempSpriteName;//当前要合成的物品图片名称

    private GameObject prefab_InventoryItem; //合成的物体的预制体（在父C层赋值）
    public GameObject Prefab_InventoryItem { set { prefab_InventoryItem = value; } }

    void Awake()
    {
        Init();
        InitButton();
    }

    /// <summary>
    /// 初始化项目
    /// </summary>
    public void Init()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        m_Image = m_Transform.Find("GoodItem/ItemImage").GetComponent<Image>();
        m_Craft_Btn = m_Transform.Find("Craft").GetComponent<Button>();
        m_CraftAll_Btn = m_Transform.Find("CraftAll").GetComponent<Button>();
        bg_Transform = m_Transform.Find("GoodItem").GetComponent<Transform>();

        m_Image.gameObject.SetActive(false);
        m_Craft_Btn.onClick.AddListener(CraftingItem);
    }

    /// <summary>
    /// 初始化图片
    /// </summary>
    /// <param name="fileName"></param>
    public void InitImage(int id, string fileName)
    {
        m_Image.gameObject.SetActive(true);
        m_Image.sprite = Resources.Load<Sprite>("Item/" + fileName);
        tempId = id;
        tempSpriteName = fileName;
    }

    /// <summary>
    /// 初始化按钮
    /// </summary>
    private void InitButton()
    {
        m_CraftAll_Btn.interactable = false;
        m_CraftAll_Btn.transform.Find("Text").GetComponent<Text>().color = Color.black;

        m_Craft_Btn.interactable = false;
        m_Craft_Btn.transform.Find("Text").GetComponent<Text>().color = Color.black;
    }

    /// <summary>
    /// 设置按钮为可使用
    /// </summary>
    public void ActiveButton()
    {
        m_Craft_Btn.interactable = true;
        m_Craft_Btn.transform.Find("Text").GetComponent<Text>().color = Color.white;
    }

    /// <summary>
    /// 物品合成.
    /// </summary>
    private void CraftingItem()
    {
        GameObject item = GameObject.Instantiate<GameObject>(prefab_InventoryItem, bg_Transform);
        //调整尺寸
        item.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 110);
        item.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 110);
        //设置信息
        item.GetComponent<InventoryItemController>().InitItem(tempSpriteName, 1, tempId, 1, "1");
        InitButton();
        //调用父类方法对消耗的材料进行处理
        SendMessageUpwards("CraftingOK");
    }
}
