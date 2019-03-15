using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 进度条控制器
/// </summary>
public class LoadingSliderControl : MonoBehaviour
{
    #region 成员变量
    /// <summary>
    /// 进度条
    /// </summary>
    [SerializeField]
    private Slider m_Progress;

    /// <summary>
    /// 进度条上的文本
    /// </summary>
    [SerializeField]
    private Text m_ProgressText;
    #endregion

    #region 提供的方法
    /// <summary>
    /// 设置进度条的值
    /// </summary>
    /// <param name="value"></param>
    public void SetProgressValue(float value)
    {
        m_Progress.value = value;
        m_ProgressText.text = string.Format("{0}%", (int)(value * 100));
    }
    #endregion
}
