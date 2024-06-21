using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 建筑材料_门
/// </summary>
public class Door : MaterialModelBase {

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
        //如果放在了门该放置的触发器上
        if (coll.gameObject.name == "DoorTrigger")
        {
            //设置标志位
            IsCunPut = true;
            IsAttach = true;

            //放在对应点位，设置旋转
            transform.position = coll.gameObject.GetComponent<Transform>().parent.Find("DoorPos").position;
            transform.rotation = coll.gameObject.GetComponent<Transform>().rotation;

            //作为门形墙壁预制体
            transform.parent = coll.gameObject.GetComponent<Transform>().parent;

            //设置要摧毁的触发器为当前触发器
            trigger = coll.gameObject;
        }
    }

    protected override void OnTriggerExit(Collider coll)
    {
        //退出预制体范围
        if (coll.gameObject.name == "DoorTrigger")
        {
            //还原标志位
            IsCunPut = false;
            IsAttach = false;
            transform.parent = null;
        }
    }
}
