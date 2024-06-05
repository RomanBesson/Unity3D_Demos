using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 左侧标签页对应内容物体上挂载
/// </summary>
public class CraftingContentController : MonoBehaviour
{
    private int index = -1; //自己的序号
    private CraftingContentItemController current = null; //保存当前亮的选项卡

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="index"></param>
    /// <param name="prefab"></param>
    public void InitContent(int index, GameObject prefab, List<CraftingContentItem> strList)
    {
        this.index = index;
        gameObject.name = "Content" + index;
        CrateAllItems(prefab, strList);
    }

    /// <summary>
    /// 创建他下面的子项目
    /// </summary>
    /// <param name="count"></param>
    /// <param name="prefab"></param>
    private void CrateAllItems(GameObject prefab, List<CraftingContentItem> strList)
    {
        for (int i = 0; i < strList.Count; i++)
        {
            GameObject go = GameObject.Instantiate<GameObject>(prefab, transform);
            go.GetComponent<CraftingContentItemController>().Init(strList[i]);

        }
    }


    /// <summary>
    /// 正文区域标题元素状态切换.
    /// </summary>
    private void ResetItemState(CraftingContentItemController item)
    {
        //避免重复点击引起的不必要调用问题
        if (current == item) return;
        if (current != null)
        {
            //把上一个选项卡关了
            current.NormalItem();
        }
        //激活新的
        item.ActiveItem();
        current = item;
        SendMessageUpwards("CreateSlotContents", item.Id);
    }

}
