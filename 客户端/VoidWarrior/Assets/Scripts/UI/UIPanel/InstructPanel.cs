using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructPanel : MonoBehaviour
{

    #region 成员变量
    [SerializeField]
    private Text welcome;
    [SerializeField]
    private Text control;
    [SerializeField]
    private Text monster1;
    [SerializeField]
    private Text monster2;
    [SerializeField]
    private Text preGame;

    private Text[] allText = new Text[5];
    #endregion
    private void Start()
    {
        InitializeAllText();
    }


    #region 公有方法
    public void SetOrder(int i)
    {
        SetAllTextFalse();
        allText[i - 1].gameObject.SetActive(true);
    }
    #endregion
    #region 私有方法
    private void InitializeAllText()
    {
        allText[0] = welcome;
        allText[1] = control;
        allText[2] = monster1;
        allText[3] = monster2;
        allText[4] = preGame;
    }
    private void SetAllTextFalse()
    {
        foreach (var text in allText)
        {
            text.gameObject.SetActive(false);
        }
    }
    #endregion
}