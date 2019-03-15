using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 子弹控制器
/// </summary>
public class BulletController : MonoBehaviour
{
    #region 成员变量
    /// <summary>
    /// 子弹飞行的速度
    /// </summary>
    public int speed = 5;

    /// <summary>
    /// 爆炸特效游戏物体
    /// </summary>
    public GameObject explosionEffect;
    /// <summary>
    /// 刚体组件
    /// </summary>
    private Rigidbody rgd;
    #endregion

    #region 游戏流程
    void Start ()
    {
        rgd = GetComponent<Rigidbody>(); //获取刚体组件
    }
	
	void Update ()
    {
        //向前移动
        rgd.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Monster")) //攻击到怪物
        {
            other.GetComponent<RoleController>().ToDamage(transform.forward);
        }
        GameObject.Instantiate(explosionEffect, transform.position, transform.rotation); //产生爆炸粒子特效
        GameFacade.Instance.PlayNormalSound(AudioManager.Sound_Explosion); //播放爆炸声音
        GameObject.Destroy(this.gameObject); //销毁自身游戏物体
    }
    #endregion
}
