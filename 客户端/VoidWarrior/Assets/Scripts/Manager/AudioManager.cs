using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// AudioManager
/// 声音管理器，继承自BaseManager（管理器基类）
/// </summary>
public class AudioManager : BaseManager
{
    #region 构造方法
    /// <summary>
    /// 构造方法，获取facade
    /// </summary>
    /// <param name="facade">facade中介者</param>
    public AudioManager(GameFacade facade) : base(facade) { }
    #endregion

    #region 成员变量
    /// <summary>
    /// 声音路径
    /// </summary>
    private const string Sound_Prefix = "Sounds/";

    #region 声音名字
    /// <summary>
    /// 声音Alert的名字
    /// </summary>
    public const string Sound_Timer = "Timer";

    /// <summary>
    /// 声音Shoot的名字
    /// </summary>
    public const string Sound_Shoot = "Shoot";

    /// <summary>
    /// 声音MenuBackGround的名字
    /// </summary>
    public const string Sound_LoginBackGround = "LoginBackGround";

    /// <summary>
    /// 声音MenuBackGround的名字
    /// </summary>
    public const string Sound_MenuBackGround = "MenuBackGround";

    /// <summary>
    /// 声音GameBackGround的名字
    /// </summary>
    public const string Sound_GameBackGround = "GameBackGround";

    /// <summary>
    /// 声音ButtonClick的名字
    /// </summary>
    public const string Sound_ButtonClick = "ButtonClick";

    /// <summary>
    /// 声音Explosion的名字
    /// </summary>
    public const string Sound_Explosion = "Explosion";
    #endregion

    /// <summary>
    /// 背景声音
    /// </summary>
    private AudioSource bgAudioSource;

    /// <summary>
    /// 普通声音
    /// </summary>
    private AudioSource normalAudioSource;
    #endregion

    #region 提供的方法
    /// <summary>
    /// 重构初始化方法
    /// </summary>
    public override void OnInit()
    {
        if (GameObject.Find("AudioSource(GameObject)") == null)
        {
            GameObject audioSourceGO = new GameObject("AudioSource(GameObject)"); //创建游戏物体AudioSource(GameObject)
            bgAudioSource = audioSourceGO.AddComponent<AudioSource>(); //给游戏物体附加背景声音
            normalAudioSource = audioSourceGO.AddComponent<AudioSource>(); //给游戏物体附加普通声音
        }

        //PlaySound(bgAudioSource, LoadSound(Sound_MenuBackGround), 0.5f, true); //播放背景声音
    }

    /// <summary>
    /// 播放背景声音
    /// </summary>
    /// <param name="soundName">声音名字</param>
    public void PlayBgSound(string soundName)
    {
        PlaySound(bgAudioSource, LoadSound(soundName), 0.5f, true);
    }

    /// <summary>
    /// 播放普通声音
    /// </summary>
    /// <param name="soundName">声音名字</param>
    public void PlayNormalSound(string soundName)
    {
        PlaySound(normalAudioSource, LoadSound(soundName), 1f);
    }
    #endregion

    #region 私有方法
    /// <summary>
    /// 播放声音
    /// </summary>
    /// <param name="audioSource">声音源组件（背景或者普通）</param>
    /// <param name="clip">声音文件</param>
    /// <param name="volume">声音大小</param>
    /// <param name="loop">是否循环</param>
    private void PlaySound(AudioSource audioSource, AudioClip clip, float volume, bool loop = false)
    {
        //设置声音属性
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.loop = loop;
        audioSource.Play();
    }

    /// <summary>
    /// 加载声音文件
    /// </summary>
    /// <param name="soundsName">声音名字</param>
    /// <returns>声音文件</returns>
    private AudioClip LoadSound(string soundsName)
    {
        return Resources.Load<AudioClip>(Sound_Prefix + soundsName); //资源加载声音，路径为声音路径加声音名字
    }
    #endregion
}
