using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 音频管理器
/// </summary>
public class AudioManager : MonoBehaviour {

    public static AudioManager Instance;
    private AudioClip[] audioClip;                                    //已加载的音频资源
    private Dictionary<string, AudioClip> audioClipDic;               //音频资源字典

    void Awake()
    {
        Instance = this;
        audioClipDic = new Dictionary<string, AudioClip>();
        audioClip = Resources.LoadAll<AudioClip>("Audios/All/");

        for (int i = 0; i < audioClip.Length; i++)
        {
            audioClipDic.Add(audioClip[i].name, audioClip[i]);
        }
    }

    /// <summary>
    /// 通过音频名称获取音频
    /// </summary>
    /// <param name="clipName"></param>
    /// <returns></returns>
    public AudioClip GetAudioClipByName(ClipName clipName)
    {
        //在字典里查找对应音频
        AudioClip tempClip;
        audioClipDic.TryGetValue(clipName.ToString(), out tempClip);
        return tempClip;
    }

    /// <summary>
    /// 在对应位置播放音频
    /// </summary>
    /// <param name="clipName"></param>
    /// <param name="position"></param>
    public void PlayAudioClipByName(ClipName clipName, Vector3 position)
    {
        AudioSource.PlayClipAtPoint(GetAudioClipByName(clipName), position);
    }

    /// <summary>
    /// 给游戏对象添加音频组件
    /// </summary>
    /// <param name="go">游戏对象</param>
    /// <param name="clipName">要播放的音频片段名称</param>
    /// <param name="playOnAwake">是否立即播放</param>
    /// <param name="loop">是否循环</param>
    /// <returns></returns>
    public AudioSource AddAudioSourceComponent(GameObject go, ClipName clipName, bool playOnAwake = true, bool loop = true)
    {
        AudioSource tempAudioSource = go.AddComponent<AudioSource>();
        tempAudioSource.clip = GetAudioClipByName(clipName);
        tempAudioSource.playOnAwake = playOnAwake;
        if (playOnAwake) tempAudioSource.Play();
        tempAudioSource.loop = loop;
        return tempAudioSource;
    }
}
