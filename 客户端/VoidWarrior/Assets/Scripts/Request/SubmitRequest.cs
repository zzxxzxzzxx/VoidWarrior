using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

/// <summary>
/// SubmitRequest
/// 提交分数请求，继承自BaseRequest（请求基类）
/// </summary>
public class SubmitRequest : BaseRequest
{
    #region 游戏物体事件
    /// <summary>
    /// 重写唤醒方法
    /// </summary>
    public override void Awake()
    {
        actionCode = ActionCode.Submit; //提交分数的动作
        base.Awake(); //先于父类定义类型，然后才能添加字典
    }
    #endregion

    #region 提供的方法
    /// <summary>
    /// 发送提交分数申请，调用父类的保护类型发送申请
    /// </summary>
    public void SendRequest(string name, int score)
    {
        string data = JsonConvert.SerializeObject(new ActionData(ActionCode.Submit,
                                                  JsonConvert.SerializeObject(new RecordData(name, score))));
        base.SendRequest(data); //调用父类的保护类型发送请求
    }

    /// <summary>
    /// 发送提交分数申请重载，调用父类的保护类型发送申请
    /// </summary>
    public new void SendRequest(string score)
    {
        string data = JsonConvert.SerializeObject(new ActionData(ActionCode.Submit,
                                                  JsonConvert.SerializeObject(new RecordData(name, score))));
        base.SendRequest(data); //调用父类的保护类型发送请求
    }
    /// <summary>
    /// 重写响应事件
    /// 回调方法不能同步调用，只能使用异步调用
    /// 响应提交事件(未实现)
    /// </summary>
    /// <param name="data">服务器发送的响应数据</param>
    public override void OnResponse(string data)
    {
    }
    #endregion
}
