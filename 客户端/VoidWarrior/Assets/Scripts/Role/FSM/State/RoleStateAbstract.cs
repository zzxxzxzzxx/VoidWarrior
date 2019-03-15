using UnityEngine;
using System.Collections;

/// <summary>
/// 角色状态抽象基类
/// </summary>
public abstract class RoleStateAbstract
{
    #region 构造方法
    /// <summary>
    /// 构造方法
    /// </summary>
    /// <param name="roleFSMMng">FSM状态机</param>
    public RoleStateAbstract(RoleFSMMng roleFSMMng)
    {
        CurrRoleFSMMng = roleFSMMng; //绑定FSM控制器
    }
    #endregion

    #region 成员变量
    /// <summary>
    /// 当前角色有限状态机管理器
    /// </summary>
    public RoleFSMMng CurrRoleFSMMng { get; private set; }

    /// <summary>
    /// 当前状态动画信息
    /// </summary>
    public AnimatorStateInfo CurrRoleAnimatorStateInfo { get; set; }
    #endregion

    #region 虚方法
    /// <summary>
    /// 进入状态
    /// </summary>
    public virtual void OnEnter() { }

    /// <summary>
    /// 执行状态
    /// </summary>
    public virtual void OnUpdate() { }

    /// <summary>
    /// 离开状态
    /// </summary>
    public virtual void OnLeave() { }
    #endregion
}
