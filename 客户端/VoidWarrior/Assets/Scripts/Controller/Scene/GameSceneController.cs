using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 游戏场景控制器
/// </summary>
public class GameSceneController : MonoBehaviour
{
    /// <summary>
    /// 主角出生点
    /// </summary>
    [SerializeField]
    private Transform m_PlayerBornPos;

    /// <summary>
    /// 怪物出生点
    /// </summary>
    [SerializeField]
    private Transform m_MonsterBornPos;

    #region 游戏流程
    void Awake()
    {
        GameFacade.Instance.InitUI(); //初始化UI
        GameFacade.Instance.LoadPanel(UIPanelType.Message); //加载信息面板
        GameFacade.Instance.LoadPanel(UIPanelType.Game); //加载游戏面板
        GameFacade.Instance.InitAudio();
        GameFacade.Instance.PlayBgSound(AudioManager.Sound_GameBackGround); //播放背景音乐

        //加载主角
        GameObject objMainPlayer = Resources.Load("Prefabs/Role/Prefabs/Role_MainPlayer") as GameObject;
        objMainPlayer.transform.position = m_PlayerBornPos.position;
        objMainPlayer = GameObject.Instantiate(objMainPlayer);
        RoleController mainPlayerCtrl = objMainPlayer.GetComponent<RoleController>();
        mainPlayerCtrl.Init(RoleType.MainPlayer, 
                            new RoleInfo(true),
                            new RoleMainPlayerAI(objMainPlayer.GetComponent<RoleController>()));

        //加载怪物
        GameObject objMonster = Resources.Load("Prefabs/Role/Prefabs/Role_Monster") as GameObject;
        objMonster.transform.position = m_MonsterBornPos.position;
        GameObject objMonster1 = GameObject.Instantiate(objMonster);
        GameObject objMonster2 = GameObject.Instantiate(objMonster);
        RoleController monsterCtrl = objMonster1.GetComponent<RoleController>();
        monsterCtrl.Init(RoleType.Monster, 
                         new RoleInfo(true), 
                         new RoleMonsterAI(objMonster1.GetComponent<RoleController>()));
        monsterCtrl = objMonster2.GetComponent<RoleController>();
        monsterCtrl.Init(RoleType.Monster,
                         new RoleInfo(true),
                         new RoleMonsterAI(objMonster2.GetComponent<RoleController>()));
    }
    #endregion
}
