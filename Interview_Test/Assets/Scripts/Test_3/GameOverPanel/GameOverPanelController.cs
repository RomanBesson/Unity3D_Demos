using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanelController : MonoBehaviour
{
    public static GameOverPanelController Instance;

    private GameOverPanelView o_gameOverPanelView;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        o_gameOverPanelView = gameObject.GetComponent<GameOverPanelView>();
        o_gameOverPanelView.ReturnButton.onClick.AddListener(ButtonOnClick);
    }

    /// <summary>
    /// 重新开始按钮点击事件
    /// </summary>
    public void ButtonOnClick()
    {
        SceneChangeManager.Instance.ToMainPanel(gameObject);
    }
}
