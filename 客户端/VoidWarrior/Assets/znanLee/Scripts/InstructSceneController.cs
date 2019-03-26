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
    private string path1 = "Prefabs/Role/Prefabs/Role_Monster";
    private string path2 = "Prefabs/Role/Prefabs/Role_Monster2";
    private string path3 = "Prefabs/Role/Prefabs/Role_Monster3";
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
                            new RoleInfo(true, 100, 10),
                            new RoleMainPlayerAI(objMainPlayer.GetComponent<RoleController>()));

        //加载怪物
        LoadMonster(m_MonsterBornPos1,path1);
        //LoadMonster(m_MonsterBornPos2, path2);
        //LoadMonster(m_MonsterBornPos3, path3);
        #endregion
    }

    private void LoadMonster(Transform transform, string path)
    {
        GameObject objMonster = Resources.Load(path.ToString()) as GameObject;
        objMonster.transform.position = transform.position;
        GameObject obj = GameObject.Instantiate(objMonster);
        RoleController monsterCtrl = obj.GetComponent<RoleController>();
        monsterCtrl.Init(RoleType.TimeMonster,
                         new RoleInfo(true, 100, 3, 10),
                         new InstructMonsterAI(obj.GetComponent<RoleController>()));
    }

}
