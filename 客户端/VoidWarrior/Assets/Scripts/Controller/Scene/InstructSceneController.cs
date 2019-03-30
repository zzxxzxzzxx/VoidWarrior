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
    [SerializeField]
    private Transform m_MonsterBornPos3;
    [SerializeField]
    private Transform m_MonsterBornPos4;
    /// <summary>
    /// 怪物预制体路径
    /// </summary>
    private string monsterurl = "Prefabs/Role/Prefabs/Role_Monster";
    private RoleController mainPlayerCtrl;
    private GameObject monsterObj1, monsterObj2, objMainPlayer;
    /// <summary>
    /// 怪物和玩家的距离
    /// </summary>
    private float triggerDistance = 35f, distance1, distance2;
    /// <summary>
    /// 流程标记
    /// </summary>
    public int Order { get; set; }
    #region 游戏流程
    void Awake()
    {
        Order = 1;
        GameFacade.Instance.InitUI(); //初始化UI
        GameFacade.Instance.LoadPanel(UIPanelType.Message); //加载信息面板
        GameFacade.Instance.LoadPanel(UIPanelType.Game); //加载游戏面板
        GameFacade.Instance.InitAudio();
        GameFacade.Instance.PlayBgSound(AudioManager.Sound_GameBackGround); //播放背景音乐

        //加载主角
        objMainPlayer = Resources.Load("Prefabs/Role/Prefabs/Role_MainPlayer") as GameObject;
        objMainPlayer.transform.position = m_PlayerBornPos.position;
        objMainPlayer = GameObject.Instantiate(objMainPlayer);
        RoleController mainPlayerCtrl = objMainPlayer.GetComponent<RoleController>();
        mainPlayerCtrl.Init(RoleType.MainPlayer,
                            new RoleInfo(true, 100, 10),
                            new RoleMainPlayerAI(objMainPlayer.GetComponent<RoleController>()));

        //loadMonsters();
    }
    #endregion
    private void Update()
    {

        if (Order >= 2 && Order <= 4)
        {
            if (monsterObj1 != null && monsterObj2 != null)
            {
                distance1 = Vector3.Distance(new Vector3(monsterObj1.transform.position.x, 0, monsterObj1.transform.position.z),
                                                         new Vector3(objMainPlayer.transform.position.x, 0, objMainPlayer.transform.position.z));
                distance2 = Vector3.Distance(new Vector3(monsterObj2.transform.position.x, 0, monsterObj2.transform.position.z),
                                                         new Vector3(objMainPlayer.transform.position.x, 0, objMainPlayer.transform.position.z));
                if (distance1 < triggerDistance)
                    Order = 3;
                else if (distance2 < triggerDistance)
                    Order = 4;
                else
                    Order = 2;
            }
            else if (monsterObj1 == null && monsterObj2 != null)
            {
                distance2 = Vector3.Distance(new Vector3(monsterObj2.transform.position.x, 0, monsterObj2.transform.position.z),
                                             new Vector3(objMainPlayer.transform.position.x, 0, objMainPlayer.transform.position.z));
                if (distance2 < triggerDistance)
                    Order = 4;
                else
                    Order = 2;
            }
            else if (monsterObj1 != null && monsterObj2 == null)
            {
                distance1 = Vector3.Distance(new Vector3(monsterObj1.transform.position.x, 0, monsterObj1.transform.position.z),
                                             new Vector3(objMainPlayer.transform.position.x, 0, objMainPlayer.transform.position.z));
                if (distance1 < triggerDistance)
                    Order = 3;
                else
                    Order = 2;
            }
            else
            {
                Order = 5;
                loadActiveMonsters();
            }
        }
    }

    #region 公有方法
    /// <summary>
    ///加载不攻击的怪物，由flagTriggerController.cs调用
    /// </summary>
    public void loadQuietMonsters()
    {
        Order = 2;
        //加载怪物
        GameObject objMonster1 = Resources.Load(monsterurl + "1") as GameObject;
        objMonster1.transform.position = m_MonsterBornPos1.position;
        GameObject obj1 = GameObject.Instantiate(objMonster1);
        RoleController monsterCtrl1 = obj1.GetComponent<RoleController>();
        monsterCtrl1.Init(RoleType.TimeMonster,
                         new RoleInfo(true, 50, 3, 10),
                         new InstructMonsterAI(obj1.GetComponent<RoleController>()));
        monsterObj1 = obj1;

        GameObject objMonster2 = Resources.Load(monsterurl + "2") as GameObject;
        objMonster2.transform.position = m_MonsterBornPos2.position;
        GameObject obj2 = GameObject.Instantiate(objMonster2);
        RoleController monsterCtrl2 = obj2.GetComponent<RoleController>();
        monsterCtrl2.Init(RoleType.WeaponMonster,
                         new RoleInfo(true, 50, 3, 10),
                         new InstructMonsterAI(obj2.GetComponent<RoleController>()));
        monsterObj2 = obj2;
    }
    #endregion

    #region 私有方法
    /// <summary>
    ///加载攻击的怪物
    /// </summary>
    private void loadActiveMonsters()
    {
        StartCoroutine(Wait(5f));
    }
    private void Instantiate(Transform transform,string path, RoleType roleType)
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
    #endregion

    #region 协程
    /// <summary>
    ///间隔一段时间加载怪物
    /// </summary>
    private IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);

        Instantiate(m_MonsterBornPos1, monsterurl + "1", RoleType.TimeMonster);
        yield return new WaitForSeconds(time);

        Instantiate(m_MonsterBornPos2, monsterurl + "2", RoleType.WeaponMonster);
        yield return new WaitForSeconds(time);

        Instantiate(m_MonsterBornPos3, monsterurl + "1", RoleType.TimeMonster);
        yield return new WaitForSeconds(time);

        Instantiate(m_MonsterBornPos4, monsterurl + "2", RoleType.WeaponMonster);
    }
    #endregion
}


