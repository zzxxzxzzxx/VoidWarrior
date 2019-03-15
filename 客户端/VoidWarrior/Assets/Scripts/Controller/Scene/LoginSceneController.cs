using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginSceneController : MonoBehaviour
{
    #region 游戏流程
    void Start()
    {
        GameFacade.Instance.InitUI(); //初始化UI
        GameFacade.Instance.InitAudio(); //初始化声音
        GameFacade.Instance.PlayBgSound(AudioManager.Sound_LoginBackGround); //播放登录背景音乐
        GameFacade.Instance.LoadPanel(UIPanelType.Message); //加载消息面板
        //移动消息面板到合适位置
        GameObject.Find("Canvas/MessagePanel(Clone)").GetComponent<Transform>().Translate(0, 130, 0);
        GameFacade.Instance.LoadPanel(UIPanelType.Login); //加载登录面板
    }
    #endregion
}
