using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanelView : MonoBehaviour
{
    /// <summary>
    /// 开始游戏按钮_按钮组件
    /// </summary>
    private Button begin_Button;
    /// <summary>
    /// 查看历史排行榜按钮_按钮组件
    /// </summary>
    private Button look_Button;
    /// <summary>
    /// 退出排行榜
    /// </summary>
    private Button exit_Button;
    /// <summary>
    /// 游玩次数
    /// </summary>
    private Text gameCount_Text;
    /// <summary>
    /// 上次分数
    /// </summary>
    private Text lastScore_Text;
    /// <summary>
    /// 历史成绩
    /// </summary>
    private Text highScores_Text;
    /// <summary>
    /// 历史排行榜对象
    /// </summary>
    private GameObject highScores_GameObject;

    /// <summary>
    /// 开始游戏按钮_按钮组件
    /// </summary>
    public Button Begin_Button { get { return begin_Button; } }

    /// <summary>
    /// 游玩次数
    /// </summary>
    public string GameCount_Text { get { return gameCount_Text.text; } set { gameCount_Text.text = value; } }
    /// <summary>
    /// 上次分数
    /// </summary>
    public string LastScore_Text { get { return lastScore_Text.text; } set { lastScore_Text.text = value; } }
    /// <summary>
    /// 历史成绩
    /// </summary>
    public string HighScores_Text { get { return highScores_Text.text; } set { highScores_Text.text = value; } }
    /// <summary>
    /// 历史排行榜对象
    /// </summary>
    public GameObject HighScores_GameObject { get => highScores_GameObject; set => highScores_GameObject = value; }
    /// <summary>
    /// 查看历史排行榜按钮_按钮组件
    /// </summary>
    public Button Look_Button { get => look_Button; set => look_Button = value; }
    /// <summary>
    /// 退出排行榜_按钮组件
    /// </summary>
    public Button Exit_Button { get => exit_Button; set => exit_Button = value; }

    private void Awake()
    {
        begin_Button = transform.Find("Begin_Button").GetComponent<Button>();
        gameCount_Text = transform.Find("GameCount/Num").GetComponent<Text>();
        lastScore_Text = transform.Find("LastScore/Num").GetComponent<Text>();
        highScores_Text = transform.Find("HighScores/Num").GetComponent<Text>();
        look_Button = transform.Find("Look_Button").GetComponent<Button>();
        exit_Button = transform.Find("HighScores/Exit").GetComponent<Button>();
        highScores_GameObject = transform.Find("HighScores").gameObject;
        highScores_GameObject.SetActive(false);
    }
}
