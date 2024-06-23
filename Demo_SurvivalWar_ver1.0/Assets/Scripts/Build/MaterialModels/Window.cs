using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 建筑材料_窗户脚本
/// </summary>
public class Window : MaterialModelBase {

    /// <summary>
    /// 触碰到的触发器
    /// </summary>
    private GameObject trigger = null;

    /// <summary>
    /// 恢复成原来的材质，摧毁对应触发器
    /// </summary>
    public override void Normal()
    {
        base.Normal();
        if (trigger != null) GameObject.Destroy(trigger);
    }


    protected override void OnTriggerEnter(Collider coll)
    {
        //放到了窗户形墙的触发器上
        if (coll.gameObject.name == "windowTrigger")
        {
            //设置标志位
            IsCunPut = true;
            IsAttach = true;

            //设置位置和旋转
            transform.position = coll.gameObject.GetComponent<Transform>().position;
            transform.rotation = coll.gameObject.GetComponent<Transform>().rotation;

            //设置要摧毁的触发器为当前触发器
            trigger = coll.gameObject;
        }
    }

    protected override void OnTriggerExit(Collider coll)
    {
        //退出触发器
        if (coll.gameObject.name == "windowTrigger")
        {
            //还原标志位
            IsCunPut = false;
            IsAttach = false;
        }
    }
}
