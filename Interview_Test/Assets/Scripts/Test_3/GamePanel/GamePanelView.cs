using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 游戏主场景视图层
/// </summary>
public class GamePanelView : MonoBehaviour
{
    #region 字段

    /// <summary>
    /// 输入的行数据
    /// </summary>
    private InputField horizontalText_InputField;
    /// <summary>
    /// 输入的列数据
    /// </summary>
    private InputField verticalText_InputField;
    /// <summary>
    /// 输入确认按钮
    /// </summary>
    private Button confirmButton_Button;
    /// <summary>
    /// 返回主菜单_按钮
    /// </summary>
    private Button return_Button;
    /// <summary>
    /// 得分版_Text组件
    /// </summary>
    private Text score;
    /// <summary>
    /// 错误信息提示版_Text组件
    /// </summary>
    private Text info;

    /// <summary>
    /// 方块列表_游戏对象
    /// </summary>
    private GameObject grid;
    /// <summary>
    /// 方块游戏对象的预制体
    /// </summary>
    private GameObject test3_Item_prefab;
    /// <summary>
    /// 设置行列的窗口框
    /// </summary>
    private GameObject setValue_gab;
    /// <summary>
    /// 错误提示框对象
    /// </summary>
    public GameObject info_GameObject;


    #endregion

    #region 属性

    /// <summary>
    /// 方块列表_位置
    /// </summary>
    public Transform Grid_Transform { get { return grid.transform; } }
    /// <summary>
    /// 方块游戏对象的预制体
    /// </summary>
    public GameObject Test3_Item_prefab { get { return test3_Item_prefab; } }
    /// <summary>
    /// 输入确认按钮
    /// </summary>
    public Button ConfirmButton_Button { get { return confirmButton_Button; } }
    /// <summary>
    /// 返回主菜单_按钮
    /// </summary>
    public Button Return_Button { get { return return_Button; } }

    /// <summary>
    /// 设置行列的窗口框
    /// </summary>
    public GameObject SetValue_gab { get { return setValue_gab; } }
    /// <summary>
    /// 输入的行数据
    /// </summary>
    public string HorizontalText_InputField { get { return horizontalText_InputField.text; } set { horizontalText_InputField.text = value; } }
    /// <summary>
    /// 输入的列数据
    /// </summary>
    public string VerticalText_InputField { get { return verticalText_InputField.text; } set { verticalText_InputField.text = value; } }

    /// <summary>
    /// 得分版数字_string
    /// </summary>
    public string Score { get { return score.text; } set { score.text = value; } }

    /// <summary>
    /// 错误信息提示版信息_string
    /// </summary>
    public string Info { get { return info.text; } set { info.text = value; } }
    /// <summary>
    /// 错误信息提示版_GameObject
    /// </summary>
    public GameObject Info_GameObject { get { return info_GameObject; }}

    #endregion

    private void Awake()
    {
        Init();
    }

    /// <summary>
    /// 初始化组件
    /// </summary>
    private void Init()
    {
        return_Button = transform.Find("Return_Button").GetComponent<Button>();
        grid = transform.Find("Scroll/Grid").gameObject;
        test3_Item_prefab = Resources.Load<GameObject>("Test3_Item");
        setValue_gab = transform.Find("SetValue").gameObject;
        horizontalText_InputField = transform.Find("SetValue/horizontal").GetComponent<InputField>();
        verticalText_InputField = transform.Find("SetValue/vertical").GetComponent<InputField>();
        confirmButton_Button = transform.Find("SetValue/ConfirmButton").GetComponent<Button>();
        score = transform.Find("Score").GetComponent<Text>();
        info = transform.Find("SetValue/Info/Text").GetComponent<Text>();
        info_GameObject = transform.Find("SetValue/Info").gameObject;
        info_GameObject.SetActive(false);
    }



}
