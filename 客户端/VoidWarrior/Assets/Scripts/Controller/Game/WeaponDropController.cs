using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDropController : MonoBehaviour
{
    /// <summary>
    /// 游戏控制器
    /// </summary>
    //private MainGameController gameController;

    /// <summary>
    /// 增加的时间
    /// </summary>
    [SerializeField]
    private int addAttack = 0;

    [SerializeField]
    private Animator animator;
    #region 游戏流程
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //gameController = GameObject.Find("MainGameController").GetComponent<MainGameController>();
            //改变游戏状态
            other.GetComponent<RoleController>().currRoleInfo.Attack += addAttack; //增加攻击力
            other.GetComponent<RoleController>().bulletPrefab = Resources.Load("Prefabs/Item/Bullet_Blue") as GameObject;
            animator.SetTrigger("Open");
            gameObject.AddComponent<DestroyForTime>().time = 5;
        }
    }
    #endregion
}
