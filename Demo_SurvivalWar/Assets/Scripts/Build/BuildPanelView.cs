using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildPanelView : MonoBehaviour {

    private Transform BG_Transform;
    private Transform player_Transform;         //玩家角色的Transform.
    private Transform models_Parent;            //所有建造模块生成的模型的父物体.

    private GameObject item_Prefab;
    private GameObject material_Prefab;

    private Text itemName_Text;

    private Camera EnvCamera = null;           

    private List<Sprite> icons = new List<Sprite>();
    private List<Sprite[]> materialIcons = new List<Sprite[]>();
    private List<string[]> materialIconNames = new List<string[]>();
    private List<GameObject[]> materialModels = new List<GameObject[]>();

    /// <summary>
    /// 环形图片 
    /// </summary>
    public Transform M_BG_Transform { get { return BG_Transform; } }
    /// <summary>
    /// 玩家角色的Transform.
    /// </summary>
    public Transform M_Player_Transform { get { return player_Transform; } }
    /// <summary>
    /// 实例化后的建造模型的管理游戏对象
    /// </summary>
    public Transform M_Models_Parent { get { return models_Parent; } }
    /// <summary>
    /// 环形每项的预制体
    /// </summary>
    public GameObject M_Item_Prefab { get { return item_Prefab; } }
    /// <summary>
    /// 材料子项预制体
    /// </summary>
    public GameObject M_Material_Prefab { get { return material_Prefab; } }
    /// <summary>
    /// 环形中心显示的物品名称
    /// </summary>
    public Text M_ItemName_Text { get { return itemName_Text; } }
    /// <summary>
    /// 环境摄像机
    /// </summary>
    public Camera M_EnvCamera { get { return EnvCamera; } }

    /// <summary>
    /// 环形每项对应的图片集合
    /// </summary>
    public List<Sprite> Icons { get { return icons; } }
    /// <summary>
    /// 材料图片数组 
    /// </summary>
    public List<Sprite[]> MaterialIcons { get { return materialIcons; } }
    /// <summary>
    /// 环形UI项材料子项名称数组的集合
    /// </summary>
    public List<string[]> MaterialIconNames { get { return materialIconNames; } }
    /// <summary>
    /// 建筑模型集合
    /// </summary>
    public List<GameObject[]> MaterialModels { get { return materialModels; } }


    void Awake()
    {
        Init();
        LoadIcons();
        LoadMaterialIcons();
        SetMaterialIconNames();
        LoadMaterialModels();
    }

    /// <summary>
    /// 基础初始化操作.
    /// </summary>
    private void Init()
    {
        BG_Transform = transform.Find("WheelBG");
        player_Transform = GameObject.Find("FPSController").GetComponent<Transform>();
        models_Parent = GameObject.Find("BuildModels").GetComponent<Transform>();

        item_Prefab = Resources.Load<GameObject>("Build/Prefab/Item");
        material_Prefab = Resources.Load<GameObject>("Build/Prefab/MaterialBG");

        itemName_Text = transform.Find("WheelBG/ItemName").GetComponent<Text>();
        EnvCamera = GameObject.Find("EnvCamera").GetComponent<Camera>();
    }


    /// <summary>
    /// 加载九个Icon图标.
    /// </summary>
    private void LoadIcons()
    {
        icons.Add(null);
        icons.Add(Resources.Load<Sprite>("Build/Icon/Question Mark"));
        icons.Add(Resources.Load<Sprite>("Build/Icon/Roof_Category"));
        icons.Add(Resources.Load<Sprite>("Build/Icon/Stairs_Category"));
        icons.Add(Resources.Load<Sprite>("Build/Icon/Window_Category"));
        icons.Add(Resources.Load<Sprite>("Build/Icon/Door_Category"));
        icons.Add(Resources.Load<Sprite>("Build/Icon/Wall_Category"));
        icons.Add(Resources.Load<Sprite>("Build/Icon/Floor_Category"));
        icons.Add(Resources.Load<Sprite>("Build/Icon/Foundation_Category"));
    }

    /// <summary>
    /// 加载具体建造材料的图标Icons.
    /// </summary>
    private void LoadMaterialIcons()
    {
        materialIcons.Add(null);
        materialIcons.Add(new Sprite[] { LoadIcon("Ceiling Light"), LoadIcon("Pillar_Wood"), LoadIcon("Wooden Ladder") });
        materialIcons.Add(new Sprite[] { null, LoadIcon("Roof_Metal"), null });
        materialIcons.Add(new Sprite[] { LoadIcon("Stairs_Wood"), LoadIcon("L Shaped Stairs_Wood"), null });
        materialIcons.Add(new Sprite[] { null, LoadIcon("Window_Wood"), null });
        materialIcons.Add(new Sprite[] { null, LoadIcon("Wooden Door"), null });
        materialIcons.Add(new Sprite[] { LoadIcon("Wall_Wood"), LoadIcon("Doorway_Wood"), LoadIcon("Window Frame_Wood") });
        materialIcons.Add(new Sprite[] { null, LoadIcon("Floor_Wood"), null });
        materialIcons.Add(new Sprite[] { null, LoadIcon("Platform_Wood"), null });
    }

    /// <summary>
    /// 设置材料Icons的名称.
    /// </summary>
    private void SetMaterialIconNames()
    {
        materialIconNames.Add(null);
        materialIconNames.Add(new string[] { "吊灯", "木柱", "梯子" });
        materialIconNames.Add(new string[] { "屋顶" });
        materialIconNames.Add(new string[] { "直梯", "L型梯" });
        materialIconNames.Add(new string[] { "窗户" });
        materialIconNames.Add(new string[] { "门" });
        materialIconNames.Add(new string[] { "普通墙壁", "门型墙壁", "窗型墙壁" });
        materialIconNames.Add(new string[] { "地板" });
        materialIconNames.Add(new string[] { "地基" });
    }

    /// <summary>
    /// 加载建造材料模型.
    /// </summary>
    private void LoadMaterialModels()
    {
        materialModels.Add(null);
        materialModels.Add(new GameObject[] { LoadBuildModel("Ceiling_Light"), LoadBuildModel("Pillar"), LoadBuildModel("Ladder") });
        materialModels.Add(new GameObject[] { LoadBuildModel("Roof") });
        materialModels.Add(new GameObject[] { LoadBuildModel("Stairs"), LoadBuildModel("L_Shaped_Stairs") });
        materialModels.Add(new GameObject[] { LoadBuildModel("Window") });
        materialModels.Add(new GameObject[] { LoadBuildModel("Door") });
        materialModels.Add(new GameObject[] { LoadBuildModel("Wall"), LoadBuildModel("Doorway"), LoadBuildModel("Window_Frame") });
        materialModels.Add(new GameObject[] { LoadBuildModel("Floor") });
        materialModels.Add(new GameObject[] { LoadBuildModel("Platform") });
    }

    /// <summary>
    /// 加载指定的材料icon图标.
    /// </summary>
    private Sprite LoadIcon(string name)
    {
        return Resources.Load<Sprite>("Build/MaterialIcon/" + name);
    }

    /// <summary>
    /// 加载指定的建造材料预制体.
    /// </summary>
    /// <returns></returns>
    private GameObject LoadBuildModel(string name)
    {
        return Resources.Load<GameObject>("Build/Prefabs/" + name);
    }
}
