using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 方块个体的管理脚本
/// </summary>
public class ItemController : MonoBehaviour
{
    /// <summary>
    /// 该方块背面的数字
    /// </summary>
    private Text num;
    /// <summary>
    /// 方块子物体的按钮组件
    /// </summary>
    private Button button;
    private bool isBack = true;

    /// <summary>
    /// 该方块背面的数字
    /// </summary>
    public int Num 
    {
        get 
        {
            return int.Parse(num.text);
        } 
    }

    private void Awake()
    {
        num = transform.Find("Button/Num").GetComponent<Text>();
        button = transform.Find("Button").GetComponent<Button>();
        button.onClick.AddListener(ButtonOnClick);
        num.gameObject.SetActive(false);
    }

    /// <summary>
    /// 修改方块上的数字
    /// </summary>
    public void ChangeNumValue(int value)
    {
        num.text = value.ToString();
    }

    /// <summary>
    /// 按钮点击事件
    /// </summary>
    private void ButtonOnClick()
    {
        DelayChangeStatus(0.1f);
        SendMessageUpwards("CompareAndClear", gameObject);
    }

    //TODO: 可以使用DOTween
    /// <summary>
    /// 延迟翻面
    /// </summary>
    /// <returns></returns>
    public void DelayChangeStatus(float seconds)
    {
        StartCoroutine(ChangeStatus(seconds));
    }

    /// <summary>
    /// 翻面
    /// </summary>
    private IEnumerator ChangeStatus(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        //如果此时是背面
        if (isBack)
        {
            num.gameObject.SetActive(true);
            isBack = false;
        }
        //如果此时是正面
        else
        {
            num.gameObject.SetActive(false);
            isBack = true;
        }
    }
}
