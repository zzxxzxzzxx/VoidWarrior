using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// MessagePanel
/// 排行面板，继承自BasePanel（面板基类）
/// </summary>
public class RankPanel : BasePanel
{
    #region 成员变量
    /// <summary>
    /// 面板动画
    /// </summary>
    private Animator animator;

    /// <summary>
    /// 关闭按钮
    /// </summary>
    [SerializeField]
    private Button closeButton;

    /// <summary>
    /// 排名布局
    /// </summary>
    [SerializeField]
    private VerticalLayoutGroup rankLayout;

    /// <summary>
    /// 信息预制体
    /// </summary>
    [SerializeField]
    private GameObject rankItemPrefab;

    #endregion

    #region 游戏流程
    private void Awake()
    {
        animator = GetComponent<Animator>(); //在其他start中会播放动画，所以提前获取
    }

    private void Start()
    {
        //加载按钮监听
        closeButton.onClick.AddListener(OnCloseClick);
    }

    private void Update()
    {
        if (facade.rankUpdate)
        {
            //加载排名
            LoadRankItem();
            facade.rankUpdate = false;
        }
    }
    #endregion

    #region UI管理器流程
    /// <summary>
    /// OnEnter
    /// 重写BasePanel中的进入
    /// </summary>
    public override void OnEnter()
    {
        base.OnEnter();
        EnterAnim();
    }

    /// <summary>
    /// OnPause
    /// 重写BasePanel中的暂停
    /// </summary>
    public override void OnPause()
    {
        base.OnPause();
        LeaveAnim();
    }

    /// <summary>
    /// OnResume
    /// 重写BasePanel中的恢复
    /// </summary>
    public override void OnResume()
    {
        base.OnResume();
        EnterAnim();
    }

    /// <summary>
    /// OnExit
    /// 重写BasePanel中的退出
    /// </summary>
    public override void OnExit()
    {
        base.OnExit();
        LeaveAnim();
    }
    #endregion

    #region 私有方法
    /// <summary>
    /// 关闭按钮监听
    /// </summary>
    private void OnCloseClick()
    {
        PlayClickSound();
        LeaveAnim();
        facade.ClosePanel();
    }

    /// <summary>
    /// 播放进入动画
    /// </summary>
    private void EnterAnim()
    {
        gameObject.SetActive(true);
        animator.SetTrigger("Enter");
    }

    /// <summary>
    /// 播放离开动画
    /// </summary>
    private void LeaveAnim()
    {
        gameObject.SetActive(true);
        animator.SetTrigger("Leave");
        Invoke("DisableSelf", 0.8f);
    }

    /// <summary>
    /// 关闭激活自身，节约性能
    /// </summary>
    private void DisableSelf()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 读取所有排名
    /// </summary>
    private void LoadRankItem()
    {
        //销毁现有所有排名显示
        RankItem[] riArray = rankLayout.GetComponentsInChildren<RankItem>(); //获取所有房间信息
        if (riArray != null)
        {
            foreach (RankItem ri in riArray)
            {
                ri.DestroySelf();
            }
        }

        List<RankItemData> rank = GameFacade.Instance.GetRank();
        int rankCount = 10;
        for (int rankID = 0; rankID < rank.Count; rankID++) 
        {
            if (rank[rankID].score < 0)
            {
                rankCount = rankID;
                break;
            }
            GameObject rankItem = GameObject.Instantiate(rankItemPrefab); //实例化排名信息
            rankItem.transform.SetParent(rankLayout.transform); //设置位置
            rankItem.GetComponent<RankItem>().SetRankInfo(rankID + 1, rank[rankID].user_name, rank[rankID].score); //设置排名信息
        }
        Vector2 size = rankLayout.GetComponent<RectTransform>().sizeDelta; //房间之间的间隔
        //计算roomlayout大小，使其正常显示
        rankLayout.GetComponent<RectTransform>().sizeDelta = new Vector2(size.x,
            rankCount * (rankItemPrefab.GetComponent<RectTransform>().sizeDelta.y + rankLayout.spacing));
    }
    #endregion
}
