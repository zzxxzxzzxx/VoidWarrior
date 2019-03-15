using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 主角AI
/// </summary>
public class RoleMainPlayerAI : IRoleAI
{
    #region 构造方法
    /// <summary>
    /// 构造方法
    /// </summary>
    /// <param name="roleCtrl">角色控制器</param>
    public RoleMainPlayerAI(RoleController roleCtrl)
    {
        currRoleCtrl = roleCtrl;
    }
    #endregion

    #region 成员变量
    /// <summary>
    /// 角色控制器
    /// </summary>
    private RoleController currRoleCtrl;
    #endregion

    #region 提供的方法
    public void DoAI() { }
    #endregion
}
