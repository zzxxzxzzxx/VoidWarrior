using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 结果模型
/// </summary>
public class ReturnData
{
    #region 构造方法
    /// <summary>
    /// 构造方法
    /// </summary>
    /// <param name="returnCode">返回结果</param>
    public ReturnData(string returnCode)
    {
        this.Return = returnCode;
    }
    #endregion

    #region 成员变量
    /// <summary>
    /// 返回属性
    /// </summary>
    public string Return
    {
        get;
        set;
    }
    #endregion
}
