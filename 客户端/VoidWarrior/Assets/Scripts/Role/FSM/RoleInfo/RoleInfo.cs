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
    public RoleInfo(bool alive)
    {
        IsAlive = alive;
    }
    #endregion

    #region 成员变量
    /// <summary>
    /// 是否存活
    /// </summary>
    public bool IsAlive { get; set; }
    #endregion
}
