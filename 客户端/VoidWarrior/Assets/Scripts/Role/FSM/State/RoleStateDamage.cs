using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleStateDamage : RoleStateAbstract
{
    #region 构造函数
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="roleFSMMng">有限状态机管理器</param>
    public RoleStateDamage(RoleFSMMng roleFSMMng) : base(roleFSMMng) { }
    #endregion

    #region 提供的方法
    /// <summary>
    /// 实现基类进入状态
    /// </summary>
    public override void OnEnter()
    {
        base.OnEnter();

        //重复攻击进入打断状态
        CurrRoleAnimatorStateInfo = CurrRoleFSMMng.CurrRoleCtrl.Animator.GetCurrentAnimatorStateInfo(0);
        if (CurrRoleAnimatorStateInfo.IsName(RoleAnimatorName.Damage.ToString()))
        {
            CurrRoleFSMMng.CurrRoleCtrl.Animator.SetTrigger(ToAnimatorConditon.ToIterrupt.ToString());
        }

        //设置受伤状态的控制变量为真
        CurrRoleFSMMng.CurrRoleCtrl.Animator.SetBool(ToAnimatorConditon.ToDamage.ToString(), true);
    }

    /// <summary>
    /// 实现基类执行状态
    /// </summary>
    public override void OnUpdate()
    {
        base.OnUpdate();

        //获取动画控制器的信息
        CurrRoleAnimatorStateInfo = CurrRoleFSMMng.CurrRoleCtrl.Animator.GetCurrentAnimatorStateInfo(0);
        if (CurrRoleAnimatorStateInfo.IsName(RoleAnimatorName.Damage.ToString()))
        {
            //更新当前状态控制变量，停止持续播放
            CurrRoleFSMMng.CurrRoleCtrl.Animator.SetInteger(ToAnimatorConditon.CurrState.ToString(), (int)RoleState.Damage);
        }
        else
        {
            //当前动画不为受伤，等待动画播放再更新当前状态
            CurrRoleFSMMng.CurrRoleCtrl.Animator.SetInteger(ToAnimatorConditon.CurrState.ToString(), 0);
        }

        if (CurrRoleAnimatorStateInfo.normalizedTime > 1f)
        {
            CurrRoleFSMMng.CurrRoleCtrl.ToIdle(); //播放完毕转换到等待状态
        }
    }

    /// <summary>
    /// 实现基类离开状态
    /// </summary>
    public override void OnLeave()
    {
        base.OnLeave();

        //设置受伤状态的控制变量为真
        CurrRoleFSMMng.CurrRoleCtrl.Animator.SetBool(ToAnimatorConditon.ToDamage.ToString(), false);
    }
    #endregion
}
