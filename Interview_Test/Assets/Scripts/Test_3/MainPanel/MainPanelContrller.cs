using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPanelContrller : MonoBehaviour
{
    public static MainPanelContrller Instance;

    /// <summary>
    /// 主页面视图层游戏脚本_游戏脚本
    /// </summary>
    private MainPanelView m_mainPanelView;
    /// <summary>
    /// 主页面数据层游戏脚本_游戏脚本
    /// </summary>
    private MainPanelModel m_mainPanelModel;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Init();
        InitShowUI();
    }

    /// <summary>
    /// 初始化组件
    /// </summary>
    private void Init()
    {
        m_mainPanelView = gameObject.GetComponent<MainPanelView>();
        m_mainPanelModel = gameObject.GetComponent<MainPanelModel>();
        m_mainPanelView.Begin_Button.onClick.AddListener(BeginButtonOnClick);
        m_mainPanelView.Look_Button.onClick.AddListener(LookButtonOnClick);
        m_mainPanelView.Exit_Button.onClick.AddListener(ExitButtonOnClick);
    }

    private void OnEnable()
    {
        InitShowUI();
    }

    /// <summary>
    /// 修改面板成绩展示UI数值
    /// </summary>
    public void InitShowUI()
    {
        //安全性检测
        if (m_mainPanelView != null)
        {
            m_mainPanelView.GameCount_Text = ScoreData.Instance.GameCount.ToString();
            m_mainPanelView.LastScore_Text = ScoreData.Instance.LastScore.ToString();
            m_mainPanelView.HighScores_Text = ScoreData.Instance.GetHighScoresToString();
        }
    }

    /// <summary>
    /// 开始游戏按钮点击事件
    /// </summary>
    public void BeginButtonOnClick()
    {
        SceneChangeManager.Instance.ToGamePanel(gameObject);
    }

    /// <summary>
    /// 查看历史排行榜按钮点击事件
    /// </summary>
    public void LookButtonOnClick()
    {
        //激活排行榜
        m_mainPanelView.HighScores_GameObject.SetActive(true);
    }

    /// <summary>
    /// 退出历史排行榜按钮点击事件
    /// </summary>
    public void ExitButtonOnClick()
    {
        //激活排行榜
        m_mainPanelView.HighScores_GameObject.SetActive(false);
    }
}
