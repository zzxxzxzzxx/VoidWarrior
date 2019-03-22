using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 摄像机控制器
/// </summary>
public class CameraController : MonoBehaviour
{
    #region 成员变量
    /// <summary>
    /// 控制摄像机上升与下降父物体
    /// </summary>
    [SerializeField]
    private Transform m_CameraUpAndDown;

    /// <summary>
    /// 控制摄像机朝向父物体
    /// </summary>
    [SerializeField]
    private Transform m_CameraDirection;

    /// <summary>
    /// 控制摄像机缩放父物体
    /// </summary>
    [SerializeField]
    private Transform m_CameraZoom;

    /// <summary>
    /// 摄像机朝向的目标模型
    /// </summary>
    public Transform target;

    /// <summary>
    /// 摄像机与模型保持的距离
    /// </summary>
    public float distance = 10.0f;

    /// <summary>
    /// 射线机与模型保持的高度
    /// </summary>
    public float height = 5.0f;

    /// <summary>
    /// 高度阻尼
    /// </summary>
    public float heightDamping = 2.0f;

    /// <summary>
    /// 旋转阻尼
    /// </summary>
    public float rotationDamping = 3.0f;

    /// <summary>
    /// 主角对象
    /// </summary>
    private GameObject controller;

    /// <summary>
    /// 上一次隐藏的游戏物体
    /// </summary>
    private GameObject lastobj;
    #endregion

    #region 游戏流程
    void Start()
    {
        //得到主角对象
        controller = GameObject.FindGameObjectWithTag("Player");
        target = controller.transform;
    }

    void LateUpdate()
    {
        
        // Early out if we don't have a target
        if (!target) return;

        ////当鼠标或者手指在触摸中时
        //if (JFConst.TouchIng())
        //{
        //    bool follow = true;
        //    //计算相机与主角Y轴旋转角度的差。
        //    float abs = Mathf.Abs(transform.rotation.eulerAngles.y - controller.transform.rotation.eulerAngles.y);
        //    //abs等于180的时候标示摄像机完全面对这主角， 》130 《 230 表示让面对的角度左右偏移50度
        //    //这样做是不希望摄像机跟随主角，具体效果大家把代码下载下来看看，这样的摄像机效果很好。
        //    if (abs > 130 && abs < 230)
        //    {
        //        follow = false;
        //    }
        //    else
        //    {
        //        follow = true;
        //    }

        //    float wantedRotationAngle = target.eulerAngles.y;
        //    float wantedHeight = target.position.y + height;

        //    float currentRotationAngle = transform.eulerAngles.y;
        //    float currentHeight = transform.position.y;

        //    //主角面朝射线机 和背对射线机 计算正确的位置
        //    if (follow)
        //    {
        //        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
        //        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);
        //        Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);
        //        Vector3 positon = target.position;
        //        positon -= currentRotation * Vector3.forward * distance;
        //        positon = new Vector3(positon.x, currentHeight, positon.z);
        //        transform.position = Vector3.Lerp(transform.position, positon, Time.time);

        //    }
        //    else
        //    {
        //        Vector3 positon = target.position;
        //        Quaternion cr = Quaternion.Euler(0, currentRotationAngle, 0);

        //        positon += cr * Vector3.back * distance;
        //        positon = new Vector3(positon.x, target.position.y + height, positon.z);
        //        transform.position = Vector3.Lerp(transform.position, positon, Time.time);
        //    }
        //}

        //------------------ToDebug,摄像机射线不能射向主角，欧拉角方面不太懂
        //这里是计算射线的方向，从主角发射方向是射线机方向
        //Vector3 aim = target.position;
        ////得到方向
        //Vector3 ve = (target.position - Camera.main.transform.position).normalized;
        //float an = transform.eulerAngles.y;
        //aim -= an * ve;
        ////在场景视图中可以看到这条射线
        //Debug.DrawLine(Camera.main.transform.position, aim, Color.red);
        ////主角朝着这个方向发射射线
        //RaycastHit hit;
        //if (Physics.Linecast(target.position, aim, out hit))
        //{
        //    string name = hit.collider.gameObject.tag;
        //    if (name != "MainCamera" && name != "Ground" && name != "Player")
        //    {

        //        //当碰撞的不是摄像机也不是地形 那么直接移动摄像机的坐标
        //        Camera.main.transform.position = hit.point;

        //    }
        //}
    }
    #endregion

    #region 提供的方法
    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        m_CameraUpAndDown.localEulerAngles = new Vector3(0, 0, Mathf.Clamp(m_CameraUpAndDown.localEulerAngles.z, 35f, 80f));
        CameraAutoLookAt(transform.position);
    }

    /// <summary>
    /// 自动看向指定物体
    /// </summary>
    /// <param name="pos">物体位置</param>
    public void CameraAutoLookAt(Vector3 pos)
    {
        m_CameraDirection.LookAt(pos);
    }

    /// <summary>
    /// 控制摄像机左右旋转
    /// </summary>
    /// <param name="type">0=左旋 1=右旋</param> (type == 0 ? 1 : -1)
    public void SetCameraRotate(float speed)
    {
        transform.Rotate(0, speed * 80 * Time.deltaTime, 0);
    }

    /// <summary>
    /// 控制摄像机上下旋转
    /// </summary>
    /// <param name="type">0=上旋 1=下旋</param>
    public void SetCameraUpAndDown(float speed)
    {
        m_CameraUpAndDown.Rotate(0, 0, speed * 80 * Time.deltaTime);
        m_CameraUpAndDown.localEulerAngles = new Vector3(0, 0, Mathf.Clamp(m_CameraUpAndDown.localEulerAngles.z, 35f, 80f));
    }

    /// <summary>
    /// 控制摄像机缩放
    /// </summary>
    /// <param name="type">0=拉近 1=拉远</param>
    public void SetCameraZoom(float speed)
    {
        m_CameraZoom.Translate(speed * Vector3.forward * 500 * Time.deltaTime);
        m_CameraZoom.localPosition = new Vector3(0, 0, Mathf.Clamp(m_CameraZoom.localPosition.z, -50f, -25f));
    }

    /// <summary>
    /// 获取摄像机的z轴方向
    /// </summary>
    /// <returns>摄像机的z轴方向</returns>
    public Vector3 GetCameraForward()
    {
        return transform.forward;
    }

    /// <summary>
    /// 获取摄像机的x轴方向
    /// </summary>
    /// <returns>摄像机的x轴方向</returns>
    public Vector3 GetCameraRight()
    {
        return transform.right;
    }
    #endregion
}
