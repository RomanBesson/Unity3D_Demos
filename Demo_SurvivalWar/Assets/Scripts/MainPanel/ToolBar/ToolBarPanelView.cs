using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBarPanelView : MonoBehaviour {


    private Transform grid_Transform; //物品列表预制体

    private GameObject prefab_ToolBarSlot; //工具栏物品预制体

    public Transform Grid_Transform { get { return grid_Transform; } }
    public GameObject Prefab_ToolBarSlot { get { return prefab_ToolBarSlot; } }

	void Awake () {
        grid_Transform = transform.Find("Grid").GetComponent<Transform>();

        prefab_ToolBarSlot = Resources.Load<GameObject>("ToolBarSlot");
	}
	
}
