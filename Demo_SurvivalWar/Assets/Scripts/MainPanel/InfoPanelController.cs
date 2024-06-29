using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 主页面输出信息脚本
/// </summary>
public class InfoPanelController : MonoBehaviour
{
    public static InfoPanelController Instance;

    /// <summary>
    /// 输出信息文本
    /// </summary>
    private Text info_Text;

    private void Awake()
    {
        Instance = this;
        info_Text = transform.Find("InfoText").GetComponent<Text>();
    }

    /// <summary>
    /// 设置输出信息
    /// </summary>
    /// <param name="str"></param>
    public void SetInfoText(string str)
    {
        info_Text.text = str;
    }

}
