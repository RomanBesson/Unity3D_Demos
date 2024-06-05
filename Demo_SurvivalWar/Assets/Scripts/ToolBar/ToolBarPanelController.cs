using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolBarPanelController : MonoBehaviour {

    public static ToolBarPanelController Instance;

    private ToolBarPanelView m_ToolBarPanelView;
    private ToolBarPanelModel m_ToolBarPanelModel;

    private GameObject currentActive = null; //物品槽现在被激活的物品

    private List<GameObject> slotList; //工具栏的物品槽

    private void Awake()
    {
        Instance = this;
    }

    void Start () {
        Init();
        CreateAllSlot();
	}
	
    private void Init()
    {
        m_ToolBarPanelView = gameObject.GetComponent<ToolBarPanelView>();
        m_ToolBarPanelModel = gameObject.GetComponent<ToolBarPanelModel>();
        slotList = new List<GameObject>();
    }

    /// <summary>
    /// 生成工具栏所有物品槽.
    /// </summary>
    private void CreateAllSlot()
    {
        for (int i = 0; i < 8; i++)
        {
            GameObject slot = GameObject.Instantiate<GameObject>(m_ToolBarPanelView.Prefab_ToolBarSlot, m_ToolBarPanelView.Grid_Transform);
            //初始化信息
            slot.GetComponent<ToolBarSlotController>().InitInfo(m_ToolBarPanelView.Prefab_ToolBarSlot.name + i, i + 1);
            slotList.Add(slot);
        }
    }

    /// <summary>
    /// 存储当前激活的物品槽以及保持只有一个物品被激活
    /// </summary>
    private void SaveActiveSlot(GameObject activeSlot)
    {
        if(currentActive != null && currentActive != activeSlot)
        {
            //熄灭当前激活
            currentActive.GetComponent<ToolBarSlotController>().Normal();
        }
        //保存现在激活的工具栏位
        currentActive = activeSlot;
    }

    /// <summary>
    /// 按键切切换工具栏的工具
    /// </summary>
    /// <param name="keyIndex"></param>
    public void SaveActiveSlotByKey(int keyIndex)
    {
        //调用点击事件
        slotList[keyIndex].GetComponent<ToolBarSlotController>().SlotClick();
    }

}
