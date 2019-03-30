using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagTriggerController : MonoBehaviour {

    /// <summary>
    /// 引用InstructSceneController 
    /// </summary>
    private InstructSceneController ctller;
    /// <summary>
    /// 触发判断
    /// </summary>
    private bool isTrigger = false;
    private void Start()
    {
        ctller = GameObject.Find("InstructSceneController").GetComponent<InstructSceneController>();
    }

    /// <summary>
    /// 触发函数
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if(other.name== "Role_MainPlayer(Clone)"&&isTrigger==false)
        {
            isTrigger = true;
            ctller.loadQuietMonsters();
        }

    }
}
