using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagTriggerController : MonoBehaviour {

    private InstructSceneController ctller;
    private bool isTrigger = false;
    private void Start()
    {
        ctller = GameObject.Find("InstructSceneController").GetComponent<InstructSceneController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("tirgger");
        if(other.name== "Role_MainPlayer(Clone)"&&isTrigger==false)
        {
            isTrigger = true;
            ctller.loadMonsters();
        }

    }
}
