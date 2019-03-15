using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 排名模型
/// </summary>
public class RankItemData
{
    #region 构造方法
    /// <summary>
    /// 构造方法
    /// </summary>
    /// <param name="user_name">名称</param>
    /// <param name="score">分数</param>
    public RankItemData(string user_name, int score)
    {
        this.user_name = user_name;
        this.score = score;
    }
    #endregion

    #region 成员变量
    /// <summary>
    /// 分数
    /// </summary>
    public int score;

    /// <summary>
    /// 名称
    /// </summary>
    public string user_name;
    #endregion
}
