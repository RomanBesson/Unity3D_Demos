using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 地基模型.
/// </summary>
public class Platform : MaterialModelBase {

    /// <summary>
    /// 记录 已经被触发过 地基上的触发器 的名称
    /// </summary>
    private string indexName;

    /// <summary>
    /// 触发到的游戏地基.
    /// </summary>
    private Transform targetPlatform;   

    protected void Start()
    {
        IsCunPut = true;
    }

    private void OnCollisionEnter(Collision coll)
    {
        //不是场景，不可以摆放
        if (coll.collider.tag != "Terrain")
        {
            IsCunPut = false;
        }
    }


    private void OnCollisionStay(Collision coll)
    {
        //不是场景，不可以摆放
        if (coll.collider.tag != "Terrain")
        {
            IsCunPut = false;
        }
    }

    protected void OnCollisionExit(Collision coll)
    {
        //脱离不是场景的物体，可以摆放
        if (coll.collider.tag != "Terrain")
        {
            IsCunPut = true;
        }
    }

    protected override void OnTriggerEnter(Collider coll)
    {
        //如果触发到了 地基上的对应触发器
        if (coll.gameObject.tag == "PlatformToWall")
        {
            IsAttach = true;

            //初始化偏移量和目标地基位置
            Vector3 modelPosition = Vector3.zero;
            Vector3 targetPos = coll.gameObject.GetComponent<Transform>().parent.position;

            //获取目标地基
            targetPlatform = coll.gameObject.GetComponent<Transform>().parent;

            //设置偏移量 记录触发器名称
            switch (coll.gameObject.name)
            {
                case "A":
                    modelPosition = new Vector3(-3.3f, 0, 0);
                    indexName = "A";
                    break;
                case "B":
                    modelPosition = new Vector3(0, 0, 3.3f);
                    indexName = "B";
                    break;
                case "C":
                    modelPosition = new Vector3(3.3f, 0, 0);
                    indexName = "C";
                    break;
                case "D":
                    modelPosition = new Vector3(0, 0, -3.3f);
                    indexName = "D";
                    break;
            }

            //吸附
            transform.position = targetPos + modelPosition;
        }
    }

    protected override void OnTriggerExit(Collider coll)
    {
        //退出地基吸附范围
        if (coll.gameObject.tag == "Platform")
        {
            //标志位变为不可吸附
            IsAttach = false;
        }
    }

}
