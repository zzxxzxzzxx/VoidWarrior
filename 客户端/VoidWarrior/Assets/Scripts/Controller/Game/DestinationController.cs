using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 终点控制器
/// </summary>
public class DestinationController : MonoBehaviour
{
    #region 游戏流程
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //改变游戏状态
            GameFacade.Instance.currentGameState = GameStateType.Victory;
            GameFacade.Instance.gameUIUpdate = false;
        }
    }
    #endregion
}
