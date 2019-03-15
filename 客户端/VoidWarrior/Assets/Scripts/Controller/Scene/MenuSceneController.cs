using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 游戏菜单控制器
/// </summary>
public class MenuSceneController : MonoBehaviour
{
    #region 游戏流程
    void Start ()
    {
        GameFacade.Instance.InitUI(); //初始化UI
        GameFacade.Instance.LoadPanel(UIPanelType.Menu); //加载菜单面板
        GameFacade.Instance.InitAudio();
        GameFacade.Instance.PlayBgSound(AudioManager.Sound_MenuBackGround); //播放背景音乐
    }
    #endregion
}
