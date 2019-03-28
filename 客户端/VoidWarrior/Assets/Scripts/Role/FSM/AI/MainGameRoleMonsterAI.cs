using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

/// <summary>
/// 怪物AI
/// </summary>
public class MainGameRoleMonsterAI : IRoleAI
{
    #region 成员变量
    /// <summary>
    /// 当前的巡逻点
    /// </summary>
    private int currPoint;

    /// <summary>
    /// 怪物的攻击范围
    /// </summary>
    private float monsterAttackRange = 8f;

    /// <summary>
    /// 怪物的追击范围
    /// </summary>
    private float monsterPursuitRange = 20f;

    /// <summary>
    /// 下一次移动的时间
    /// </summary>
    private float nextMoveTime;
    #endregion

    #region 构造方法
    /// <summary>
    /// 构造方法
    /// </summary>
    /// <param name="roleCtrl">角色控制器</param>
    public MainGameRoleMonsterAI(RoleController roleCtrl, RoleController lockEnemy)
    {
        currRoleCtrl = roleCtrl;
        currRoleCtrl.lockEnemy = lockEnemy;
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
        if (currRoleCtrl.currRoleFSMMng.CurrRoleStateEnum.Equals(RoleState.Run) || //在跑步状态或者等待状态才会执行
            currRoleCtrl.currRoleFSMMng.CurrRoleStateEnum.Equals(RoleState.Idle))
        {
            if (currRoleCtrl.lockEnemy != null) //存在锁定的敌人
            {
                float distance = Vector3.Distance(new Vector3(currRoleCtrl.transform.position.x, 0, currRoleCtrl.transform.position.z),
                                                  new Vector3(currRoleCtrl.lockEnemy.transform.position.x, 0, currRoleCtrl.lockEnemy.transform.position.z));
                Debug.DrawLine(currRoleCtrl.agent.transform.position, currRoleCtrl.lockEnemy.agent.destination, Color.red);
                if (!currRoleCtrl.MoveTo(currRoleCtrl.lockEnemy.agent.destination))
                {
                    Debug.Log(1);
                    currRoleCtrl.ToIdle(); //追击
                    currRoleCtrl.lockEnemy = null;
                }
                //距离到达可攻击范围
                if (distance < monsterAttackRange)
                {
                    //攻击
                    currRoleCtrl.ToAttack(new Vector3(currRoleCtrl.lockEnemy.transform.position.x,
                                                      currRoleCtrl.lockEnemy.transform.position.y + 0.1f,
                                                      currRoleCtrl.lockEnemy.transform.position.z));
                    //锁定敌人的受到伤害方法
                    currRoleCtrl.lockEnemy.ToDamage(Vector3.zero, currRoleCtrl.currRoleInfo.Attack);
                }
            }
        }
        if (currRoleCtrl.currRoleFSMMng.CurrRoleStateEnum.Equals(RoleState.Attack)) //在攻击状态
        {
            if (currRoleCtrl.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
            {
                currRoleCtrl.ToIdle(); //攻击动画播放完毕转到等待状态
            }
        }
    }
    #endregion
}