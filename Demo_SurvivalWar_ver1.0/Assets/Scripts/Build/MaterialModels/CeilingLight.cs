using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 建筑材料_灯脚本
/// </summary>
public class CeilingLight : MaterialModelBase {

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
        //如果触发到了屋顶模型的中心的触发器
        if (coll.gameObject.name == "LightTrigger")
        {
            //设置标志位
            IsCunPut = true;
            IsAttach = true;

            //设置吸附位置
            transform.position = coll.gameObject.GetComponent<Transform>().position;

            //设置要摧毁的触发器为当前触发器
            trigger = coll.gameObject;
        }
    }

    protected override void OnTriggerExit(Collider coll)
    {
        //退出触发器
        if (coll.gameObject.name == "LightTrigger")
        {
            //还原标志位
            IsCunPut = false;
            IsAttach = false;
        }
    }

}
