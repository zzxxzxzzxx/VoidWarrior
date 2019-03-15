using Newtonsoft.Json;
using System;

/// <summary>
/// LoginRequest
/// 登录请求，继承自BaseRequest（请求基类）
/// </summary>
public class LoginRequest : BaseRequest
{
    #region 成员变量
    /// <summary>
    /// 登录面板
    /// </summary>
    private LoginPanel loginPanel;
    #endregion

    #region 游戏物体事件
    /// <summary>
    /// 重写唤醒方法
    /// </summary>
    public override void Awake()
    {
        actionCode = ActionCode.Login; //登录的动作
        loginPanel = GetComponent<LoginPanel>(); //获取登录面板
        base.Awake(); //先于父类定义类型，然后才能添加字典
    }
    #endregion

    #region 提供的方法
    /// <summary>
    /// 发送登录申请，调用父类的保护类型发送申请
    /// </summary>
    /// <param name="username">用户名</param>
    /// <param name="password">密码</param>
    public void SendRequest(string username, string password)
    {
        string data = JsonConvert.SerializeObject(new ActionData(ActionCode.Login, 
                                                  JsonConvert.SerializeObject(new UserData(username, password))));
        base.SendRequest(data); //调用父类的保护类型发送请求
    }

    /// <summary>
    /// 重写响应事件
    /// 回调方法不能同步调用，只能使用异步调用
    /// 响应登录事件
    /// </summary>
    /// <param name="data">服务器发送的响应数据</param>
    public override void OnResponse(string data)
    {
        ReturnData returnData = JsonConvert.DeserializeObject<ReturnData>(data);
        ReturnCode returnCode = (ReturnCode)Enum.Parse(typeof(ReturnCode), returnData.Return);
        
        loginPanel.OnLoginResponse(returnCode); //异步调用登录面板响应事件
    }
    #endregion
}
