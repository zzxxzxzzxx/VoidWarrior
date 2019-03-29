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
    /// <summary>
    /// 怪物预制体路径
    /// </summary>
    private string monsterurl = "Prefabs/Role/Prefabs/Role_Monster";
    private RoleController mainPlayerCtrl;
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
                            new RoleInfo(true, 1000, 10),
                            new RoleMainPlayerAI(objMainPlayer.GetComponent<RoleController>()));

        loadMonsters();
    }

    public void loadMonsters()
    {
        //加载怪物
        Instantiate(m_MonsterBornPos1, monsterurl + "1", RoleType.TimeMonster);
        Instantiate(m_MonsterBornPos2, monsterurl + "2", RoleType.WeaponMonster);
    }
    #endregion

    private void Instantiate(Transform transform, string path,RoleType roleType)
    {
        GameObject objMonster = Resources.Load(path.ToString()) as GameObject;
        objMonster.transform.position = transform.position;
        GameObject obj = GameObject.Instantiate(objMonster);
        RoleController monsterCtrl = obj.GetComponent<RoleController>();
        monsterCtrl.Init(roleType,
                         new RoleInfo(true, 10, 3, 10),
                         new MainGameRoleMonsterAI(obj.GetComponent<RoleController>(), mainPlayerCtrl));
        monsterCtrl.lockEnemy = GameObject.Find("Role_MainPlayer(Clone)").GetComponent<RoleController>();
    }
}


