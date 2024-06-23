using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItemController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Image m_Image;                 //物品图片
    private Text m_Text;                   //物品数量
    private Image m_Bar;                   //物品血条.

    private int id = -1;                   //物品id和合成台对应
    private int num = -1;                  //物品数量
    private int bar = 0;                   //当前物品是否需要血条[耐久值]. 0:不需要, 1:需要.

    private bool isDrag = false;           //是否被拖拽
    private bool inInventory = true;       //当前物体是否在背包内.true:在背包内;false:在合成面板内.


    private RectTransform m_RectTransform;
    private Transform bac_Parent;          //物体拖拽过程中临时父物体.
    private Transform self_Parent;         //保存自身原来的父物体
    private CanvasGroup m_CanvasGroup;

#region 属性
    public int Id 
    { 
        get { return id; }
        set { id = value; }
    }

    /// <summary>
    /// 物品槽内图片
    /// </summary>
    public Image M_Image { get { return m_Image; } }

    public bool IsDrag { get { return isDrag; } }
    public int Num
    { 
        get { return num;}
        set
        {
            num = value;
            //设置显示数值
            m_Text.text = num.ToString();
        }
    }
    public bool InInventory
    {
        get { return inInventory; }
        set 
        { 
            inInventory = value;
            //初始化位置 缩放 大小
            ResetSpriteSize(m_RectTransform, 85, 85);
            m_RectTransform.localPosition = Vector3.zero;
 
        }
    }
#endregion
    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        //拖拽时右键拆分
        if (Input.GetMouseButtonDown(1) && isDrag)
        {
            BreakMaterials();
        }
    }

    /// <summary>
    /// 组件资源初始化
    /// </summary>
    private void Init()
    {
        m_Image = gameObject.GetComponent<Image>();
        m_Text = transform.Find("Num").GetComponent<Text>();
        m_RectTransform = gameObject.GetComponent<RectTransform>();
        //parent = m_RectTransform.parent.parent.parent.parent;
        bac_Parent = GameObject.Find("InventoryPanel").GetComponent<Transform>();
        m_CanvasGroup = gameObject.GetComponent<CanvasGroup>();
        gameObject.name = "InventoryItem";
        m_Bar = m_RectTransform.Find("Bar").GetComponent<Image>();
    }

    /// <summary>
    /// 初始化背包物品信息
    /// </summary>
    /// <param name="name">物品图片名称</param>
    /// <param name="num">物品个数</param>
    public void InitItem(string name, int num, int index, int bar, string barValue)
    {
        m_Image.sprite = Resources.Load<Sprite>("Item/" + name);
        m_Text.text = num.ToString();
        this.num = num;
        id = index;
        this.bar = bar;

        //设置耐久
        if (bar == 1) 
        m_Bar.fillAmount = float.Parse(barValue);
        BarOrNum();
    }


    /// <summary>
    /// 更新血条值.
    /// </summary>
    public void UpdateUI(float value)
    {
        //耐久血条归零，销毁自身，边框恢复成默认状态
        if (value <= 0)
        {
            gameObject.GetComponent<Transform>().parent.GetComponent<ToolBarSlotController>().Normal();
            GameObject.Destroy(gameObject);
        }
        m_Bar.fillAmount = value;
    }

    /// <summary>
    /// 物品剩余耐久值
    /// </summary>
    /// <returns></returns>
    public string GetBarValue()
    {
        return m_Bar.fillAmount.ToString();
    }

    /// <summary>
    /// 获取此物品槽的图片名称
    /// </summary>
    /// <returns></returns>
    public string GetImageName()
    {
        if (m_Image.sprite != null)
        {
            return m_Image.sprite.name.ToString();
        }
        return "";
    }

    /// <summary>
    /// 获取物品血条显示状态
    /// </summary>
    /// <returns></returns>
    public int GetBar()
    {
        if (m_Bar.gameObject.activeSelf)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    #region 拖拽相关
    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        //保存原来的父物体
        self_Parent = m_RectTransform.parent;
        //拖拽开始，把他的父类改为最外层，防止遮挡
        m_RectTransform.SetParent(bac_Parent);
        //防止射线检测
        m_CanvasGroup.blocksRaycasts = false;
        //改变被拖拽状态
        isDrag = true;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        //拖拽三件套
        Vector3 pos;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(m_RectTransform, eventData.position, eventData.enterEventCamera, out pos);
        m_RectTransform.position = pos;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        //检测到的物体
        GameObject targetObject = eventData.pointerEnter;

        //如果放到了UI上
        if(targetObject != null)
        {
            #region 放到了背包空物品槽上
            //放到了背包空物品槽上
            if (targetObject.tag == "InventorySlot")
            {
                InInventory = true;
                ResetSpriteSize(m_RectTransform, 85, 85);
                //放到对应格子上
                m_RectTransform.SetParent(targetObject.transform);
            }
            #endregion

            #region 放到了已经有物品的背包格上
            //放到了已经有物品的背包格上
            else if (targetObject.tag == "InventoryItem")
            {
                InventoryItemController targetScr = targetObject.GetComponent<InventoryItemController>();
                //如果两者都在背包里
                if (InInventory && targetScr.InInventory)
                {
                    //相同物品合并数量
                    if(targetScr.Id == Id)
                    {
                        MergeMaterials(targetScr);
                    }
                    else
                    //不同物品
                    {
                        //互换位置
                        Transform tarTransfrom = targetObject.GetComponent<Transform>();
                        m_RectTransform.SetParent(tarTransfrom.parent);
                        tarTransfrom.SetParent(self_Parent);
                        tarTransfrom.localPosition = Vector3.zero;
                    }
                }
                //不是都处于背包
                else
                {
                    //目标物体在背包，拖拽物体不在
                    if(targetScr.Id == Id && targetScr.InInventory)
                    {
                        MergeMaterials(targetScr);
                    }
                    //放回原位
                    m_RectTransform.SetParent(self_Parent);
                }
            }
            #endregion

            #region 放到了合成台上
            //放到合成台上
            else if (targetObject.tag == "CraftingSlot")
            {

                //目标格子的临时脚本
                CraftingSlotController tempSc = targetObject.GetComponent<CraftingSlotController>();
                //如果允许放置
                if (tempSc.IsOpen)
                {
                    //如果是对应物品,放到对应格子上
                    if (tempSc.Id == Id) 
                    {
                        //已在合成台上
                        InInventory = false;
                        m_RectTransform.SetParent(targetObject.transform);
                        ResetSpriteSize(m_RectTransform, 70, 62);
                        //调用父C层的传递方法，把这个对象传递过去
                        InventoryPanelController.Instance.SendDargMaterilasItem(gameObject);
                    } 
                    //否则放回原位
                    else m_RectTransform.SetParent(self_Parent);
                }
                else
                {
                    //放回原位
                    m_RectTransform.SetParent(self_Parent);
                }
               
            }
            #endregion

            #region 放到了别的UI元素上
            //放到了别的UI元素上
            else
            {
                //放回原位
                m_RectTransform.SetParent(self_Parent);
            }
            #endregion
        }
        //没有放到UI上
        else
        {
            //放回原位
            m_RectTransform.SetParent(self_Parent);
        }
        //恢复射线检测，否则无法拖拽
        m_CanvasGroup.blocksRaycasts = true;
        //在新格子上安顿好位置
        m_RectTransform.localPosition = Vector3.zero;
        //改变被拖拽状态
        isDrag = false;
    }
    #endregion

    /// <summary>
    /// 控制显示耐久还是显示数量
    /// </summary>
    private void BarOrNum()
    {
        if (bar == 0)
        {
            m_Bar.gameObject.SetActive(false);
        }
        else
        {
            m_Text.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 调整图片尺寸
    /// </summary>
    /// <param name="rectTransform">对象的位置</param>
    /// <param name="width">调整的宽度</param>
    /// <param name="height">调整的高度</param>
    private void ResetSpriteSize(RectTransform rectTransform ,float width, float height)
    {
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
    }

    
    /// <summary>
    /// 拆分为一半数量的相同物品
    /// </summary>
    private void BreakMaterials()
    {
        GameObject cloneGameobject = Instantiate<GameObject>(gameObject);
        RectTransform cloneTransform = cloneGameobject.GetComponent<RectTransform>();

        //调整位置
        cloneTransform.SetParent(self_Parent);
        cloneTransform.localPosition = Vector3.zero;
        cloneTransform.localScale = Vector3.one;

        //设置数量
        int temp = num / 2;
        cloneGameobject.GetComponent<InventoryItemController>().Num = temp;
        Num -= temp;

        //恢复射线检查
        cloneGameobject.GetComponent<CanvasGroup>().blocksRaycasts = true;

        //重新设置id
        cloneGameobject.GetComponent<InventoryItemController>().Id = this.Id;

    }

    /// <summary>
    /// 物品合并
    /// </summary>
    /// <param name="targetScr">目标位置物品的脚本</param>
    private void MergeMaterials(InventoryItemController targetScr)
    {
        targetScr.Num += Num;
        Destroy(this.gameObject);
    }

}
