using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

/// <summary>
/// GetRankRequest
/// 获取排名请求，继承自BaseRequest（请求基类）
/// </summary>
public class GetRankRequest : BaseRequest
{
    #region 游戏物体事件
    /// <summary>
    /// 重写唤醒方法
    /// </summary>
    public override void Awake()
    {
        actionCode = ActionCode.GetRank; //获取排名的动作
        base.Awake(); //先于父类定义类型，然后才能添加字典
    }
    #endregion

    #region 提供的方法
    /// <summary>
    /// 发送获取排名申请，调用父类的保护类型发送申请
    /// </summary>
    public override void SendRequest()
    {
        string data = JsonConvert.SerializeObject(new ActionData(ActionCode.GetRank, "{}"));
        base.SendRequest(data); //调用父类的保护类型发送请求
    }

    /// <summary>
    /// 重写响应事件
    /// 回调方法不能同步调用，只能使用异步调用
    /// 响应获取排名事件
    /// </summary>
    /// <param name="data">服务器发送的响应数据</param>
    public override void OnResponse(string data)
    {
        List<RankItemData> rankList = JsonConvert.DeserializeObject<List<RankItemData>>(data);
        facade.SetRankSync(rankList);
    }
    #endregion
}
