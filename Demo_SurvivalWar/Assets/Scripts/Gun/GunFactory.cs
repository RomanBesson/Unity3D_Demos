using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 枪械工厂类.
/// </summary>
public class GunFactory : MonoBehaviour {

    public static GunFactory Instance;

    private GameObject prefab_AssaultRifle;     //步枪
    private GameObject prefab_Shotgun;          //霰弹枪
    private GameObject prefab_WoodenBow;        //弓箭
    private GameObject prefab_WoodenSpear;      //长矛
    private int index = 0;                      //序号

    void Awake()
    {
        Instance = this;
    }

	void Start () {
        PrefabLoad();
	}

    /// <summary>
    /// 加载枪械资源
    /// </summary>
    private void PrefabLoad()
    {
        prefab_AssaultRifle = Resources.Load<GameObject>("Gun/Prefabs/Assault Rifle");
        prefab_Shotgun = Resources.Load<GameObject>("Gun/Prefabs/Shotgun");
        prefab_WoodenBow = Resources.Load<GameObject>("Gun/Prefabs/Wooden Bow");
        prefab_WoodenSpear = Resources.Load<GameObject>("Gun/Prefabs/Wooden Spear");
    }

    /// <summary>
    /// 生成枪械
    /// </summary>
    /// <param name="gunName">枪械名称</param>
    /// <param name="icon">对应的UI物体</param>
    /// <returns></returns>
    public GameObject CreateGun(string gunName, GameObject icon)
    {
        GameObject tempGun = null;
        switch(gunName)
        {
            case "Assault Rifle":
                tempGun = GameObject.Instantiate<GameObject>(prefab_AssaultRifle, transform);
                InitGun(tempGun, 50, 20, GunType.AssaultRifle, icon);
                break;
            case "Shotgun":
                tempGun = GameObject.Instantiate<GameObject>(prefab_Shotgun, transform);
                InitGun(tempGun, 100, 10, GunType.Shotgun, icon);
                break;
            case "Wooden Bow":
                tempGun = GameObject.Instantiate<GameObject>(prefab_WoodenBow, transform);
                InitGun(tempGun, 20, 100, GunType.WoodenBow, icon);
                break;
            case "Wooden Spear":
                tempGun = GameObject.Instantiate<GameObject>(prefab_WoodenSpear, transform);
                InitGun(tempGun, 200, 5, GunType.WoodenSpear, icon);
                break;
        }
        return tempGun;
    }

    /// <summary>
    /// 初始化武器的基本数值
    /// </summary>
    /// <param name="gun"></param>
    /// <param name="damage"></param>
    /// <param name="durable"></param>
    /// <param name="type"></param>
    private void InitGun(GameObject gun, int damage, int durable, GunType type, GameObject icon)
    {
        GunControllerBase gcb = gun.GetComponent<GunControllerBase>();
        gcb.Id = index++;
        gcb.Damage = damage;
        gcb.Durable = durable;
        gcb.GunWeaponType = type;
        gcb.ToolBarIcon = icon;
    }
}
