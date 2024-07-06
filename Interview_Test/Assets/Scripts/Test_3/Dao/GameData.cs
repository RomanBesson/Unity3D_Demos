using System.Collections.Generic;
using System.Xml.Serialization;

/// <summary>
/// 游戏结算数据数据实体类
/// </summary>
[XmlRoot("GameData")]
public class GameData
{
    private int gameCount;
    private int lastScore;
    private List<int> highScores;

     /// <summary>
     /// 游玩次数
     /// </summary>
     [XmlElement("GameCount")]
     public int GameCount
     {
         get { return gameCount; }
         set { gameCount = value; }
     }

     /// <summary>
     /// 上次得分数
     /// </summary>
     [XmlElement("LastScore")]
     public int LastScore
     {
         get { return lastScore; }
         set { lastScore = value; }
     }

     /// <summary>
     /// 历史分数集合
     /// </summary>
     [XmlElement("HighScores")]
     public List<int> HighScores
     {
         get { return highScores; }
         set { highScores = value; }
     }


     public GameData()
     {
         HighScores = new List<int>();
     }

     public GameData(int count, int lastCount, List<int> nums)
     {
         GameCount = count;
         LastScore = lastCount;
         HighScores = nums;
     }

    public override string ToString()
    {
        return string.Format("游玩次数{0}, 上次得分数{1}", GameCount, LastScore);
    }

}