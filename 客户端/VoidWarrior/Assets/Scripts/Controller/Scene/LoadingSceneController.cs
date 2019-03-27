using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 加载场景控制器
/// </summary>
public class LoadingSceneController : MonoBehaviour
{
    #region 成员变量
    /// <summary>
    /// 进度条控制器
    /// </summary>
    [SerializeField]
    private LoadingSliderControl m_LoadingSliderControl;

    /// <summary>
    /// 异步加载标记
    /// </summary>
    private AsyncOperation m_Async = null;

    /// <summary>
    /// 当前的进度
    /// </summary>
    private int m_CurrProgress;
    #endregion

    #region 游戏流程
    void Start()
    {
        GameFacade.Instance.InitUI(); //初始化UI
        m_CurrProgress = 0; //初始化当前进度
        m_LoadingSliderControl.SetProgressValue(0); //初始化进度条
        //LayerUIMgr.Instance.Reset();
        StartCoroutine(LoadingScene()); //开始加载
    }

    void Update()
    {
        //进度数字化显示
        int toProgress = 0;

        if (m_Async.progress < 0.9f)
        {
            toProgress = Mathf.Clamp((int)m_Async.progress * 100, 1, 100);
        }
        else
        {
            toProgress = 100;
        }

        if (m_CurrProgress < toProgress)
        {
            m_CurrProgress++; //平滑显示进度
        }

        if (m_CurrProgress == 100)
        {
            m_Async.allowSceneActivation = true; //加载完成标记
        }

        m_LoadingSliderControl.SetProgressValue(m_CurrProgress * 0.01f); //使用进度条控制器显示进度
    }
    #endregion

    #region 协程
    /// <summary>
    /// 协程加载场景
    /// </summary>
    /// <returns></returns>
    private IEnumerator LoadingScene()
    {

        //根据中介者获取目前场景状态，确定加载什么场景
        string strSceneName = string.Empty;

        switch (GameFacade.Instance.currentSceneType)
        {
            case SceneType.Game:
                strSceneName = "Scene_MainGame";
                break;
            case SceneType.Menu:
                strSceneName = "Scene_Menu";
                break;
            case SceneType.Instruct:
                strSceneName = "Scene_Instruct";
                break;
        }

        //异步加载
        m_Async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(strSceneName);
        m_Async.allowSceneActivation = false;
        yield return m_Async;
    }
    #endregion
}
