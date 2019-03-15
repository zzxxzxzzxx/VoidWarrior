using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// GamePanel
/// 游戏面板，继承自BasePanel（面板基类）
/// </summary>
public class GamePanel : BasePanel
{
    #region 成员变量
    /// <summary>
    /// 显示得分的文本
    /// </summary>
    [SerializeField]
    private Text scoreText;

    /// <summary>
    /// 显示倒计时的文本
    /// </summary>
    [SerializeField]
    private Text TimeText;

    /// <summary>
    /// 中途放弃按钮
    /// </summary>
    [SerializeField]
    private Button gameForgiveButton;

    /// <summary>
    /// 游戏放弃按钮
    /// </summary>
    [SerializeField]
    private Button gameOverButton;
    #endregion

    #region 游戏流程
    private void Start()
    {
        //给按钮添加监听
        gameForgiveButton.onClick.AddListener(OnForgiveClick);
        gameOverButton.onClick.AddListener(OnGameOverClick);
    }
    #endregion

    #region UI管理器流程
    /// <summary>
    /// OnEnter
    /// 重写BasePanel中的进入
    /// </summary>
    public override void OnEnter()
    {
        base.OnEnter();
        gameObject.SetActive(true);
    }

    /// <summary>
    /// OnExit
    /// 重写BasePanel中的退出
    /// </summary>
    public override void OnExit()
    {
        base.OnExit();
        uiMng.PopPanel();
        gameObject.SetActive(false);
    }
    #endregion

    #region 提供的方法
    /// <summary>
    /// 设置显示的分数
    /// </summary>
    public void SetScoreText(string score)
    {
        scoreText.text = score; //更新分数
    }

    /// <summary>
    /// 设置倒计时
    /// </summary>
    /// <param name="time">倒计时</param>
    public void SetTimeText(string time)
    {
        TimeText.text = time;
    }

    /// <summary>
    /// 设置放弃按钮的状态
    /// </summary>
    /// <param name="active">状态</param>
    public void SetGameForgiveActive(bool active)
    {
        gameForgiveButton.gameObject.SetActive(active);
    }

    /// <summary>
    /// 设置结束按钮的状态
    /// </summary>
    /// <param name="active">状态</param>
    public void SetGameOverActive(bool active)
    {
        gameOverButton.gameObject.SetActive(active);
    }
    #endregion

    #region 私有方法
    /// <summary>
    /// 放弃按钮监听
    /// </summary>
    private void OnForgiveClick()
    {
        GameFacade.Instance.PlayNormalSound(AudioManager.Sound_ButtonClick);
        GameFacade.Instance.LoadToMenu(); //加载场景
    }

    /// <summary>
    /// 游戏结束监听
    /// </summary>
    private void OnGameOverClick()
    {
        GameFacade.Instance.PlayNormalSound(AudioManager.Sound_ButtonClick);
        GameFacade.Instance.LoadToMenu(); //加载场景
    }
    #endregion
}
