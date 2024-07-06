using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeManager : MonoBehaviour
{
    public static SceneChangeManager Instance;

    private GamePanelCotroller s_gamePanelCotroller;

    private MainPanelContrller s_mainPanelContrller;

    private GameOverPanelController s_gameOverPanelController;

    private void Start()
    {
        Instance = this;
        Init();
    }
    
    /// <summary>
    /// 初始化方法
    /// </summary>
    private void Init()
    {
        //导入页面控制脚本
        s_gamePanelCotroller = GamePanelCotroller.Instace;
        s_gamePanelCotroller.gameObject.SetActive(false);
        s_mainPanelContrller = MainPanelContrller.Instance;
        s_gameOverPanelController = GameOverPanelController.Instance;
        s_gameOverPanelController.gameObject.SetActive(false);
    }

    #region 可以配合枚举合并成一个方法（待实现）
    /// <summary>
    /// 跳转到游戏场景
    /// </summary>
    public void ToGamePanel(GameObject oldPanel)
    {
        //上个页面隐藏，新页面显示
        oldPanel.SetActive(false);
        s_gamePanelCotroller.gameObject.SetActive(true);
    }

    /// <summary>
    /// 跳转到主页面
    /// </summary>
    /// <param name="oldPanel"></param>
    public void ToMainPanel(GameObject oldPanel)
    {
        oldPanel.SetActive(false);
        s_mainPanelContrller.gameObject.SetActive(true);
    }

    /// <summary>
    /// 跳转到游戏结束页面
    /// </summary>
    /// <param name="oldPanel"></param>
    public void ToGameOverPanel(GameObject oldPanel)
    {
        oldPanel.SetActive(false);
        s_gameOverPanelController.gameObject.SetActive(true);
    }
    #endregion
}
