using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// AI的总管理脚本，挂AI总管理物体上
/// </summary>
public class AIManagers : MonoBehaviour {

    private Transform[] points;

	void Start () {
        points = transform.GetComponentsInChildren<Transform>();
        CreateAIManager();
	}

    /// <summary>
    /// 给生成点位物体挂载管理脚本
    /// </summary>
    private void CreateAIManager()
    {
        for (int i = 1; i < points.Length; i++)
        {
            if(i % 2 == 0)
            {
                points[i].gameObject.AddComponent<AIManager>().AIManagerType = AIManagerType.CANNIBAL;
            }
            else
            {
                points[i].gameObject.AddComponent<AIManager>().AIManagerType = AIManagerType.BOAR;
            }

        }
    }

}
