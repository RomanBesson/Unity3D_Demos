using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBarPanelView : MonoBehaviour {

    private Transform grid_Transform;            // 工具栏格子管理列表预制体

    private GameObject prefab_ToolBarSlot;       // 工具栏物品格子预制体


    private GameObject prefab_Item;              // 背包格子具体物品预制体

    /// <summary>
    /// 工具栏格子管理列表预制体
    /// </summary>
    public Transform Grid_Transform { get { return grid_Transform; } }
    /// <summary>
    /// 工具栏物品格子预制体
    /// </summary>
    public GameObject Prefab_ToolBarSlot { get { return prefab_ToolBarSlot; } }
    /// <summary>
    /// 背包格子具体物品预制体
    /// </summary>
    public GameObject Prefab_Item { get { return prefab_Item; } }

	void Awake () {
        grid_Transform = transform.Find("Grid").GetComponent<Transform>();

        prefab_ToolBarSlot = Resources.Load<GameObject>("ToolBarSlot");

        prefab_Item = Resources.Load<GameObject>("InventoryItem");
    }
	
}
