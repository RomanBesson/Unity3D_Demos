using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 合成面板的视图层
/// </summary>
public class CraftingPanelView : MonoBehaviour
{
    #region UI物品和资源的字段和属性
    private Transform tabs_Transform; //左面标签的位置
    private Transform contents_Transform; //左边标签所对应的内容的位置
    private Transform center_Transform; //合成台内容位置

    private GameObject prefab_TabsItem; //标签的预制体
    //左侧内容区的预制体
    private GameObject prefab_Content;
    private GameObject prefab_ContentItem;
    //合成台物品的预制体
    private GameObject prefab_Slot;
    private GameObject prefab_InventoryItem; //合成出的物品预制体

    private Dictionary<string, Sprite> tabIconDic; //储存内容选项卡的图片
    private Dictionary<string, Sprite> materialIconDic; //合成台图谱

    public Transform Tabs_Transform { get { return tabs_Transform; } }
    public Transform Contents_Transform { get { return contents_Transform; } }
    public Transform Center_Transform { get { return center_Transform; } }
    public GameObject Prefab_TabsItem { get { return prefab_TabsItem; } }
    public GameObject Prefab_Content { get { return prefab_Content; } }
    public GameObject Prefab_ContentItem { get { return prefab_ContentItem; } }
    public GameObject Prefab_Slot { get { return prefab_Slot; } }
    public GameObject Prefab_InventoryItem { get { return prefab_InventoryItem; } }
    #endregion

    private void Awake()
    {
        tabs_Transform = transform.Find("Left/Tabs").transform;
        contents_Transform = transform.Find("Left/Contents").transform;
        center_Transform = transform.Find("Center").GetComponent<Transform>();

        prefab_TabsItem = Resources.Load<GameObject>("CraftingTabsItem");
        prefab_Content = Resources.Load<GameObject>("CraftingContent");
        prefab_ContentItem = Resources.Load<GameObject>("CraftingContentItem");
        prefab_Slot = Resources.Load<GameObject>("CraftingSlot");
        prefab_InventoryItem = Resources.Load<GameObject>("InventoryItem");

        tabIconDic = new Dictionary<string, Sprite>();
        materialIconDic = new Dictionary<string, Sprite>();

        //加载所有选项卡图标
        ResourcesTools.LoadFolderAssets("TabIcon", tabIconDic);
        //合成图谱材料加载
        ResourcesTools.LoadFolderAssets("Material", materialIconDic);


    }

    /// <summary>
    /// 通过名称查找字典中的一个图片对象.
    /// </summary>
    public Sprite ByNameGetSprite(string name)
    {
        return ResourcesTools.GetAsset(name, tabIconDic);
    }

    /// <summary>
    /// 通过名字获取合成材料的图标资源.
    /// </summary>
    public Sprite ByNameGetMaterialIconSprite(string name)
    {
        return ResourcesTools.GetAsset(name, materialIconDic);
    }

}
