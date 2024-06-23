using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 建筑材料_屋顶脚本
/// </summary>
public class Roof : MaterialModelBase {

    protected override void OnTriggerEnter(Collider coll)
    {
        //进入墙壁模型的屋顶吸附触发器上
        if (coll.gameObject.tag == "WallToRoof")
        {
            //设置标志位
            IsCunPut = true;
            IsAttach = true;

            //设置位置
            transform.position = coll.gameObject.GetComponent<Transform>().position;
        }

        //如果同样是屋顶模型的触发器
        if (coll.gameObject.tag == "Roof")
        {
            //设置标志位
            IsCunPut = true;
            IsAttach = true;

            //碰撞到的屋顶模型位置
            Vector3 targetPos = coll.gameObject.GetComponent<Transform>().parent.position;

            //偏移量
            Vector3 selfPos = Vector3.zero;

            //根据不同位置确定偏移量
            switch(coll.gameObject.name)
            {
                case "A":
                    selfPos = new Vector3(0, 0, 3.3f);
                    break;
                case "B":
                    selfPos = new Vector3(3.3f, 0, 0);
                    break;
                case "C":
                    selfPos = new Vector3(0, 0, -3.3f);
                    break;
                case "D":
                    selfPos = new Vector3(-3.3f, 0, 0);
                    break;
            }

            //设置位置
            transform.position = targetPos + selfPos;
        }
    }

    protected override void OnTriggerExit(Collider coll)
    {
        //退出墙壁上的触发器
        if (coll.gameObject.tag == "WallToRoof")
        {
            IsCunPut = false;
            IsAttach = false;
        }
    }

}
