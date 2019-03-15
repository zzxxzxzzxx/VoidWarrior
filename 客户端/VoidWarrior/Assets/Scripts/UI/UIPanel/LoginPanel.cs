using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// LoginPanel
/// 登录面板，继承自BasePanel（面板基类）
/// </summary>
public class LoginPanel : BasePanel
{
    #region 成员变量
    private Animator animator;
    /// <summary>
    /// 登录按钮
    /// </summary>
    [SerializeField]
    private Button loginButton;

    /// <summary>
    /// 用户名输入框
    /// </summary>
    [SerializeField]
    private InputField usernameIF;

    /// <summary>
    /// 密码输入框
    /// </summary>
    [SerializeField]
    private InputField passwordIF;

    /// <summary>
    /// 登录申请脚本
    /// </summary>
    [SerializeField]
    private LoginRequest loginRequest;

    /// <summary>
    /// 登录标记
    /// </summary>
    private bool loginFlag;
    #endregion

    #region 游戏物体事件
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Start
    /// MonoBehaviour中的初始化
    /// </summary>
    private void Start()
    {
        loginRequest = GetComponent<LoginRequest>(); //获取登录请求脚本

        loginButton.onClick.AddListener(OnLoginClick); //给登录按钮添加监听
    }

    private void Update()
    {
        if (loginFlag)
        {
            GameFacade.Instance.LoadToMenu();
            loginFlag = false;
        }
    }

    /// <summary>
    /// OnEnter
    /// 重写BasePanel中的进入
    /// </summary>
    public override void OnEnter()
    {
        base.OnEnter();
        EnterAnimation(); //进入动画
    }

    /// <summary>
    /// OnPause
    /// 重写BasePanel中的暂停
    /// </summary>
    public override void OnPause()
    {
        HideAnimation(); //退出动画
    }

    /// <summary>
    /// OnResume
    /// 重写BasePanel中的恢复
    /// </summary>
    public override void OnResume()
    {
        EnterAnimation(); //进入动画
    }

    /// <summary>
    /// OnExit
    /// 重写BasePanel中的退出
    /// </summary>
    public override void OnExit()
    {
        HideAnimation(); //退出动画
    }
    #endregion

    #region 提供的方法
    /// <summary>
    /// 响应登录事件
    /// </summary>
    /// <param name="returnCode">服务器发送的响应动作</param>
    public void OnLoginResponse(ReturnCode returnCode)
    {
        if (returnCode == ReturnCode.Success)
        {
            UserData ud = new UserData(usernameIF.text, passwordIF.text); //生成用户DAO
            facade.SetUserData(ud); //调用角色管理器的设置用户数据
            loginFlag = true;
        }
        else
        {
            uiMng.ShowMessageSync("用户名或密码错误，无法登录，请重新输入!!"); //异常处理
        }
    }
    #endregion

    #region 私有方法
    /// <summary>
    /// 登录按钮监听
    /// </summary>
    private void OnLoginClick()
    {
        PlayClickSound(); //发出点击声音

        //判断账号正确性
        string msg = "";
        if (string.IsNullOrEmpty(usernameIF.text))
        {
            msg += "用户名不能为空 ";
        }
        if (string.IsNullOrEmpty(passwordIF.text))
        {
            msg += "密码不能为空 ";
        }
        if (msg != "")
        {
            uiMng.ShowMessage(msg); return;
        }

        loginRequest.SendRequest(usernameIF.text, passwordIF.text); //向服务器确认账号是否存在
    }

    /// <summary>
    /// 进入动画显示
    /// </summary>
    private void EnterAnimation()
    {
        gameObject.SetActive(true); //显示登录面板
        animator.SetTrigger("Enter");
    }

    /// <summary>
    /// 退出动画显示
    /// </summary>
    private void HideAnimation()
    {
        animator.SetTrigger("Leave");
        Invoke("DisableSelf", 1);
    }

    /// <summary>
    /// 关闭激活自身，节约性能
    /// </summary>
    private void DisableSelf()
    {
        gameObject.SetActive(false);
    }
    #endregion
}
