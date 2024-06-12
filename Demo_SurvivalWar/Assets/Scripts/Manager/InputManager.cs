using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

/// <summary>
/// 统一管理所有按键输入
/// </summary>
public class InputManager : MonoBehaviour {

    private bool inventoryState = false;                       //背包UI显示状态
    private FirstPersonController m_FirstPersonController;     //玩家控制脚本

    void Start()
    {
        InventoryPanelController.Instance.UIPanelHide();
        FindInit();
    }

	void Update ()
    {
        //检测背包隐藏和显示按键是否点击
        InventoryPanelKey();
        //检测是否按下了切换工具栏的按键
        ToolBarPanelKey();
    }

    /// <summary>
    /// 查找相关组件
    /// </summary>
    private void FindInit()
    {
       m_FirstPersonController = GameObject.Find("FPSController").GetComponent<FirstPersonController>();
    }

    /// <summary>
    /// 检测背包隐藏和显示按键是否点击
    /// </summary>
    private void InventoryPanelKey()
    {
        if (Input.GetKeyDown(GameConst.InventoryPanelKey))
        {
            //如果当前背包UI是显示的,要转变为关闭
            if (inventoryState)
            {
                //设置背包状态标志位为关闭
                inventoryState = false;
                //隐藏背包
                InventoryPanelController.Instance.UIPanelHide();
                //开启角色控制
                m_FirstPersonController.enabled = true;
                //开启角色控制
                if (ToolBarPanelController.Instance.CurrentActiveModel != null) 
                    ToolBarPanelController.Instance.CurrentActiveModel.SetActive(true);
            }
            else
            //如果当前背包UI是隐藏的，要打开背包
            {
                //设置背包状态标志位为开启
                inventoryState = true;
                //打开背包
                InventoryPanelController.Instance.UIPanelShow();
                //关闭角色控制
                m_FirstPersonController.enabled = false;
                //关闭鼠标锁定
                Cursor.lockState = CursorLockMode.None;
                //显示鼠标指针
                Cursor.visible = true;
                //关闭角色控制
                if (ToolBarPanelController.Instance.CurrentActiveModel != null)
                    ToolBarPanelController.Instance.CurrentActiveModel.SetActive(false);
            }
        }
    }

    /// <summary>
    /// 检测是否按下了切换工具栏的按键
    /// </summary>
    private void ToolBarPanelKey()
    {
        //如果背包隐藏的话，可以切换工具栏
        if (!inventoryState)
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
