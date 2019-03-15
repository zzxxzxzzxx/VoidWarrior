using UnityEngine;
using System.Collections;

/// <summary>
/// 跑状态
/// </summary>
public class RoleStateRun : RoleStateAbstract
{
    #region 构造函数
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="roleFSMMng">有限状态机管理器</param>
    public RoleStateRun(RoleFSMMng roleFSMMng) : base(roleFSMMng) { }
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

        m_RotationSpeed = 0; //初始化旋转速度

        //设置跑步状态的控制变量为真
        CurrRoleFSMMng.CurrRoleCtrl.Animator.SetBool(ToAnimatorConditon.ToRun.ToString(), true);
    }

    /// <summary>
    /// 实现基类执行状态
    /// </summary>
    public override void OnUpdate()
    {
        base.OnUpdate();

        //获取动画控制器的信息
        CurrRoleAnimatorStateInfo = CurrRoleFSMMng.CurrRoleCtrl.Animator.GetCurrentAnimatorStateInfo(0);
        if (CurrRoleAnimatorStateInfo.IsName(RoleAnimatorName.Run.ToString()))
        {
            //更新当前状态控制变量，停止持续播放
            CurrRoleFSMMng.CurrRoleCtrl.Animator.SetInteger(ToAnimatorConditon.CurrState.ToString(), (int)RoleState.Run);
        }
        else
        {
            //当前动画不为跑步，等待动画播放再更新当前状态
            CurrRoleFSMMng.CurrRoleCtrl.Animator.SetInteger(ToAnimatorConditon.CurrState.ToString(), 0);
        }

        //目标位置接近游戏物体位置
        if (Vector3.Distance(CurrRoleFSMMng.CurrRoleCtrl.TargetPos, CurrRoleFSMMng.CurrRoleCtrl.transform.position) > 0.3f)
        {
            CurrRoleFSMMng.CurrRoleCtrl.agent.ResetPath();
            CurrRoleFSMMng.CurrRoleCtrl.agent.SetDestination(CurrRoleFSMMng.CurrRoleCtrl.TargetPos);
            CurrRoleFSMMng.CurrRoleCtrl.agent.isStopped = false;

            //Vector3 direction = CurrRoleFSMMng.CurrRoleCtrl.TargetPos - CurrRoleFSMMng.CurrRoleCtrl.transform.position;
            ////归一化
            //direction = direction.normalized;

            //direction = direction * Time.deltaTime * CurrRoleFSMMng.CurrRoleCtrl.speed;
            //direction.y = 0;

            ////判断是否旋转完毕
            //if (m_RotationSpeed < 1)
            //{
            //    m_RotationSpeed += 5f * Time.deltaTime;
            //    m_TargetQuaternion = Quaternion.LookRotation(direction);
            //    CurrRoleFSMMng.CurrRoleCtrl.transform.rotation = Quaternion.Lerp(CurrRoleFSMMng.CurrRoleCtrl.transform.rotation, m_TargetQuaternion, m_RotationSpeed);

            //    if (Quaternion.Angle(CurrRoleFSMMng.CurrRoleCtrl.transform.rotation, m_TargetQuaternion) < 1f)
            //    {
            //        m_RotationSpeed = 0;
            //    }
            //}

            ////移动游戏物体
            //CurrRoleFSMMng.CurrRoleCtrl.m_CharacterController.Move(direction);
        }
        else
        {
            CurrRoleFSMMng.CurrRoleCtrl.agent.isStopped = true; //停止寻路
            CurrRoleFSMMng.CurrRoleCtrl.ToIdle(); //转换到等待状态
        }
    }

    /// <summary>
    /// 实现基类离开状态
    /// </summary>
    public override void OnLeave()
    {
        base.OnLeave();

        //设置跑步状态的控制变量为假
        CurrRoleFSMMng.CurrRoleCtrl.Animator.SetBool(ToAnimatorConditon.ToRun.ToString(), false);
    }
    #endregion
}
