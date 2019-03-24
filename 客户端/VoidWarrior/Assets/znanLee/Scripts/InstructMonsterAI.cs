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
    #region 成员变量
    /// <summary>
    /// 巡逻点
    /// </summary>
    private Vector3[] patrolPoint;

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
    public InstructMonsterAI(RoleController roleCtrl)
    {
        currRoleCtrl = roleCtrl;
        patrolPoint = new Vector3[3];
        patrolPoint[0] = new Vector3(80, .28f, 120);
        patrolPoint[1] = new Vector3(120, .28f, 80);
        patrolPoint[2] = new Vector3(60, .28f, 100);
        currPoint = Random.Range(0, 3);
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
                if (distance > monsterPursuitRange) //超出追寻范围
                {
                    currRoleCtrl.lockEnemy = null;
                }
                else
                {
                    currRoleCtrl.MoveTo(currRoleCtrl.lockEnemy.transform.position); //追击
                    //距离到达可攻击范围
                    if (distance < monsterAttackRange)
                    {
                        //攻击
                        currRoleCtrl.ToAttack(new Vector3(currRoleCtrl.lockEnemy.transform.position.x,
                                                          currRoleCtrl.lockEnemy.transform.position.y + 0.1f,
                                                          currRoleCtrl.lockEnemy.transform.position.z));
                        //锁定敌人的受到伤害方法
                        currRoleCtrl.lockEnemy.ToDamage(Vector3.zero);
                    }
                }
            }
            else
            {
                //移动判断
                if (currRoleCtrl.currRoleFSMMng.CurrRoleStateEnum.Equals(RoleState.Idle) && Time.time > nextMoveTime)
                {
                    currPoint = (currPoint + Random.Range(-1, 2) + 3) % 3;
                    currRoleCtrl.MoveTo(new Vector3(patrolPoint[currPoint].x + Random.Range(-10, 10),
                                                    patrolPoint[currPoint].y,
                                                    patrolPoint[currPoint].z + Random.Range(-10, 10)));
                    nextMoveTime = Time.time + 5f + Random.Range(-5, 10);
                }

                //检测周围是否存在主角
                Collider[] colliderArr = Physics.OverlapSphere(currRoleCtrl.transform.position, monsterPursuitRange, 1 << LayerMask.NameToLayer("Player"));
                if (colliderArr != null && colliderArr.Length > 0)
                {
                    for (int i = 0; i < colliderArr.Length; i++)
                    {
                        currRoleCtrl.lockEnemy = colliderArr[i].gameObject.GetComponent<RoleController>();
                    }
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
