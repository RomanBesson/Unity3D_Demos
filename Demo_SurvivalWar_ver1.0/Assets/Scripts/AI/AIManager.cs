using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 怪物AI管理脚本，挂生成点物体上
/// </summary>
public class AIManager : MonoBehaviour {

    private GameObject prefab_Cannibal;                                 //丧尸的预制体
    private GameObject prefab_Boar;                                     //野猪的预制体
    private AIManagerType aiManagerType = AIManagerType.NULL;
    private List<GameObject> AIList = new List<GameObject>();           //该生成点下生成的AI怪物集合

    private Transform[] posTransform;                                   //导航目标点位置
    private List<Vector3> posList = new List<Vector3>();                //存储目标点位置

    private int index = 0;                                              //复活后的导航点对应的序号
    public AIManagerType AIManagerType
    {
        get { return aiManagerType; }
        set { aiManagerType = value; }
    }

	void Start () {
        prefab_Cannibal = Resources.Load<GameObject>("AI/Cannibal");
        prefab_Boar = Resources.Load<GameObject>("AI/Boar");
        posTransform = transform.GetComponentsInChildren<Transform>(true);
        
        //把死亡方法添加到玩家死亡事件
        GameObject.Find("FPSController").GetComponent<PlayerController>().PlayerDeathDel += Death;

        //因为会检索到父物体，所以从1开始遍历
        for (int i = 1; i < posTransform.Length; i++)
        {
            posList.Add(posTransform[i].position);
        }
        CreateAIByEnum();
    }
	
    /// <summary>
    /// 根据不同类型生成不同怪物
    /// </summary>
    private void CreateAIByEnum()
    {
        if(aiManagerType == global::AIManagerType.CANNIBAL)
        {
            CreateAI(prefab_Cannibal, AIType.CANNIBAL);
        }
        else if(aiManagerType == global::AIManagerType.BOAR){
            CreateAI(prefab_Boar, AIType.BOAR);
        }
    }

    /// <summary>
    /// 生成对应怪物
    /// </summary>
    /// <param name="prefab_AI"></param>
    private void CreateAI(GameObject prefab_AI, AIType aiType)
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject ai = GameObject.Instantiate<GameObject>(prefab_AI, transform.position, Quaternion.identity, transform);
            //取出目标点作为导航目标点
            ai.GetComponent<AI>().Dir = posList[i];
            //设置巡逻路线
            ai.GetComponent<AI>().PosList = posList;
            //设置血量
            ai.GetComponent<AI>().Hp = 300;
            //设置攻击力
            ai.GetComponent<AI>().Attack = 100;
            //设置怪物类型
            ai.GetComponent<AI>().M_AIType = aiType;
            AIList.Add(ai);
        }
    }

    /// <summary>
    /// 怪物死亡
    /// </summary>
    /// <param name="ai"></param>
    private void AIDeath(GameObject ai)
    {
        AIList.Remove(ai);
        StartCoroutine("CreateOneAI");
    }

    /// <summary>
    /// 延迟生成新的怪物
    /// </summary>
    /// <returns></returns>
    private IEnumerator CreateOneAI()
    {   
        GameObject ai = null;
        yield return new WaitForSeconds(3);
        //按照种类再次生成
        if (aiManagerType == global::AIManagerType.CANNIBAL)
        {
            ai = GameObject.Instantiate<GameObject>(prefab_Cannibal, transform.position, Quaternion.identity, transform);
            ai.GetComponent<AI>().M_AIType = AIType.CANNIBAL;
        }
        else if (aiManagerType == global::AIManagerType.BOAR)
        {
            ai = GameObject.Instantiate<GameObject>(prefab_Boar, transform.position, Quaternion.identity, transform);
            ai.GetComponent<AI>().M_AIType = AIType.BOAR;
        }
        //重新设置导航点和巡逻路线
        ai.GetComponent<AI>().Dir = posList[index];
        ai.GetComponent<AI>().PosList = posList;
        ai.GetComponent<AI>().Hp = 300;
        ai.GetComponent<AI>().Attack = 100;
        //更新复活导航点
        index++;
        index = index % posList.Count;

        AIList.Add(ai);
    }

    /// <summary>
    /// AI角色销毁死亡.
    /// </summary>
    private void Death()
    {
        for (int i = 0; i < AIList.Count; i++)
        {
            GameObject.Destroy(AIList[i]);
        }
    }
}
