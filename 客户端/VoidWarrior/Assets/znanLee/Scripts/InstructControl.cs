using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructControl : MonoBehaviour {
    //public RoleFSMMng currRoleFSMMng = null;

    public float Speed = 2f;
    public GameObject followCamera;
    public GameObject soldier;


    private RoleController myRoleController;
    // Use this for initialization
    void Start () {
        followCamera.transform.rotation = soldier.transform.rotation;
        followCamera.transform.rotation = Quaternion.Euler(60.0f, 0.0f, 0.0f);

        myRoleController = soldier.GetComponent<RoleController>();

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.A))
        {
            soldier.transform.Translate(-Speed * Time.deltaTime, 0, 0, Space.Self);
        }
        if (Input.GetKey(KeyCode.D))
        {
            soldier.transform.Translate(Speed * Time.deltaTime, 0, 0, Space.Self);
        }
        if (Input.GetKey(KeyCode.W))
        {
            soldier.transform.Translate(0, 0, Speed * Time.deltaTime, Space.Self);
            myRoleController.currRoleFSMMng.ChangeState(RoleState.Run);
        }
        if (Input.GetKey(KeyCode.S))
        {
            soldier.transform.Translate(0, 0, -Speed * Time.deltaTime, Space.Self);
        }
    }
}
