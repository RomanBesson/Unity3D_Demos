using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 统一管理所有按键输入
/// </summary>
public class InputManager : MonoBehaviour {

    private bool inventoryState = false; //背包UI显示状态

    void Start()
    {
        InventoryPanelController.Instance.UIPanelHide();
    }



	void Update ()
    {
        //检测背包隐藏和显示按键是否点击
        InventoryPanelKey();
        //检测是否按下了切换工具栏的按键
        ToolBarPanelKey();
    }

    /// <summary>
    /// 检测背包隐藏和显示按键是否点击
    /// </summary>
    private void InventoryPanelKey()
    {
        if (Input.GetKeyDown(GameConst.InventoryPanelKey))
        {
            if (inventoryState)
            {
                inventoryState = false;
                InventoryPanelController.Instance.UIPanelHide();
            }
            else
            {
                inventoryState = true;
                InventoryPanelController.Instance.UIPanelShow();
            }
        }
    }

    /// <summary>
    /// 检测是否按下了切换工具栏的按键
    /// </summary>
    private void ToolBarPanelKey()
    {
        ToolBarKey(GameConst.ToolBarPanelKey_1, 0);
        ToolBarKey(GameConst.ToolBarPanelKey_2, 1);
        ToolBarKey(GameConst.ToolBarPanelKey_3, 2);
        ToolBarKey(GameConst.ToolBarPanelKey_4, 3);
        ToolBarKey(GameConst.ToolBarPanelKey_5, 4);
        ToolBarKey(GameConst.ToolBarPanelKey_6, 5);
        ToolBarKey(GameConst.ToolBarPanelKey_7, 6);
        ToolBarKey(GameConst.ToolBarPanelKey_8, 7);
    }

    /// <summary>
    /// 切换工具栏单按键点击方法
    /// </summary>
    /// <param name="keyCode">按下的键字</param>
    /// <param name="index">键对应的索引</param>
    private void ToolBarKey(KeyCode keyCode, int index)
    {
        if (Input.GetKeyDown(keyCode))
        {
            ToolBarPanelController.Instance.SaveActiveSlotByKey(index);
        }
    }
}
