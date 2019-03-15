using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 记录模型
/// </summary>
public class RecordData
{
    #region 构造方法
    /// <summary>
    /// 构造方法
    /// </summary>
    /// <param name="name">名称</param>
    /// <param name="score">分数</param>
    public RecordData(string name, string score)
    {
        this.score = int.Parse(score);
        this.name = name;
    }

    /// <summary>
    /// 构造方法重载
    /// </summary>
    /// <param name="name">名称</param>
    /// <param name="score">分数</param>
    public RecordData(string name, int score)
    {
        this.name = name;
        this.score = score;
    }
    #endregion

    #region 成员变量
    /// <summary>
    /// 名称
    /// </summary>
    public string name;

    /// <summary>
    /// 分数
    /// </summary>
    public int score;
    #endregion
}
