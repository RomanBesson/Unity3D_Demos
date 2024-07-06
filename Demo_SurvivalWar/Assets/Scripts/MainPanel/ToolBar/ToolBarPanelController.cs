using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 工具栏页面管理脚本
/// </summary>
public class ToolBarPanelController : MonoBehaviour {

    public static ToolBarPanelController Instance;

    private ToolBarPanelView m_ToolBarPanelView;
    private ToolBarPanelModel m_ToolBarPanelModel;

    /// <summary>
    /// 工具栏现在被激活的物品栏子
    /// </summary>
    private GameObject currentActive = null;                        
    private int currentKeyCode = -1;                                //存储当前的按键.
    private List<GameObject> slotList;                              //工具栏的物品槽
    private GameObject currentActiveModel = null;                   //存储当前激活的角色模型.

    /// <summary>
    /// 工具栏对应的物品字典 <物品栏子，物品栏子对应武器物品>
    /// </summary>
    private Dictionary<GameObject, GameObject> toolBarDic = null;   

    public GameObject CurrentActiveModel { get { return currentActiveModel; } }

    private void Awake()
    {
        Instance = this;
    }

    void Start () {
        Init();
        CreateAllSlot();
	}

    void OnDisable()
    {
        //不激活的时候保存数据
        m_ToolBarPanelModel.ObjectToJson(slotList, "ToolBarJsonData.txt");
    }

    private void Init()
    {
        m_ToolBarPanelView = gameObject.GetComponent<ToolBarPanelView>();
        m_ToolBarPanelModel = gameObject.GetComponent<ToolBarPanelModel>();
        slotList = new List<GameObject>();
        toolBarDic = new Dictionary<GameObject, GameObject>();
    }

    /// <summary>
    /// 生成工具栏所有物品槽.
    /// </summary>
    private void CreateAllSlot()
    {
        //向数据层获取物品数据
        List<InventoryItem> inventoryItems = m_ToolBarPanelModel.GetJsonList("ToolBarJsonData");

        for (int i = 0; i < 8; i++)
        {
            GameObject slot = GameObject.Instantiate<GameObject>(m_ToolBarPanelView.Prefab_ToolBarSlot, m_ToolBarPanelView.Grid_Transform);
            //初始化信息
            slot.GetComponent<ToolBarSlotController>().InitInfo(m_ToolBarPanelView.Prefab_ToolBarSlot.name + i, i + 1);

            //导入工具栏格子内的物品
            if (inventoryItems[i].ItemName != "")
            {
                GameObject temp = GameObject.Instantiate(m_ToolBarPanelView.Prefab_Item, slot.transform);
                temp.GetComponent<InventoryItemController>().InitItem(inventoryItems[i].ItemName, inventoryItems[i].ItemNum, inventoryItems[i].ItemId, inventoryItems[i].ItemBar, inventoryItems[i].BarValue);
            }

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
        FindInventoryItem();
    }

    /// <summary>
    /// 按键切切换工具栏的工具
    /// </summary>
    /// <param name="keyIndex"></param>
    public void SaveActiveSlotByKey(int keyIndex)
    {
        //如果对应格子里没物品，不切换（临时）
        if (slotList[keyIndex].GetComponent<Transform>().Find("InventoryItem") == null)
        {
            return;
        }
        //调用点击事件
        slotList[keyIndex].GetComponent<ToolBarSlotController>().SlotClick();
    }

    /// <summary>
    /// 删除当前持有武器模型及其字段里的对应元素关系
    /// </summary>
    public void DeleteDicItem()
    {
        //清空武器模型
        currentActiveModel = null;
        GameObject temp = null;
        toolBarDic.TryGetValue(currentActive,out temp);
        GameObject.Destroy(temp);

        //清空字典对应关系
        toolBarDic.Remove(currentActive);
    }
    /// <summary>
    /// 清除字典里的所有对应关系
    /// </summary>
    public void DeleteAllDicItem()
    {
        //清空武器模型
        currentActiveModel = null;
        toolBarDic.Clear();
    }

    /// <summary>
    /// 调用枪械工厂类生成对应物品
    /// </summary>
    private void FindInventoryItem()
    {
        Transform m_temp = currentActive.GetComponent<Transform>().Find("InventoryItem");
        StartCoroutine(CallGunFactory(m_temp));
    }

    /// <summary>
    /// 延时生成/切换枪械
    /// </summary>
    /// <param name="m_temp">工具栏物品对象</param>
    /// <returns></returns>
    private IEnumerator CallGunFactory(Transform m_temp)
    {
        //如果当前已经持有物品
        if (currentActiveModel != null)
        {
            //如果不是建筑图纸
            if (currentActiveModel.tag != "Build" && currentActiveModel.tag != "Hand")
            {
                //执行放下武器动作
                currentActiveModel.GetComponent<GunControllerBase>().Holster();
                yield return new WaitForSeconds(1);
            }

            //如果是手持武器
            if (currentActiveModel.tag == "Hand")
            {
                currentActiveModel.GetComponent<StoneHatchet>().Holster();
                yield return new WaitForSeconds(1);
            }

            currentActiveModel.SetActive(false);
        }
        //当前工具栏下有物品
        if (m_temp != null)
        {
            //查看字典里是否存在了对应物品
            GameObject temp = null;
            toolBarDic.TryGetValue(currentActive, out temp);

            //字典里没有现在的物品
            if (temp == null)
            {
                //生成物品，添加进去
                temp = GunFactory.Instance.CreateGun(m_temp.GetComponent<Image>().sprite.name, m_temp.gameObject);
                toolBarDic.Add(currentActive, temp);
            }
            //字典里有相应物品
            else
            {
                //工具栏被选中高亮才显示工具栏物品，
                if (currentActive.GetComponent<ToolBarSlotController>().SelfState)
                    //显示出来
                    temp.SetActive(true);
            }
            //设置当前物品
            currentActiveModel = temp;
        }
        //如果工具栏下没物品
        else currentActiveModel = null;
    }

}
