using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
/// <summary>
/// 教学场景下的怪物AI
/// 还需修改
/// </summary>
public class InstructMonsterAI : IRoleAI
{

    #region 构造方法
    /// <summary>
    /// 构造方法
    /// </summary>
    /// <param name="roleCtrl">角色控制器</param>
    public InstructMonsterAI(RoleController roleCtrl)
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
    public void DoAI()
    {
    }
    #endregion
}
