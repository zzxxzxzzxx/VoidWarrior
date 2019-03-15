using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData
{
    #region 构造方法
    /// <summary>
    /// 构造方法
    /// </summary>
    /// <param name="userName">用户名</param>
    /// <param name="userPW">密码</param>
    public UserData(string userName, string userPW)
    {
        this.UserName = userName;
        this.UserPW = userPW;
    }
    #endregion

    #region 成员变量
    /// <summary>
    /// 用户名属性
    /// </summary>
    public string UserName
    {
        get;
        private set;
    }

    /// <summary>
    /// 密码属性
    /// </summary>
    public string UserPW
    {
        get;
        private set;
    }
    #endregion
}
