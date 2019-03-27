using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDropController : MonoBehaviour
{
    /// <summary>
    /// 游戏控制器
    /// </summary>
    private MainGameController gameController;

    /// <summary>
    /// 增加的时间
    /// </summary>
    [SerializeField]
    private int addTime = 0;
    #region 游戏流程
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameController = GameObject.Find("MainGameController").GetComponent<MainGameController>();
            //改变游戏状态
            gameController.AddTime(addTime);
            GameObject.Destroy(this.gameObject); //销毁自身游戏物体
        }
    }
    #endregion
}
