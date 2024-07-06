using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 游戏页面的逻辑控制脚本
/// </summary>
public class GamePanelCotroller : MonoBehaviour
{
    #region 字段
    public static GamePanelCotroller Instace;

    /// <summary>
    /// 游戏场景视图层_脚本对象
    /// </summary>
    private GamePanelView m_gamePanelView;
    /// <summary>
    /// 游戏场景数据层_脚本对象
    /// </summary>
    private GamePanelModel m_gamePanelModel;
    /// <summary>
    /// 上一个点击的方块对象
    /// </summary>
    private GameObject lastItem = null;

    /// <summary>
    /// 方块矩阵行数
    /// </summary>
    private int horizontal = 0;
    /// <summary>
    /// 方块矩阵列数
    /// </summary>
    private int vertical = 0;
    /// <summary>
    /// 记录方块矩阵的元素个数
    /// </summary>
    private int sum = 0;
    /// <summary>
    /// sum的初始值
    /// </summary>
    private int maxSum = 0;
    /// <summary>
    /// 单次得分
    /// </summary>
    private int score = 0;
    /// <summary>
    /// 总得分
    /// </summary>
    private int scores = 0;


    /// <summary>
    /// 方块背面数值的集合
    /// </summary>
    private int[] nums = null;
    /// <summary>
    /// 方块游戏对象集合
    /// </summary>
    private List<GameObject> items = null;
    #endregion

    #region 属性
    public int Sum
    {
        set
        {
            sum = value;

            //方块全被消除,游戏结束
            if (sum <= 0) 
            {
                //记录游戏得分
                ScoreData.Instance.LastScore = scores;
                ScoreData.Instance.GameCount++;
                ScoreData.Instance.HighScoresAdd(scores); 
                
                //清空方块以及其他数据
                ClearItemsList();
                
                //跳转
                SceneChangeManager.Instance.ToGameOverPanel(gameObject); 
            }
        }
        get { return sum; }
    }
    #endregion

    private void Awake()
    {
        Instace = this;
    }

    void Start()
    {
        Init();
    }
    private void OnEnable()
    {
        InitGamePanelView();
    }

    /// <summary>
    /// 激活设置面板, 初始化面板数据
    /// </summary>
    private void InitGamePanelView()
    {
        //激活设置面板, 初始化面板数据
        if (m_gamePanelView != null)
        {
            m_gamePanelView.SetValue_gab.SetActive(true);
            m_gamePanelView.HorizontalText_InputField = "";
            m_gamePanelView.VerticalText_InputField = "";
            m_gamePanelView.Score = "0";
        }
        scores = 0;
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        m_gamePanelView = gameObject.GetComponent<GamePanelView>();
        m_gamePanelModel = gameObject.GetComponent<GamePanelModel>();
        m_gamePanelView.Return_Button.onClick.AddListener(ReturnButtonOnclick);
        m_gamePanelView.ConfirmButton_Button.onClick.AddListener(ConfirmButtonClick);
        items = new List<GameObject>();
    }

    //TODO: 可以用DOTween
    /// <summary>
    /// 延时激活或者隐藏组件
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="seconds"></param>
    private void DelayisActive(GameObject gameObject, float seconds)
    {
        StartCoroutine(isActive(gameObject, seconds));
    }

    /// <summary>
    /// 组件激活或者关闭
    /// </summary>
    /// <param name="gameObject"></param>
    private IEnumerator isActive(GameObject gameObject, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        //如果组件已经激活
        if (gameObject.activeSelf) gameObject.SetActive(false);
        //否则
        else gameObject.SetActive(true);
    }

    /// <summary>
    /// 确认按钮点击方法
    /// </summary>
    private void ConfirmButtonClick()
    {
        //设置生成的方块矩阵的行数和列数
        horizontal = int.Parse(m_gamePanelView.HorizontalText_InputField);
        vertical = int.Parse(m_gamePanelView.VerticalText_InputField);
        sum = horizontal * vertical;
        maxSum = sum;

        //安全检测：输入的其中一个必须是偶数，且不能输入0
        if (sum == 0 || sum % 2 != 0)
        {
            //弹出错误信息框
            DelayisActive(m_gamePanelView.Info_GameObject, 0.1f);
            DelayisActive(m_gamePanelView.Info_GameObject, 1f);

            return;
        }

        nums = new int[sum];

        //设置UI面板展示行列数
        m_gamePanelView.Grid_Transform.gameObject.GetComponent<GridLayoutGroup>().constraintCount = vertical;

        //隐藏设置面板
        m_gamePanelView.SetValue_gab.SetActive(false);

        CreateItems();
    }

    /// <summary>
    /// 返回按钮点击方法
    /// </summary>
    private void ReturnButtonOnclick()
    {
        //清空方块数据
        sum = 0;
        ClearItemsList();

        SceneChangeManager.Instance.ToMainPanel(gameObject);
    }

    /// <summary>
    /// 生成所有方块
    /// </summary>
    private void CreateItems()
    {
        Shuffle();
        for (int i = 0; i < sum; i++)
        {
            GameObject temp = GameObject.Instantiate(m_gamePanelView.Test3_Item_prefab, m_gamePanelView.Grid_Transform);
            ItemController itemController = temp.GetComponent<ItemController>();

            itemController.ChangeNumValue(nums[i]);

            items.Add(temp);
        }
    }

    /// <summary>
    /// 清空所有方块
    /// </summary>
    private void ClearItemsList()
    {
        for(int i = 0; i < items.Count; i++)
        {
            if (items[i] != null) Destroy(items[i]);
        }
        items.Clear();
        maxSum = 0;
        score = 0;
        scores = 0;
    }

    /// <summary>
    /// 打乱方块矩阵顺序
    /// </summary>
    private void Shuffle()
    {
        InitNums();
        for(int i = 0; i < sum; i++)
        {
            //随机置换
            int index = Random.Range(0, sum);
            int tempNum = nums[i];
            nums[i] = nums[index];
            nums[index] = tempNum;
        }
    }

    /// <summary>
    /// 初始化数值数组
    /// </summary>
    private void InitNums()
    {
        // 初始化数字种类为2
        int maxNum = 2;

        int len = sum;

        //动态难度调节： 如果sum大于10，增加数组中包含的数字种类
        while (len > 10)
        {
            maxNum++; 
            len -= 20; 
        }

        //给数组赋值
        for (int i = 0; i < sum; i++)
        {
            nums[i] = i % maxNum + 1; 
        }

        // 确保每个数字的个数都是偶数个
        for (int i = 0; i < maxNum; i++)
        {
            //统计当前种类数字数量
            int numCount = 0;
            for (int j = 0; j < sum; j++)
            {
                if (nums[j] == i + 1)
                {
                    numCount++;
                }
            }

            // 如果某个数字的个数不是偶数个，调整数组
            if (numCount % 2 != 0)
            {
                // 将一个该数字的实例改为下一个数字
                for (int j = 0; j < sum; j++)
                {
                    if (nums[j] == i + 1)
                    {
                        nums[j] = i + 2;
                        break;
                    }
                }
            }
        }
    }


    /// <summary>
    /// 比较两个方块是否相同
    /// </summary>
    private void CompareAndClear(GameObject item)
    {
        //如果没记录过上一个点击的方块
        if(lastItem == null)
        {
            //记录
            lastItem = item;
        }
        //如果已经记录了上一个点击的方块
        else
        {
            //重复点击同一方块，不消除
            if (item == lastItem) return;

            //比较方块背面数字，如果相同就删除两个方块
            ItemController lastItemScr = lastItem.GetComponent<ItemController>();
            ItemController itemScr = item.GetComponent<ItemController>();

            //相同
            if (lastItemScr.Num.Equals(itemScr.Num))
            {
                //销毁相同的两个方块
                Destroy(item, 0.5f);
                Destroy(lastItem, 0.5f);

                //记录得分和剩余方块数
                Sum -= 2;
                score = (maxSum / 10) * 10 + 10;
                scores += score;
                m_gamePanelView.Score = scores.ToString();
                lastItem = null;
            }
            //不同
            else
            {
                lastItemScr.DelayChangeStatus(0.5f);
                itemScr.DelayChangeStatus(0.5f);
                lastItem = null;
            }

        }
    }

}
