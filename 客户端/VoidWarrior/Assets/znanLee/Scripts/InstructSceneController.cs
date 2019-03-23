using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 教学场景控制器
/// </summary>

public class InstructSceneController : MonoBehaviour {
    /// <summary>
    /// 主角出生点
    /// </summary>
    [SerializeField]
    private Transform m_PlayerBornPos;

    /// <summary>
    /// 怪物出生点
    /// </summary>
    [SerializeField]
    private Transform m_MonsterBornPos1;
    [SerializeField]
    private Transform m_MonsterBornPos2;
   // private Transform m_MonsterBornPos3;

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
        GameObject objMonster1 = Resources.Load("Prefabs/Role/Prefabs/Role_Monster2") as GameObject;
        objMonster1.transform.position = m_MonsterBornPos1.position;
        GameObject obj1 = GameObject.Instantiate(objMonster1);
        RoleController monsterCtrl1 = obj1.GetComponent<RoleController>();
        monsterCtrl1.Init(RoleType.Monster,
                         new RoleInfo(true),
                         new RoleMonsterAI(obj1.GetComponent<RoleController>()));

        //GameObject objMonster2 = Resources.Load("Prefabs/Role/Prefabs/Role_Monster") as GameObject;
        //objMonster2.transform.position = m_MonsterBornPos2.position;
        //GameObject obj2 = GameObject.Instantiate(objMonster2);
        //RoleController monsterCtrl2 = obj2.GetComponent<RoleController>();
        //monsterCtrl2.Init(RoleType.Monster,
        //                 new RoleInfo(true),
        //                 new RoleMonsterAI(obj2.GetComponent<RoleController>()));
    }
    #endregion
}
