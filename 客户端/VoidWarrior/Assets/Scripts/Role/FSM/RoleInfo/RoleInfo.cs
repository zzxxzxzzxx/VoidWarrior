using UnityEngine;
using System.Collections;

/// <summary>
/// 角色信息基类
/// </summary>
public class RoleInfo
{
    #region 构造函数
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="alive">是否存活</param>
    public RoleInfo(bool alive, int hp, int attack, int score = 0)
    {
        this.IsAlive = alive;
        this.HealthPoint = hp;
        this.Attack = attack;
        this.Score = score;
    }
    #endregion

    #region 成员变量
    /// <summary>
    /// 是否存活
    /// </summary>
    public bool IsAlive { get; set; }

    /// <summary>
    /// 血量
    /// </summary>
    public int HealthPoint { get; set; }

    /// <summary>
    /// 攻击力
    /// </summary>
    public int Attack { get; set; }

    /// <summary>
    /// 怪物奖励分数
    /// </summary>
    public int Score { get; set; }
    #endregion
}
