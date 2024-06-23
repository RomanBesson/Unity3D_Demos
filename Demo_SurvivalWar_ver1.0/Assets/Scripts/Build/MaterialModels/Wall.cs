using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 建筑材料_墙壁脚本
/// </summary>
public class Wall : MaterialModelBase {

    /// <summary>
    /// 触发到的触发器
    /// </summary>
    private GameObject trigger = null;

    /// <summary>
    /// 恢复成原来的材质，以及设置触碰到的触发器
    /// </summary>
    public override void Normal()
    {
        base.Normal();
        if (trigger != null) trigger.name = trigger.name + "_Over";
    }

    protected override void OnTriggerEnter(Collider coll)
    {
        //如果是地基物品上可以放置墙壁的点，且没被放置过墙壁
        if (coll.gameObject.tag == "PlatformToWall" && coll.gameObject.name.Length == 1)
        {
            //设置标志位
            IsCunPut = true;
            IsAttach = true;

            //设置位置和旋转
            transform.position = coll.gameObject.GetComponent<Transform>().position;
            transform.rotation = coll.gameObject.GetComponent<Transform>().rotation;

            //设置要修改的触发器
            trigger = coll.gameObject;
        }
    }

    protected override void OnTriggerExit(Collider coll)
    {
        //退出吸附点
        if (coll.gameObject.tag == "PlatformToWall")
        {
            //还原标志位
            IsCunPut = false;
            IsAttach = false;
        }
    }


}
