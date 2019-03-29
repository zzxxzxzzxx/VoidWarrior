using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDropController : MonoBehaviour
{
    /// <summary>
    /// 游戏控制器
    /// </summary>
    private MainGameController mainGameController;
    private InstructController instructController;
    /// <summary>
    /// 增加的时间
    /// </summary>
    [SerializeField]
    private int addTime = 0;

    private void Start()
    {
        setController();
    }

    void setController()
    {
        if (GameFacade.Instance.currentSceneType.Equals(SceneType.Instruct))
        {
            instructController = GameObject.Find("InstructController").GetComponent<InstructController>();
        }
        else
        {
            mainGameController = GameObject.Find("MainGameController").GetComponent<MainGameController>();
        }
    }
    #region 游戏流程
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //改变游戏状态
            other.GetComponent<RoleController>().currRoleInfo.HealthPoint += addTime; //增加时间
            addTimes(addTime); //增加生命
            GameObject.Destroy(this.gameObject); //销毁自身游戏物体
        }
    }

    void addTimes(int addTime)
    {
        if (GameFacade.Instance.currentSceneType.Equals(SceneType.Instruct))
            instructController.AddTime(addTime);
        else if (GameFacade.Instance.currentSceneType.Equals(SceneType.Game))
            mainGameController.AddTime(addTime);
    }
    #endregion
}
