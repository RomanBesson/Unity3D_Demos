using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 建筑材料_楼梯
/// </summary>
public class Stairs : MaterialModelBase {

    /// <summary>
    /// 触碰到的触发器对象
    /// </summary>
    private GameObject trigger = null;

    /// <summary>
    /// 恢复成原来的材质，设置触发器名称
    /// </summary>
    public override void Normal()
    {
        base.Normal();
        if (trigger != null) trigger.name = "E";
    }

    protected override void OnTriggerEnter(Collider coll)
    {
        //如果触发到了地基上的墙壁触发器
        if (coll.gameObject.tag == "PlatformToWall")
        {
            //设置标志位
            IsCunPut = true;
            IsAttach = true;

            //触发器位置
            Vector3 targetPos = coll.gameObject.GetComponent<Transform>().parent.position;

            //位置偏移量
            Vector3 modelPos = Vector3.zero;
            
            //旋转偏移量
            Vector3 modelRot = Vector3.zero;

            //不同方向的触发器为偏移量进行不同的赋值 (取前面的标识值)
            switch(coll.gameObject.name[0].ToString())
            {
                case "A":
                    modelPos = new Vector3(-2.5f, 0, 0);
                    modelRot = new Vector3(0, 0, 0);
                    break;
                case "B":
                    modelPos = new Vector3(0, 0, 2.5f);
                    modelRot = new Vector3(0, 90, 0);
                    break;
                case "C":
                    modelPos = new Vector3(2.5f, 0, 0);
                    modelRot = new Vector3(0, 180, 0);
                    break;
                case "D":
                    modelPos = new Vector3(0, 0, -2.5f);
                    modelRot = new Vector3(0, 270, 0);
                    break;
            }

            //设置位置和旋转
            transform.position = targetPos + modelPos;
            transform.rotation = Quaternion.Euler(modelRot);
            
            //把当前对象设置为目标触发器
            trigger = coll.gameObject;
        }
    }

    protected override void OnTriggerExit(Collider coll)
    {
        //脱离触发器
        if (coll.gameObject.tag == "PlatformToWall")
        {
            //还原标志位
            IsCunPut = false;
            IsAttach = false;
        }
    }

}
