using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanelView : MonoBehaviour
{
    /// <summary>
    /// 返回主页面按钮_按钮组件
    /// </summary>
    private Button returnButton;


    public Button ReturnButton { get { return returnButton; } }

    private void Awake()
    {
        returnButton = transform.Find("ReturnButton").GetComponent<Button>();
    }

}
