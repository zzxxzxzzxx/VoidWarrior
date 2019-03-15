using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 动作模型
/// </summary>
public class ActionData
{
    #region 构造方法
    /// <summary>
    /// 构造方法
    /// </summary>
    /// <param name="action">相应动作</param>
    /// <param name="jsonData">Json数据</param>
    public ActionData(ActionCode action, string jsonData)
    {
        this.Action = Enum.GetName(typeof(ActionCode), action); ;
        this.JsonData = jsonData;
    }
    #endregion

    #region 成员变量
    /// <summary>
    /// 动作属性
    /// </summary>
    public string Action
    {
        get;
        set;
    }

    /// <summary>
    /// Json属性
    /// </summary>
    public string JsonData
    {
        get;
        set;
    }
    #endregion
}
