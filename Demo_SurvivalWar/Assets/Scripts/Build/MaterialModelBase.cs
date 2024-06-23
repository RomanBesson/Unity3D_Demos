using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 建筑材料的抽象父类
/// </summary>
public abstract class MaterialModelBase : MonoBehaviour {


    /// <summary>
    /// 原始材质球
    /// </summary>
    private Material oldMaterial;
    /// <summary>
    /// 透明材质球.
    /// </summary>
    private Material newMaterial;

    private bool isCunPut = false;
    /// <summary>
    /// 当前模型所处的位置,是否可以摆放       [true: 可以摆放 | false：不可以摆放]
    /// </summary>
    public bool IsCunPut
    {
        get { return isCunPut; }
        set { isCunPut = value; }
    }
    private bool isAttach = false;
    /// <summary>
    /// 当前模型的吸附状态       [true: 可以吸附 | false：不可以吸附]
    /// </summary>
    public bool IsAttach
    {
        get { return isAttach; }
        set { isAttach = value; }
    }


    protected void Awake()
    {
        newMaterial = Resources.Load<Material>("Build/Building Preview");
        oldMaterial = gameObject.GetComponent<MeshRenderer>().material;
        gameObject.GetComponent<MeshRenderer>().material = newMaterial;
    }

    void Update()
    {
        //当前位置可以摆放
        if (IsCunPut)
        {
            gameObject.GetComponent<MeshRenderer>().material.color = new Color32(0, 255, 0, 100);
        }
        //当前位置不可以摆放
        else
        {
            gameObject.GetComponent<MeshRenderer>().material.color = new Color32(255, 0, 0, 100);
        }
    }

    /// <summary>
    /// 恢复成原来的材质
    /// </summary>
    public virtual void Normal()
    {
        gameObject.GetComponent<MeshRenderer>().material = oldMaterial;
    }

    protected abstract void OnTriggerEnter(Collider coll);
    protected abstract void OnTriggerExit(Collider coll);
}
