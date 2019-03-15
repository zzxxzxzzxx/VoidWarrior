using UnityEngine;
using System.Collections;

/// <summary>
/// 待机状态
/// </summary>
public class RoleStateIdle : RoleStateAbstract
{
    #region 构造函数
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="roleFSMMng">有限状态机管理器</param>
    public RoleStateIdle(RoleFSMMng roleFSMMng) : base(roleFSMMng) { }
    #endregion

    #region 提供的方法
    /// <summary>
    /// 实现基类进入状态
    /// </summary>
    public override void OnEnter()
    {
        base.OnEnter();
        //设置等待状态的控制变量为真
        CurrRoleFSMMng.CurrRoleCtrl.Animator.SetBool(ToAnimatorConditon.ToIdle.ToString(), true);
    }

    /// <summary>
    /// 实现基类执行状态
    /// </summary>
    public override void OnUpdate()
    {
        base.OnUpdate();
        //获取动画控制器的信息
        CurrRoleAnimatorStateInfo = CurrRoleFSMMng.CurrRoleCtrl.Animator.GetCurrentAnimatorStateInfo(0);
        if (CurrRoleAnimatorStateInfo.IsName(RoleAnimatorName.Idle.ToString()))
        {
            //更新当前状态控制变量，停止持续播放
            CurrRoleFSMMng.CurrRoleCtrl.Animator.SetInteger(ToAnimatorConditon.CurrState.ToString(), (int)RoleState.Idle);
        }
        else
        {
            //当前动画不为等待，等待动画播放再更新当前状态
            CurrRoleFSMMng.CurrRoleCtrl.Animator.SetInteger(ToAnimatorConditon.CurrState.ToString(), 0);
        }
    }

    /// <summary>
    /// 实现基类离开状态
    /// </summary>
    public override void OnLeave()
    {
        base.OnLeave();

        //设置等待状态的控制变量为假
        CurrRoleFSMMng.CurrRoleCtrl.Animator.SetBool(ToAnimatorConditon.ToIdle.ToString(), false);
    }
    #endregion
}
