using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;//先加入AI

public class AutoStart : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;

    public GameObject wall;
    // Use this for initialization
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();//获取navmeshagent
    }
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        navMeshAgent.SetDestination(new Vector3(22.4f, 0, -15.04f));//设置导航的目标点
    }

}