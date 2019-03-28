using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 主游戏场景控制器
/// </summary>
public class MainGameSceneController : MonoBehaviour
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

    /// <summary>
    /// 怪物预制体路径
    /// </summary>
    private string monsterurl = "Prefabs/Role/Prefabs/Role_Monster";

    private int monsterCount = 2;
    private GameObject[] objMonster;

    private RoleController mainPlayerCtrl;
    private RoleType[] monsterType;

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
        mainPlayerCtrl = objMainPlayer.GetComponent<RoleController>();
        mainPlayerCtrl.Init(RoleType.MainPlayer,
                            new RoleInfo(true, 1000, 10),
                            new RoleMainPlayerAI(objMainPlayer.GetComponent<RoleController>()));

        objMonster = new GameObject[monsterCount+1];
        for (int i = 1; i <= monsterCount; i++) 
        {
            objMonster[i] = Resources.Load(monsterurl + i.ToString()) as GameObject;
        }
        monsterType = new RoleType[monsterCount + 1];
        for (int i = 1; i <= monsterCount; i++)
        {
            monsterType[i] = (RoleType)(i + 1);
        }
        //加载怪物
        StartCoroutine(InstantiateMonsters());
    }
    #endregion

    private IEnumerator InstantiateMonsters()
    {
        while (true)
        {
            for (int i = 0; i < Random.Range(1, 3); i++) InstantiateMonster(); //每5s生成一波怪物
            yield return new WaitForSeconds(5f);
        }
    }

    /// <summary>
    /// 随机生成一个怪物
    /// </summary>
    private void InstantiateMonster()
    {
        Transform[] monsterTransform = m_MonsterBornPos.GetComponentsInChildren<Transform>();
        int random = Random.Range(1, monsterCount + 1);
        GameObject obj = objMonster[random];
        obj.transform.position = monsterTransform[Random.Range(1, 4)].position;
        obj = GameObject.Instantiate(obj);
        RoleController monsterCtrl = obj.GetComponent<RoleController>();
        monsterCtrl.Init(monsterType[random],
                         new RoleInfo(true, 100, 3, 10),
                         new MainGameRoleMonsterAI(obj.GetComponent<RoleController>(), mainPlayerCtrl));
    }
}
