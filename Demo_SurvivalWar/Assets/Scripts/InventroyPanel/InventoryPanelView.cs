using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 背包视图层：UI组件的获取，资源的导入
/// </summary>
public class InventoryPanelView : MonoBehaviour
{
    private Transform grid_Transform; //布局对象的位置信息

    private GameObject prefab_Slot; //背包格子预制体
    private GameObject prefab_Item; //背包格子具体信息预制体

    #region 字段属性信息
    public Transform GridTransform { get { return grid_Transform; } }
    public GameObject Prefab_Slot { get { return prefab_Slot; } }
    public GameObject Prefab_Item { get { return prefab_Item; } }
    #endregion

    private void Awake()
    {
        grid_Transform = transform.Find("Background/Grid").transform;

        prefab_Slot = Resources.Load<GameObject>("InventorySlot");
        prefab_Item = Resources.Load<GameObject>("InventoryItem");
    }
}
