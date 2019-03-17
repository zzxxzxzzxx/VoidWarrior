using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

/// <summary>
/// 场景管理器
/// </summary>
public class SceneManager : BaseManager
{
    #region 构造方法
    /// <summary>
    /// 构造方法
    /// </summary>
    /// <param name="facade">中介者</param>
    public SceneManager(GameFacade facade) : base(facade) { }
    #endregion

    #region 提供的方法
    /// <summary>
    /// 加载登录场景
    /// </summary>
    public void LoadToLogin()
    {
        facade.currentSceneType = SceneType.Login;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scene_Login");
    }

    /// <summary>
    /// 加载到菜单场景
    /// </summary>
    public void LoadToMenu()
    {
        facade.currentSceneType = SceneType.Menu;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scene_Loading");
    }

    /// <summary>
    /// 加载教学场景
    /// </summary>
    public void LoadToInstruct()
    {
        facade.currentSceneType = SceneType.Instruct;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scene_Instruct");
    }

    /// <summary>
    /// 加载到游戏场景
    /// </summary>
    public void LoadToGame()
    {
        facade.currentSceneType = SceneType.Game;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scene_Loading");
    }
    #endregion
}
