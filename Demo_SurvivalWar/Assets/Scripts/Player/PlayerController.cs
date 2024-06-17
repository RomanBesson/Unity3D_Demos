using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

/// <summary>
/// 玩家角色死亡状态委托.
/// </summary>
public delegate void PlayerDeathDelegate();

/// <summary>
/// 玩家控制脚本
/// </summary>
public class PlayerController : MonoBehaviour {

    private FirstPersonController FPC;
    private PlayerInfoPanel m_PlayerInfoPanel;                     //体力值血量显示脚本
    private BloodScreenPanel m_BloodScreenPanel;                   //血屏显示脚本

    private int hp = 1000;                                         //血量
    private int vit = 100;                                         //体力值.
    private int index = 0;                                         //计时角标(用于标记奔跑的时长，计算扣除的体力)

    private AudioSource m_AudioSource;                             //用于播放呼吸声
    private bool audioIsPlay = false;                              //呼吸声是否播放的标志位

    private bool isDeath = false;                                  //玩家角色死亡标志位.
    public event PlayerDeathDelegate PlayerDeathDel;               //死亡事件.

    /// <summary>
    /// 生命值.
    /// </summary>
    public int HP
    {
        get { return hp; }
        set { hp = value; }
    }

    /// <summary>
    /// 体力值
    /// </summary>
    public int VIT
    {
        get { return vit; }
        set { vit = value; }
    }

	void Start () {
        FPC = gameObject.GetComponent<FirstPersonController>();
        m_PlayerInfoPanel = GameObject.Find("Canvas/MainPanel/PlayerInfoPanel").GetComponent<PlayerInfoPanel>();
        m_BloodScreenPanel = GameObject.Find("Canvas/MainPanel/BloodScreenPanel").GetComponent<BloodScreenPanel>();
        m_AudioSource = AudioManager.Instance.AddAudioSourceComponent(gameObject, ClipName.PlayerBreathingHeavy, false);

        StartCoroutine(RestoreVIT());
	}
	

	void Update () {
        CutVIT();
	}

    /// <summary>
    /// 生命值削减.
    /// </summary>
    public void CutHP(int HPvalue)
    {
        this.HP -= HPvalue;

        //显示血量下降
        m_PlayerInfoPanel.SetHP(this.HP);

        //血屏效果显现
        // m_BloodScreenPanel.SetImageAlpha(true);

        //播放受伤音效
        AudioManager.Instance.PlayAudioClipByName(ClipName.PlayerHurt, transform.position);
        
        //如果玩家死亡
        if (this.HP <= 0 && isDeath == false)
        {
            PlayerDeath();
        }

    }

    /// <summary>
    /// 体力值削减.
    /// </summary>
    public void CutVIT()
    {
        //奔跑消耗体力
        if (FPC.M_PlayerState == PlayerState.RUN)
        {
            index++;
            if (index >= 20)
            {
                this.VIT -= 2;
                ResetSpeed();
                index = 0; 
            }
        }
        //体里小于50，还没播放呼吸声时
        if (this.VIT < 50 && audioIsPlay == false)
        {
            m_AudioSource.Play();
            audioIsPlay = true;
            Debug.Log("呼吸声开始播放.");
        }
        m_PlayerInfoPanel.SetVIT(this.VIT);
    }


    /// <summary>
    /// 体力值自动恢复.
    /// </summary>
    private IEnumerator RestoreVIT()
    {
        Vector3 tempPos;
        while(true)
        {
            tempPos = transform.position;
            yield return new WaitForSeconds(1);
            if (this.VIT <= 95 && transform.position == tempPos)
            {
                //体力恢复到50以上，呼吸声停止播放
                if (this.VIT > 50 && audioIsPlay == true)
                {
                    m_AudioSource.Stop();
                    audioIsPlay = false;
                    Debug.Log("呼吸声停止播放.");
                }

                this.VIT += 5;
                m_PlayerInfoPanel.SetVIT(this.VIT);
                ResetSpeed();
            }
        }
    }


    /// <summary>
    /// 重置玩家角色的行走/奔跑速度.
    /// </summary>
    private void ResetSpeed()
    {
        //新的移动/奔跑速度= 原始默认速度 * (VIT * 0.01f);
        FPC.M_WalkSpeed = 5 * (this.VIT * 0.01f);
        FPC.M_RunSpeed = 10 * (this.VIT * 0.01f);
    }

    /// <summary>
    /// 玩家角色死亡时的设置
    /// </summary>
    private void PlayerDeath()
    {
        //标记为死亡
        isDeath = true;

        //播放死亡音效
        AudioManager.Instance.PlayAudioClipByName(ClipName.PlayerDeath, transform.position);

        //玩家控制脚本取消激活
        transform.GetComponent<FirstPersonController>().enabled = false;

        //输入管理器取消激活
        GameObject.Find("Managers").GetComponent<InputManager>().enabled = false;

        //运行死亡事件
        PlayerDeathDel();

        //跳转到重来的场景
        StartCoroutine("JumpScene");
    }

    /// <summary>
    /// 跳转到重来场景.
    /// </summary>
    private IEnumerator JumpScene()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("ResetScene");
    }

}
