using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

/// <summary>
/// 角色控制器
/// </summary>
public class RoleController : MonoBehaviour
{
    #region 成员变量
    /// <summary>
    /// 动画
    /// </summary>
    [SerializeField]
    public Animator Animator;

    /// <summary>
    /// 枪口位置
    /// </summary>
    [SerializeField]
    private Transform muzzleTrans;

    /// <summary>
    /// 子弹预设体
    /// </summary>
    [SerializeField]
    public GameObject bulletPrefab;

    /// <summary>
    /// 移动的目标地点
    /// </summary>
    [HideInInspector]
    public Vector3 TargetPos = Vector3.zero;

    /// <summary>
    /// 控制主角走路的速度
    /// </summary>
    public float speed = 10f;

    /// <summary>
    /// 移动时间标记，不让navigation每帧移动
    /// </summary>
    private float moveTime;

    /// <summary>
    /// 锁定敌人，进行追击
    /// </summary>
    public RoleController lockEnemy;

    /// <summary>
    /// 当前角色信息
    /// </summary>
    public RoleInfo currRoleInfo;

    /// <summary>
    /// 当前角色AI
    /// </summary>
    public IRoleAI currRoleAI = null;

    /// <summary>
    /// 当前角色有限状态机管理器
    /// </summary>
    public RoleFSMMng currRoleFSMMng = null;

    /// <summary>
    /// 当前游戏角色类型
    /// </summary>
    public RoleType currRoleType = RoleType.None;

    /// </summary>
    ///网格导航代理
    /// </summary>
    public NavMeshAgent agent;
    /// <summary>
    /// 游戏控制器
    /// </summary>
    private MainGameController mainGameController;
    private InstructController instructController;
    /// <summary>
    /// 死亡标记
    /// </summary>
    private bool deadFlag = true;
    #endregion

    #region 游戏流程
    void Start ()
    {
        //获取游戏控制器
        //mainGameController = GameObject.Find("MainGameController").GetComponent<MainGameController>();  
        setController();
        agent = GetComponent<NavMeshAgent>(); //获取网格导航组件
        //获取摄像机控制器
        GameFacade.Instance.GetCamera(GameObject.Find("CameraFollowAndRotate").GetComponent<CameraController>());
        ////摄像机组件初始化
        GameFacade.Instance.CameraInit();

        currRoleFSMMng = new RoleFSMMng(this); //实例化该角色的FSM状态机
        ToIdle(); //初始化状态为等待
    }
    void  setController()
    {
        if(GameFacade.Instance.currentSceneType.Equals(SceneType.Instruct))
        {
            instructController = GameObject.Find("InstructController").GetComponent<InstructController>();
        }
        else
        {
            mainGameController = GameObject.Find("MainGameController").GetComponent<MainGameController>();
        }
    }
    void Update()
    {
        if (currRoleType == RoleType.MainPlayer) //主角可控制摄像机视角
        {
            CameraAutoFollow(); //摄像机自动跟随
            #region 摄像机旋转
            //右键控制摄像机旋转
            if (Input.GetMouseButton(1))
            {
                if (Input.GetAxis("Mouse X") != 0) GameFacade.Instance.SetCameraRotate(-Input.GetAxis("Mouse X")); //鼠标水平位移控制左右旋转
                if (Input.GetAxis("Mouse Y") != 0) GameFacade.Instance.SetCameraUpAndDown(-Input.GetAxis("Mouse Y")); //鼠标垂直位移控制上下旋转
            }
            //滚轮控制缩放
            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                GameFacade.Instance.SetCameraZoom(Input.GetAxis("Mouse ScrollWheel"));
            }
            #endregion
        }


        if (GameFacade.Instance.currentGameState.Equals(GameStateType.Teaching))
        {
            ////如果角色没有AI，直接返回
            if (currRoleAI == null) return;
            currRoleAI.DoAI();

            if (agent == null) return; //没有网格导航直接返回

            if (currRoleFSMMng != null)
            {
                currRoleFSMMng.OnUpdate(); //执行当前动画状态
            }

            #region 主角
            if (currRoleType.Equals(RoleType.MainPlayer))
            {
                if (currRoleInfo.IsAlive //&& !EventSystem.current.IsPointerOverGameObject()
                    )
                {
                    //主角移动
                    if (Input.GetKey(KeyCode.A))
                    {
                        MoveTo(agent.transform.position - GameFacade.Instance.GetCameraForward() * 10);
                    }
                    else if (Input.GetKey(KeyCode.D))
                    {
                        MoveTo(agent.transform.position + GameFacade.Instance.GetCameraForward() * 10);
                    }
                    else if (Input.GetKey(KeyCode.S))
                    {
                        MoveTo(agent.transform.position + GameFacade.Instance.GetCameraRight() * 10);
                    }
                    else if (Input.GetKey(KeyCode.W))
                    {
                        MoveTo(agent.transform.position - GameFacade.Instance.GetCameraRight() * 10);
                    }

                    //主角攻击
                    if (Input.GetMouseButtonUp(0))
                    {
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        RaycastHit hitInfo;
                        if (Physics.Raycast(ray, out hitInfo))
                        {
                            ToAttack(hitInfo.point);
                        }
                    }
                }

                if (!currRoleInfo.IsAlive && deadFlag) //主角死亡
                {
                    ToDie(); //死亡动画
                    gameObject.AddComponent<DestroyForTime>().time = 5; //销毁自身
                    deadFlag = false;
                    StartCoroutine(GameOver()); //转到游戏结束
                }
            }
            #endregion

            #region 怪物
            if (currRoleType.Equals(RoleType.TimeMonster))
            {
                if (!currRoleInfo.IsAlive && deadFlag) //怪物死亡
                {
                    ToDie(); //死亡动画
                    deadFlag = false;
                    gameObject.AddComponent<DestroyForTime>().time = 3; //销毁自身
                    addScore(currRoleInfo); //增加分数
                    string url = "Prefabs/Item/Gems Ultimate Pack/Prefabs/TimeDrop";
                    GameObject objTimeDrop = Resources.Load(url + new System.Random().Next(1, 17)) as GameObject;
                    objTimeDrop.transform.position = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);
                    GameObject.Instantiate(objTimeDrop);
                }
            }

            if (currRoleType.Equals(RoleType.WeaponMonster))
            {
                if (!currRoleInfo.IsAlive && deadFlag) //怪物死亡
                {
                    ToDie(); //死亡动画
                    deadFlag = false;
                    gameObject.AddComponent<DestroyForTime>().time = 3; //销毁自身
                    addScore(currRoleInfo); //增加分数
                    string url = "Prefabs/Item/Fantasy Chests PBR/Prefabs/Mesh_Chest_01_Mobile";
                    GameObject objWeaponDrop = Resources.Load(url) as GameObject;
                    objWeaponDrop.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                    GameObject.Instantiate(objWeaponDrop);
                }
            }
            #endregion
        }

        if (GameFacade.Instance.currentGameState.Equals(GameStateType.Gaming)) //游戏状态为游戏中才执行
        {
            ////如果角色没有AI，直接返回
            if (currRoleAI == null) return;
            currRoleAI.DoAI();

            if (agent == null) return; //没有网格导航直接返回

            if (currRoleFSMMng != null)
            {
                currRoleFSMMng.OnUpdate(); //执行当前动画状态
            }

            #region 主角
            if (currRoleType.Equals(RoleType.MainPlayer))
            {
                if (currRoleInfo.IsAlive //&& !EventSystem.current.IsPointerOverGameObject()
                    )
                {
                    //主角移动
                    if (Input.GetKey(KeyCode.A))
                    {
                        MoveTo(agent.transform.position - GameFacade.Instance.GetCameraForward() * 10);
                    }
                    else if (Input.GetKey(KeyCode.D))
                    {
                        MoveTo(agent.transform.position + GameFacade.Instance.GetCameraForward() * 10);
                    }
                    else if (Input.GetKey(KeyCode.S))
                    {
                        MoveTo(agent.transform.position + GameFacade.Instance.GetCameraRight() * 10);
                    }
                    else if (Input.GetKey(KeyCode.W))
                    {
                        MoveTo(agent.transform.position - GameFacade.Instance.GetCameraRight() * 10);
                    }

                    //主角攻击
                    if (Input.GetMouseButtonUp(0))
                    {
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        RaycastHit hitInfo;
                        if (Physics.Raycast(ray, out hitInfo))
                        {
                            ToAttack(hitInfo.point);
                        }
                    }
                }

                if (!currRoleInfo.IsAlive && deadFlag) //主角死亡
                {
                    ToDie(); //死亡动画
                    gameObject.AddComponent<DestroyForTime>().time = 5; //销毁自身
                    deadFlag = false;
                    StartCoroutine(GameOver()); //转到游戏结束
                }
            }
            #endregion

            #region 怪物
            if (currRoleType.Equals(RoleType.TimeMonster))
            {
                if (!currRoleInfo.IsAlive && deadFlag) //怪物死亡
                {
                    ToDie(); //死亡动画
                    deadFlag = false;
                    gameObject.AddComponent<DestroyForTime>().time = 5; //销毁自身
                    addScore(currRoleInfo); //增加分数
                    if (UnityEngine.Random.Range(1, 8) > 3)
                    {
                        string url = "Prefabs/Item/Gems Ultimate Pack/Prefabs/TimeDrop";
                        GameObject objTimeDrop = Resources.Load(url + new System.Random().Next(1, 17)) as GameObject;
                        objTimeDrop.transform.position = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);
                        GameObject.Instantiate(objTimeDrop);
                    }
                }
            }

            if (currRoleType.Equals(RoleType.WeaponMonster))
            {
                if (!currRoleInfo.IsAlive && deadFlag) //怪物死亡
                {
                    ToDie(); //死亡动画
                    deadFlag = false;
                    gameObject.AddComponent<DestroyForTime>().time = 5; //销毁自身
                    addScore(currRoleInfo); //增加分数
                    if (UnityEngine.Random.Range(1, 8) > 3)
                    {
                        string url = "Prefabs/Item/Fantasy Chests PBR/Prefabs/Mesh_Chest_01_Mobile";
                        GameObject objWeaponDrop = Resources.Load(url) as GameObject;
                        objWeaponDrop.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                        GameObject.Instantiate(objWeaponDrop);
                    }
                }
            }
            #endregion
        }
    }
    void addScore(RoleInfo roleInfo)
    {
        if (GameFacade.Instance.currentSceneType.Equals(SceneType.Instruct))
            instructController.AddScore(roleInfo.Score);
        else if (GameFacade.Instance.currentSceneType.Equals(SceneType.Game))
            mainGameController.AddScore(roleInfo.Score);
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Destination"))
    //    {
    //        //改变游戏状态
    //        GameFacade.Instance.currentGameState = GameStateType.Victory;
    //        GameFacade.Instance.gameUIUpdate = false;
    //    }
    //}
    #endregion

    #region 控制角色方法
    /// <summary>
    /// FSM状态机转换到等待动画
    /// </summary>
    public void ToIdle()
    {
        currRoleFSMMng.ChangeState(RoleState.Idle);
    }

    /// <summary>
    /// FSM状态机转换到移动动画，并移动
    /// </summary>
    /// <param name="tar
    /// Pos">移动位置</param>
    public bool MoveTo(Vector3 targetPos)
    {
        TargetPos = targetPos;
        //如果目标点不是原点，移动
        if (TargetPos == Vector3.zero) return false;

        if (Time.time < moveTime + 1) return true; //移动时间标记
        currRoleFSMMng.ChangeState(RoleState.Run);
        moveTime = Time.time; //更新时间标记
        return agent.SetDestination(targetPos); //网格导航自动寻路
    }

    /// <summary>
    /// FSM状态机转换到死亡动画
    /// </summary>
    public void ToDie()
    {
        agent.isStopped = true; //停止自动寻路
        currRoleFSMMng.ChangeState(RoleState.Die);
    }

    /// <summary>
    /// FSM状态机转换到攻击动画
    /// </summary>
    /// <param name="targetPos">攻击方向</param>
    public void ToAttack(Vector3 targetPos)
    {
        TargetPos = targetPos;
        //如果目标点不是原点，进行攻击
        if (TargetPos == Vector3.zero) return;

        agent.isStopped = true; //停止自动寻路
        currRoleFSMMng.ChangeState(RoleState.Attack);

        if (currRoleType.Equals(RoleType.MainPlayer)) //主角进行子弹射击
        {
            Invoke("Shoot", 0.1f);
        }
    }
    #endregion

    #region 提供的方法
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="roleType">角色类型</param>
    /// <param name="roleInfo">角色信息</param>
    /// <param name="ai">角色AI</param>
    public void Init(RoleType roleType, RoleInfo roleInfo, IRoleAI ai)
    {
        currRoleType = roleType;
        currRoleAI = ai;
        currRoleInfo = roleInfo;
    }

    /// <summary>
    /// 受到伤害的方法
    /// </summary>
    /// <param name="direction">受到伤害的方向</param>
    public void ToDamage(Vector3 direction, int damage)
    {
        if (!currRoleInfo.IsAlive) return;

        agent.isStopped = true; //navigation停止寻路

        if ((currRoleType.Equals(RoleType.TimeMonster) || 
            currRoleType.Equals(RoleType.WeaponMonster)) && 
            !Animator.GetCurrentAnimatorStateInfo(0).IsName(RoleAnimatorName.Attack.ToString())) //怪物受到伤害显示
        {
            currRoleFSMMng.ChangeState(RoleState.Damage);
            direction = direction.normalized;
            transform.LookAt(transform.position - direction); //看向受到伤害的方向
            StartCoroutine(MonsterToDamageCoroutine(direction, damage)); //延时显示
        }

        if (currRoleType.Equals(RoleType.MainPlayer)) //主角收到伤害显示
        {
            StartCoroutine(MainPlayerToDamageCoroutine(damage)); //延时显示
        }
    }
    #endregion

    #region 私有方法
    #region CameraAutoFollow 摄像机自动跟随
    /// <summary>
    /// 摄像机自动跟随
    /// </summary>
    private void CameraAutoFollow()
    {
        //摄像机跟随主角位置
        GameFacade.Instance.SetCameraPos(gameObject.transform.position);
        //摄像机看向主角位置
        GameFacade.Instance.SetCameraLookAt(gameObject.transform.position);
    }
    #endregion

    /// <summary>
    /// 主角射击的方法
    /// </summary>
    private void Shoot()
    {
        GameObject obj = GameObject.Instantiate(bulletPrefab); //实例化子弹
        obj.GetComponent<BulletController>().damage = currRoleInfo.Attack;
        obj.transform.position = muzzleTrans.position; //位置为枪口位置
        obj.transform.rotation = Quaternion.LookRotation(TargetPos - transform.position, Vector3.up); //方向为枪口方向
        GameFacade.Instance.PlayNormalSound(AudioManager.Sound_Shoot); //发出射击的声音
    }

    #region 物体接触监控
    /*
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(transform.name + ", " + other.gameObject.name + "：开始接触");
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(transform.name + ", " + other.gameObject.name + "：正在接触");
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(transform.name + ", " + other.gameObject.name + "：结束接触");
    }
    */
    #endregion

    #region 物体碰撞监控
    /*
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(transform.name + ", " + collision.gameObject.name + "：开始碰撞");
    }

    private void OnCollisionStay(Collision collision)
    {
        Debug.Log(transform.name + ", " + collision.gameObject.name + "：正在碰撞");
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log(transform.name + ", " + collision.gameObject.name + "：结束碰撞");
    }
    */
    #endregion

    #region 绘制辅助线
    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 3);
    }
    */
    #endregion
    #endregion

    #region 协程
    /// <summary>
    /// 游戏结束协程
    /// </summary>
    /// <returns></returns>
    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(4f);
        //更新游戏状态，显示相应UI
        GameFacade.Instance.currentGameState = GameStateType.GameOver; 
        GameFacade.Instance.gameUIUpdate = false;
    }

    /// <summary>
    /// 主角受到伤害协程
    /// </summary>
    /// <returns></returns>
    private IEnumerator MainPlayerToDamageCoroutine(int damage)
    {
        yield return new WaitForSeconds(1.5f);
        currRoleFSMMng.ChangeState(RoleState.Damage);
        currRoleInfo.HealthPoint -= damage;
        if (currRoleInfo.HealthPoint <= 0)
        {
            currRoleFSMMng.ChangeState(RoleState.Die);
            currRoleInfo.IsAlive = false; //直接死亡
        }
    }


    /// <summary>
    /// 怪物受到伤害协程
    /// </summary>
    /// <param name="direction">受到伤害方向</param>
    /// <returns></returns>
    private IEnumerator MonsterToDamageCoroutine(Vector3 direction, int damage)
    {
        //向后击退
        float endTime = Time.time + 1f; 
        while (Time.time < endTime)
        {
            transform.Translate(direction * Time.deltaTime, Space.World);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        currRoleInfo.HealthPoint -= damage;
        if (currRoleInfo.HealthPoint <= 0) currRoleInfo.IsAlive = false; //直接死亡
    }
    #endregion
}
