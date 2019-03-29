using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 主游戏控制器
/// </summary>
public class MainGameController : MonoBehaviour
{
    #region 成员变量
    /// <summary>
    /// 记录得分
    /// </summary>
    [SerializeField]
    private int score;

    /// <summary>
    /// 倒计时
    /// </summary>
    private float timeCountDown;

    /// <summary>
    /// 游戏面板
    /// </summary>
    private GamePanel gamePanel;

    /// <summary>
    /// 提交分数申请
    /// </summary>
    [SerializeField]
    private SubmitRequest submitRequest;
    #endregion

    #region 游戏流程
    private void Start()
    {
        GameObject obj = GameObject.Find("GamePanel(Clone)") as GameObject;
        gamePanel = obj.GetComponent<GamePanel>();
    }

    private void Update()
    {
        //更新标记未更新，状态进行了改变
        if (!GameFacade.Instance.gameUIUpdate &&
            GameFacade.Instance.currentGameState.Equals(GameStateType.Start))
        {
            ToGameStart();
        }

        if (GameFacade.Instance.currentGameState.Equals(GameStateType.Gaming))         
        {
            if (!GameFacade.Instance.gameUIUpdate)
            {
                ToGaming();
            }
            else if (timeCountDown <= 0)
            {
                ToGameOver();
            }
            else
            {
                timeCountDown -= Time.deltaTime;
                gamePanel.SetTimeText(((int)timeCountDown).ToString());
            }

        }

        if (!GameFacade.Instance.gameUIUpdate &&
            GameFacade.Instance.currentGameState.Equals(GameStateType.GameOver))
        {
            ToGameOver();
        }

    }
    #endregion

    #region 提供的方法
    /// <summary>
    /// 增加分数
    /// </summary>
    public void AddScore(int score)
    {
        this.score += score;
        gamePanel.SetScoreText(this.score.ToString()); //改变ui
    }
    /// <summary>
    /// 增加时间
    /// </summary>
    public void AddTime(int time)
    {
        timeCountDown += time;
        gamePanel.SetTimeText(timeCountDown.ToString()); //改变ui
    }
    #endregion

    #region 私有方法
    /// <summary>
    /// ui切换到游戏开始
    /// </summary>
    private void ToGameStart()
    {
        score = 0; //分数重置
        gamePanel.SetScoreText(score.ToString()); //刷新ui

        timeCountDown = 3; //倒计时设置

        //按钮显示设置
        gamePanel.SetGameForgiveActive(false);
        gamePanel.SetGameOverActive(false);

        GameFacade.Instance.gameUIUpdate = true; //刷新标记更新

        StartCoroutine(TimeCountDown()); //倒计时协程
    }

    /// <summary>
    /// ui切换到游戏中
    /// </summary>
    private void ToGaming()
    {
        score = 0; //分数重置
        timeCountDown = 200; //设置时间
        //刷新ui
        gamePanel.SetScoreText(score.ToString());
        gamePanel.SetTimeText(((int)timeCountDown).ToString());

        //按钮显示设置
        gamePanel.SetGameForgiveActive(true);
        gamePanel.SetGameOverActive(false);

        GameFacade.Instance.gameUIUpdate = true;//刷新标记更新
    }

    /// <summary>
    /// ui切换到游戏结束
    /// </summary>
    private void ToGameOver()
    {
        //按钮显示设置
        gamePanel.SetGameForgiveActive(false);
        gamePanel.SetGameOverActive(true);
        submitRequest.SendRequest(GameFacade.Instance.GetUserName(), score); //更新排名

        GameFacade.Instance.gameUIUpdate = true;//刷新标记更新
    }
    #endregion

    #region 协程
    /// <summary>
    /// 倒计时协程
    /// </summary>
    /// <returns></returns>
    private IEnumerator TimeCountDown()
    {
        for (int timer = (int)timeCountDown; timer > 0; timer--)
        {
            GameFacade.Instance.ShowMessage(timer.ToString()); //倒计时显示
            GameFacade.Instance.PlayNormalSound(AudioManager.Sound_Timer);
            yield return new WaitForSeconds(2f);
        }

        //更新游戏状态
        GameFacade.Instance.currentGameState = GameStateType.Gaming;
        GameFacade.Instance.gameUIUpdate = false;
    }
    #endregion
}
