using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// RegistePanel
/// 注册面板，继承自BasePanel（面板基类）
/// </summary>
public class RegistePanel : BasePanel
{
    #region 成员变量
    private Animator animator;
    /// <summary>
    /// 提交按钮
    /// </summary>
    [SerializeField]
    private Button SubmitButton;

    /// <summary>
    /// 取消按钮
    /// </summary>
    [SerializeField]
    private Button CancleButton;

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
    /// 重复密码输入框
    /// </summary>
    [SerializeField]
    private InputField rePasswordIF;

    /// <summary>
    /// 登录申请脚本
    /// </summary>
    [SerializeField]
    private RegisteRequest registeRequest;

    /// <summary>
    /// 注册标记
    /// </summary>
    private bool registeFlag;
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
        //loginRequest = GetComponent<LoginRequest>(); //获取登录请求脚本

        SubmitButton.onClick.AddListener(OnSubmitClick); //给登录按钮添加监听
        CancleButton.onClick.AddListener(OnCancleClick); //给注册按钮添加监听
    }

    private void Update()
    {
        if (registeFlag)
        {
            facade.ShowMessage("注册成功");
            facade.ClosePanel();
            registeFlag = false;
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
    public void OnRegisteResponse(ReturnCode returnCode)
    {
        if (returnCode == ReturnCode.Success)
        {
            registeFlag = true;
        }
        else
        {
            facade.ShowMessageSync("用户名已存在!!"); //异常处理
        }
    }
    #endregion

    #region 私有方法
    /// <summary>
    /// 登录按钮监听
    /// </summary>
    private void OnSubmitClick()
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
        if (!string.Equals(passwordIF.text, rePasswordIF.text))
        {
            Debug.Log(passwordIF.text + "|" + rePasswordIF.text);
            msg += "两次密码不一致";
        }
        if (msg != "")
        {
            facade.ShowMessage(msg); return;
        }

        registeRequest.SendRequest(usernameIF.text, passwordIF.text); //向服务器确认账号是否存在
        facade.ShowMessage("注册中");
    }

    /// <summary>
    /// 注册按钮监听
    /// </summary>
    private void OnCancleClick()
    {
        PlayClickSound(); //发出点击声音
        facade.ClosePanel();
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
        usernameIF.text = "";
        passwordIF.text = "";
        rePasswordIF.text = "";
        gameObject.SetActive(false);
    }
    #endregion
}
