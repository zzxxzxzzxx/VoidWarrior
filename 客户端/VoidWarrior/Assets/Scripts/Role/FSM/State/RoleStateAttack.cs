using UnityEngine;
using System.Collections;

/// <summary>
/// 攻击状态
/// </summary>
public class RoleStateAttack : RoleStateAbstract
{
    #region 构造函数
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="roleFSMMng">有限状态机管理器</param>
    public RoleStateAttack(RoleFSMMng roleFSMMng): base(roleFSMMng) { }
    #endregion

    #region 成员变量
    /// <summary>
    /// 控制主角旋转的速度
    /// </summary>
    private float m_RotationSpeed = 1;

    /// <summary>
    /// 计算转向目标地点的四元数
    /// </summary>
    private Quaternion m_TargetQuaternion;
    #endregion

    #region 提供的方法
    /// <summary>
    /// 实现基类进入状态
    /// </summary>
    public override void OnEnter()
    {
        base.OnEnter();
        CurrRoleFSMMng.CurrRoleCtrl.agent.isStopped = true;

        m_RotationSpeed = 0; //初始化旋转速度

        //设置攻击状态的控制变量为真
        CurrRoleFSMMng.CurrRoleCtrl.Animator.SetBool(ToAnimatorConditon.ToAttack.ToString(), true);
        //重置状态
        CurrRoleFSMMng.CurrRoleCtrl.Animator.SetInteger(ToAnimatorConditon.CurrState.ToString(), 0);
    }

    /// <summary>
    /// 实现基类执行状态
    /// </summary>
    public override void OnUpdate()
    {
        base.OnUpdate();

        //获取动画控制器的信息
        CurrRoleAnimatorStateInfo = CurrRoleFSMMng.CurrRoleCtrl.Animator.GetCurrentAnimatorStateInfo(0);
        if (CurrRoleAnimatorStateInfo.IsName(RoleAnimatorName.Attack.ToString()) ||
            CurrRoleAnimatorStateInfo.IsName(RoleAnimatorName.Aiming.ToString()))
        {
            //更新当前状态控制变量，停止持续播放
            CurrRoleFSMMng.CurrRoleCtrl.Animator.SetInteger(ToAnimatorConditon.CurrState.ToString(), (int)RoleState.Attack);
        }
        else
        {
            //当前动画不为攻击，等待动画播放再更新当前状态
            CurrRoleFSMMng.CurrRoleCtrl.Animator.SetInteger(ToAnimatorConditon.CurrState.ToString(), 0);
        }

        Vector3 direction = CurrRoleFSMMng.CurrRoleCtrl.TargetPos - CurrRoleFSMMng.CurrRoleCtrl.transform.position;
        //归一化
        direction = direction.normalized;

        direction = direction * Time.deltaTime * CurrRoleFSMMng.CurrRoleCtrl.speed;
        direction.y = 0;

        //判断是否旋转完毕
        if (m_RotationSpeed < 1)
        {
            m_RotationSpeed += 5f * Time.deltaTime;
            m_TargetQuaternion = Quaternion.LookRotation(direction);
            CurrRoleFSMMng.CurrRoleCtrl.transform.rotation = Quaternion.Lerp(CurrRoleFSMMng.CurrRoleCtrl.transform.rotation, m_TargetQuaternion, m_RotationSpeed);

            if (Quaternion.Angle(CurrRoleFSMMng.CurrRoleCtrl.transform.rotation, m_TargetQuaternion) < 1f)
            {
                m_RotationSpeed = 0;
            }
        }

    }

    /// <summary>
    /// 实现基类离开状态
    /// </summary>
    public override void OnLeave()
    {
        base.OnLeave();

        //设置攻击状态的控制变量为假
        CurrRoleFSMMng.CurrRoleCtrl.Animator.SetBool(ToAnimatorConditon.ToAttack.ToString(), false); 
    }
    #endregion
}
