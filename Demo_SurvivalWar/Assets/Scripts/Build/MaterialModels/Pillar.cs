using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 建筑材料_承重柱子的脚本
/// </summary>
public class Pillar : MaterialModelBase {


    protected override void OnTriggerEnter(Collider coll)
    {
        //如果接触到地基上的方块触发器
        if (coll.gameObject.tag == "PlatformToPillar")
        {
            //设置标志位
            IsCunPut = true;
            IsAttach = true;

            //设置位置
            transform.position = coll.gameObject.GetComponent<Transform>().position;
        }
    }

    protected override void OnTriggerExit(Collider coll)
    {
        //脱离触发器
        if (coll.gameObject.tag == "PlatformToPillar")
        {
            //还原标志位
            IsCunPut = false;
            IsAttach = false;
        }
    }
}
