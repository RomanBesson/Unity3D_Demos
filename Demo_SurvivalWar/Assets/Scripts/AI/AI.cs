using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 怪物预制体身上挂载的类
/// </summary>
public class AI : MonoBehaviour {

    private NavMeshAgent m_navMeshAgent;
    private Animator m_Animator;
    private GameObject prefab_Effect;
    private AIRagdoll m_AIRagdoll = null;                     //根据是否是人形，添加布娃娃

    private Transform player_Transform;                       //玩家位置
    private PlayerController m_PlayerController;              //玩家控制脚本

    private Vector3 dir;
    /// <summary>
    /// 初始导航目标点
    /// </summary>
    public Vector3 Dir                                         //初始导航目标点
    {
        get { return dir; }
        set { dir = value; }
    }

    private AIState m_AIState;                                 //现在处于的动作状态
    /// <summary>
    /// 现在处于的动作状态
    /// </summary>
    public AIState M_AIState
    {
        get { return m_AIState; }
        set { m_AIState = value; }
    }

    private List<Vector3> posList = new List<Vector3>();       //巡逻的几个点位
    /// <summary>
    /// 巡逻的几个点位的集合
    /// </summary>
    public List<Vector3> PosList
    {
        get { return posList; }
        set { posList = value; }
    }

    private int hp;                                             //生命值
    /// <summary>
    /// 当前生命值
    /// </summary>
    public int Hp
    {
        get { return hp; }
        set
        {
            hp = value;
            //hp为0切换成死亡状态
            if (hp <= 0) ToggleState(AIState.DEATH);
        }
    }

    private int attack;                                          //攻击力
    /// <summary>
    /// 攻击力
    /// </summary>
    public int Attack
    {
        get { return attack; }
        set { attack = value; }
    }

    private AIType m_AIType;                                      //怪物种类
    /// <summary>
    /// 怪物种类
    /// </summary>
    public AIType M_AIType
    {
        get { return m_AIType; }
        set { m_AIType = value; }
    }

    private void Awake()
    {
        player_Transform = GameObject.Find("FPSController").GetComponent<Transform>();
        m_PlayerController = player_Transform.GetComponent<PlayerController>();
        prefab_Effect = Resources.Load<GameObject>("Effects/Gun/Bullet Impact FX_Flesh");
        m_Animator = gameObject.GetComponent<Animator>();
        m_navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        m_navMeshAgent.SetDestination(dir);
        //设置默认处于的状态
        m_AIState = AIState.IDLE;
        if (m_AIType == AIType.CANNIBAL)
        {
            m_AIRagdoll = gameObject.GetComponent<AIRagdoll>();
        }
    }

    void Update()
    {
        AIFollowPlayer();
        Distance();
        AIAttackPlayer();
    }


    /// <summary>
    /// 巡逻状态下的默认状态和行走状态的切换
    /// </summary>
    private void Distance()
    {
        //处于巡逻状态时
        if (m_AIState == AIState.IDLE || m_AIState == AIState.WALK)
        {
            //当前导航即将完毕
            if (Vector3.Distance(transform.position, dir) <= 1)
            {
                //随机下一个导航点进行导航
                int index = Random.Range(0, posList.Count);
                dir = posList[index];
                m_navMeshAgent.SetDestination(dir);
                //切换成默认状态
                ToggleState(AIState.IDLE);
            }
            else
            {
                //切换成行走状态
                ToggleState(AIState.WALK);
            }
        }
            
    }

    /// <summary>
    /// 一定距离内AI角色跟随玩家角色.
    /// </summary>
    private void AIFollowPlayer()
    {
        if (Vector3.Distance(transform.position, player_Transform.position) <= 20)
        {
            //跟随玩家角色.
            ToggleState(AIState.ENTERRUN);
        }
        else
        {
            //不再跟随玩家角色.
            ToggleState(AIState.EXITRUN);
        }
    }

    /// <summary>
    /// 一定距离内AI角色攻击玩家角色.
    /// </summary>
    private void AIAttackPlayer()
    {
        if (m_AIState == AIState.ENTERRUN)
        {
            if (Vector3.Distance(transform.position, player_Transform.position) <= 2)
            {
                //角色需要进入攻击状态.
                ToggleState(AIState.ENTERATTACK);
            }
            else
            {
                //角色退出攻击状态(恢复奔跑状态).
                ToggleState(AIState.EXITATTACK);
            }
        }
    }

    /// <summary>
    /// 状态切换方法.
    /// </summary>
    private void ToggleState(AIState aiState)
    {
        switch (aiState)
        {
            //切换成默认状态
            case AIState.IDLE:
                IdleState();
                break;
            //切换成行走状态
            case AIState.WALK:
                WalkState();
                break;
            //切换成追击状态
            case AIState.ENTERRUN:
                EnterRunState();
                break;
            //退出追击状态
            case AIState.EXITRUN:
                ExitRunState();
                break;
            //进入攻击状态
            case AIState.ENTERATTACK:
                EnterAttackState();
                break;
            //退出攻击状态
            case AIState.EXITATTACK:
                ExitAttackState();
                break;
            //角色死亡
            case AIState.DEATH:
                DeathState();
                break;
        }
    }

    #region 切换到各个状态
    /// <summary>
    /// 切换成行走状态.
    /// </summary>
    private void WalkState()
    {
        m_Animator.SetBool("Walk", true);
        m_AIState = AIState.WALK;
    }

    /// <summary>
    /// 切换成默认状态.
    /// </summary>
    private void IdleState()
    {
        m_Animator.SetBool("Walk", false);
        m_AIState = AIState.IDLE;
    }

    /// <summary>
    /// 进行奔跑状态.
    /// </summary>
    private void EnterRunState()
    {
        m_Animator.SetBool("Run", true);
        m_AIState = AIState.ENTERRUN;
        m_navMeshAgent.speed = 2;
        m_navMeshAgent.enabled = true;
        //设置目标点为玩家位置
        m_navMeshAgent.SetDestination(player_Transform.position);
    }

    /// <summary>
    /// 退出奔跑状态.
    /// </summary>
    private void ExitRunState()
    {
        m_Animator.SetBool("Run", false);
        ToggleState(AIState.WALK);
        m_navMeshAgent.speed = 0.8f;
        //设置目标点为巡逻点
        m_navMeshAgent.SetDestination(dir);
    }


    /// <summary>
    /// 进入攻击状态.
    /// </summary>
    private void EnterAttackState()
    {
        m_Animator.SetBool("Attack", true);
        m_navMeshAgent.enabled = false;
        m_AIState = AIState.ENTERATTACK;
    }

    /// <summary>
    /// 退出攻击状态.
    /// </summary>
    private void ExitAttackState()
    {
        m_Animator.SetBool("Attack", false);
        m_navMeshAgent.enabled = true;
        //回到追击状态
        ToggleState(AIState.ENTERRUN);
    }

    /// <summary>
    /// 头部受伤.
    /// </summary>
    private void HitHard()
    {
        m_Animator.SetTrigger("HitHard");
    }

    /// <summary>
    /// 其他部位受伤.
    /// </summary>
    private void HitNormal()
    {
        m_Animator.SetTrigger("HitNormal");
    }

    /// <summary>
    /// 死亡状态.
    /// </summary>
    private void DeathState()
    {
        m_AIState = AIState.DEATH;
        //停止导航
        //m_navMeshAgent.Stop();
        m_Animator.SetTrigger("Death");

        //非人形播放死亡动画
        if (m_AIType == AIType.BOAR)
        {
            m_Animator.SetTrigger("Death");
        }
        //人形进行布娃娃死亡模拟
        else if (m_AIType == AIType.CANNIBAL)
        {
            m_Animator.enabled = false;
            m_AIRagdoll.StartRagdoll();
        }
        //复活重置
        StartCoroutine(Death());
    }

    /// <summary>
    /// 怪物死亡重置方法
    /// </summary>
    private IEnumerator Death()
    {
        yield return new WaitForSeconds(2);
        GameObject.Destroy(gameObject);
        SendMessageUpwards("AIDeath", gameObject);
    }

    #endregion

    /// <summary>
    /// 播放特效.
    /// </summary>
    public void PlayerEffect(RaycastHit hit)
    {
        GameObject effect = GameObject.Instantiate<GameObject>(prefab_Effect, hit.point, Quaternion.LookRotation(hit.normal));
        GameObject.Destroy(effect, 3);
    }

    /// <summary>
    /// 头部受伤害.
    /// </summary>
    public void HeadHit(int value)
    {
        HitHard();
        Hp -= value;
        Debug.Log("头部受伤害.");
        PlayHitAudio();
    }

    /// <summary>
    /// 身体受伤害.
    /// </summary>
    public void NormalHit(int value)
    {
        HitNormal();
        Hp -= value;
        Debug.Log("身体受伤害.");
        PlayHitAudio();
    }

    /// <summary>
    /// 攻击玩家角色.
    /// </summary>
    private void AttackPlayer()
    {
        //玩家脚本扣除生命值
        m_PlayerController.CutHP(this.Attack);
        //播放攻击音效
        if (m_AIType == AIType.CANNIBAL)
        {
            AudioManager.Instance.PlayAudioClipByName(ClipName.ZombieAttack, transform.position);
        }
        else if (m_AIType == AIType.BOAR)
        {
            AudioManager.Instance.PlayAudioClipByName(ClipName.BoarAttack, transform.position);
        }
    }

    /// <summary>
    /// 播放受伤音效.
    /// </summary>
    private void PlayHitAudio()
    {
        if (m_AIType == AIType.CANNIBAL)
        {
            AudioManager.Instance.PlayAudioClipByName(ClipName.ZombieInjured, transform.position);
        }
        else if (m_AIType == AIType.BOAR)
        {
            AudioManager.Instance.PlayAudioClipByName(ClipName.BoarInjured, transform.position);
        }

    }

}
