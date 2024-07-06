using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/// <summary>
/// 临时存储得分相关数据
/// </summary>
public class ScoreData : MonoBehaviour
{
    public static ScoreData Instance;
    private int gameCount;
    private int lastScore;
    private List<int> highScores;

    /// <summary>
    /// 游玩次数
    /// </summary>
    public int GameCount
    {
        get { return gameCount; }
        set { gameCount = value; }
    }

    /// <summary>
    /// 上次得分数
    /// </summary>
    public int LastScore
    {
        get { return lastScore; }
        set { lastScore = value; }
    }

    /// <summary>
    /// 历史分数集合
    /// </summary>
    public List<int> HighScores
    {
        get { return highScores; }
        set { highScores = value; }
    }

    private void Awake()
    {
        Instance = this;
        InitScoreData();
    }

    private void OnDestroy()
    {
        WriteGameScores();
    }


    /// <summary>
    /// 初始化得分相关数据
    /// </summary>
    private void InitScoreData()
    {
        GameData gameData = XmlTool.ReadGameStatsFromXml<GameData>("GameData");
        GameCount = gameData.GameCount;
        LastScore = gameData.LastScore;
        HighScores = gameData.HighScores;
    }

    /// <summary>
    /// 把得分数据写入Xml文件
    /// </summary>
    private void WriteGameScores()
    {
        GameData gameData = new GameData(GameCount, LastScore, highScores);

        XmlTool.WriteGameStatsToXml(gameData, "GameData");
    }

    /// <summary>
    /// 向历史分数集合里添加元素
    /// </summary>
    public void HighScoresAdd(int score)
    {
        // 如果排行榜为空或者score大于排行榜中的最小值，则添加score
        if (highScores.Count == 0 || score > highScores[highScores.Count - 1])
        {
            // 添加score到排行榜
            highScores.Add(score);

            // 保持排行榜从大到小的排序
            highScores.Sort((a, b) => b.CompareTo(a));

            // 移除最小的数字

            highScores.RemoveAt(highScores.Count - 1);
        }
    }

    /// <summary>
    /// 把历史分数集合作为字符串输出
    /// </summary>
    /// <returns></returns>
    public string GetHighScoresToString()
    {
        StringBuilder result = new StringBuilder();
        for (int i = 0; i < highScores.Count; i++)
        {
            //-1是初始化数值
            if (highScores[i] != -1)
                result.Append(highScores[i].ToString() + "\n");
            
        }
        return result.ToString();
    }

}
