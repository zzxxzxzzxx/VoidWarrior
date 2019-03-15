using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

/// <summary>
/// 排名管理器
/// </summary>
public class RankManager : BaseManager
{
    #region 构造方法
    /// <summary>
    /// 构造方法
    /// </summary>
    /// <param name="facade">中介者</param>
    public RankManager(GameFacade facade) : base(facade) { }
    #endregion

    #region 成员变量
    /// <summary>
    /// 要存储排名的数量
    /// </summary>
    public int rankNumber;

    /// <summary>
    /// 存储排名的数组属性
    /// </summary>
    public List<RankItemData> RankList
    {
        get;
        private set;
    }
    #endregion

    #region 提供的方法
    /// <summary>
    /// 重写基类的初始化方法
    /// </summary>
    public override void OnInit()
    {
        base.OnInit();
        rankNumber = 10; //设定排名数量
    }

    /// <summary>
    /// 将从服务端获取的排名存到自身中
    /// </summary>
    /// <param name="rankList"></param>
    public void SetRank(List<RankItemData> rankList)
    {
        this.RankList = rankList;
    }
    #endregion
}
