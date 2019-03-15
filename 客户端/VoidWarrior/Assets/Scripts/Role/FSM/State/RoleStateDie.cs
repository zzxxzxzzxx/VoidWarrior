using UnityEngine;
using System.Collections;

/// <summary>
/// 死亡状态
/// </summary>
public class RoleStateDie : RoleStateAbstract
{
    #region 构造函数
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="roleFSMMng">有限状态机管理器</param>
    public RoleStateDie(RoleFSMMng roleFSMMng) : base(roleFSMMng) { }
    #endregion

    #region 提供的方法
    /// <summary>
    /// 实现基类进入状态
    /// </summary>
    public override void OnEnter()
    {
        base.OnEnter();

        //设置死亡状态的控制变量为真
        CurrRoleFSMMng.CurrRoleCtrl.Animator.SetBool(ToAnimatorConditon.ToDie.ToString(), true);
    }

    /// <summary>
    /// 实现基类执行状态
    /// </summary>
    public override void OnUpdate()
    {
        base.OnUpdate();

        //获取动画控制器的信息
        CurrRoleAnimatorStateInfo = CurrRoleFSMMng.CurrRoleCtrl.Animator.GetCurrentAnimatorStateInfo(0);
        if (CurrRoleAnimatorStateInfo.IsName(RoleAnimatorName.Die.ToString()))
        {
            //更新当前状态控制变量，停止持续播放
            CurrRoleFSMMng.CurrRoleCtrl.Animator.SetInteger(ToAnimatorConditon.CurrState.ToString(), (int)RoleState.Die);
        }
        else
        {
            //当前动画不为死亡，等待动画播放再更新当前状态
            CurrRoleFSMMng.CurrRoleCtrl.Animator.SetInteger(ToAnimatorConditon.CurrState.ToString(), 0);
        }
    }

    /// <summary>
    /// 实现基类离开状态
    /// </summary>
    public override void OnLeave()
    {
        base.OnLeave();

        //设置死亡状态的控制变量为真
        CurrRoleFSMMng.CurrRoleCtrl.Animator.SetBool(ToAnimatorConditon.ToDie.ToString(), false);
    }
    #endregion
}
