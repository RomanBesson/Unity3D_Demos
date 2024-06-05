using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private GameObject m_BuildingPlan; //建造模型
    private GameObject m_WoodenSpear; //长矛模型

    private GameObject currentWeapon;           //当前武器.
    private GameObject targetWeapon;            //目标武器.

    void Start () {
        Init();
    }
	

	void Update () {
        //切换建造
		if(Input.GetKeyDown(KeyCode.M))
        {
            targetWeapon = m_BuildingPlan;
            Changed();
        }
        //切换长矛
        if (Input.GetKeyDown(KeyCode.K))
        {
            targetWeapon = m_WoodenSpear;
            Changed();
        }
	}

    /// <summary>
    /// 初始化
    /// </summary>
    private void Init()
    {
        m_BuildingPlan = transform.Find("PersonCamera/Building Plan").gameObject;
        m_WoodenSpear = transform.Find("PersonCamera/Wooden Spear").gameObject;
        //默认建造模块先显示
        m_WoodenSpear.SetActive(false);
        currentWeapon = m_BuildingPlan;
        targetWeapon = null;
    }

    /// <summary>
    /// 切换状态
    /// </summary>
    private void Changed()
    {
        //做出退出动作
        currentWeapon.GetComponent<Animator>().SetTrigger("Holster");
        StartCoroutine("DelayTime");
    }


    /// <summary>
    /// 延迟切换
    /// </summary>
    /// <returns></returns>
    IEnumerator DelayTime()
    {
        yield return new WaitForSeconds(1);
        //隐藏当前武器
        currentWeapon.SetActive(false);
        //激活选中武器
        targetWeapon.SetActive(true);
        currentWeapon = targetWeapon;
    }
}
