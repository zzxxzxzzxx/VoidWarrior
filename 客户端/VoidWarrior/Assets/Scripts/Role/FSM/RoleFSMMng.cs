using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 角色有限状态机管理器
/// </summary>
public class RoleFSMMng
{
    #region 构造函数
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="currRoleCtrl"></param>
    public RoleFSMMng(RoleController currRoleCtrl)
    {
        CurrRoleCtrl = currRoleCtrl; //绑定角色控制器

        //建立状态字典
        m_RoleStateDic = new Dictionary<RoleState, RoleStateAbstract>();
        m_RoleStateDic[RoleState.Idle] = new RoleStateIdle(this);
        m_RoleStateDic[RoleState.Run] = new RoleStateRun(this);
        m_RoleStateDic[RoleState.Die] = new RoleStateDie(this);
        m_RoleStateDic[RoleState.Attack] = new RoleStateAttack(this);
        m_RoleStateDic[RoleState.Damage] = new RoleStateDamage(this);


        if (m_RoleStateDic.ContainsKey(CurrRoleStateEnum))
        {
            m_CurrRoleState = m_RoleStateDic[CurrRoleStateEnum];
        }
    }
    #endregion

    #region 成员变量
    /// <summary>
    /// 当前角色控制器
    /// </summary>
    public RoleController CurrRoleCtrl { get; private set; }

    /// <summary>
    /// 当前角色状态枚举
    /// </summary>
    public RoleState CurrRoleStateEnum { get; private set; }

    /// <summary>
    /// 当前角色状态
    /// </summary>
    private RoleStateAbstract m_CurrRoleState = null;

    /// <summary>
    /// 存储每个状态控制器实例的字典
    /// </summary>
    private Dictionary<RoleState, RoleStateAbstract> m_RoleStateDic;
    #endregion

    #region 提供的方法
    /// <summary>
    /// 用于调用当前状态控制器更新方法
    /// </summary>
    public void OnUpdate()
    {
        if (m_CurrRoleState != null)
        {
            m_CurrRoleState.OnUpdate();
        }
    }

    /// <summary>
    /// 改变当前状态
    /// </summary>
    /// <param name="newState">新的状态</param>
    public void ChangeState(RoleState newState)
    {
        //if (CurrRoleStateEnum == newState) return; //如果状态变化了再调用
        if (m_CurrRoleState != null) m_CurrRoleState.OnLeave(); //调用之前状态离开方法
        CurrRoleStateEnum = newState; //更改当前状态枚举
        m_CurrRoleState = m_RoleStateDic[newState]; //更改当前状态
        m_CurrRoleState.OnEnter(); //调用新状态进入方法

    }
    #endregion
}
