using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Common;

/// <summary>
/// GameFacade
/// 游戏中介者管理，继承自MonoBehaviour（可挂载到游戏物体上）
/// 将facade类做成单例模式
/// 将各个插件用facade类统一管理起来
/// </summary>
public class GameFacade : MonoBehaviour
{
    #region 成员变量
    /// <summary>
    /// GameFacade类单例_instance
    /// </summary>
    private static GameFacade _instance;

    /// <summary>
    /// 建立GameFacade类单例
    /// </summary>
    public static GameFacade Instance
    {
        //共有get方法，若本身不存在则建立单例（获取游戏物体GameFacade中的GameFacade脚本），否则返回本身
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("GameFacade").GetComponent<GameFacade>();
            }
            return _instance;
        }

        //私有set方法，让属性无法从外界赋值
        private set
        {
        }
    }

    /// <summary>
    /// 当前场景类型
    /// </summary>
    public SceneType currentSceneType;

    /// <summary>
    /// 当前游戏状态
    /// </summary>
    public GameStateType currentGameState;

    /// <summary>
    /// 游戏UI更新标记
    /// </summary>
    public bool gameUIUpdate;

    /// <summary>
    /// 场景管理器
    /// </summary>
    private SceneManager sceneMng;

    /// <summary>
    /// UI管理器
    /// </summary>
    private UIManager uiMng;

    /// <summary>
    /// 排名管理器
    /// </summary>
    private RankManager rankMng;

    /// <summary>
    /// 声音管理器你
    /// </summary>
    private AudioManager audioMng;

    /// <summary>
    /// 摄像机控制器
    /// </summary>
    private CameraController cameraCtr;

    /// <summary>
    /// 用户管理器
    /// </summary>
    private ClientManager clientMng;

    /// <summary>
    /// 请求管理器
    /// </summary>
    private RequestManager requestMng;

    /// <summary>
    /// 用户信息
    /// </summary>
    private UserData userData;

    /// <summary>
    /// 排名更新标记
    /// </summary>
    public bool rankUpdate;
    #endregion

    #region 游戏流程
    private void Awake()
    {
        InitManager(); //初始化管理器
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        //初始化游戏状态
        currentGameState = GameStateType.None;
        currentSceneType = SceneType.None;

    }

    private void Update()
    {
        UpdateManager();
    }

    private void OnDestroy()
    {
        DestroyManager(); //销毁所有管理器
    }
    #endregion

    #region 提供的方法
    #region 场景管理器的方法
    /// <summary>
    /// 加载菜单场景
    /// </summary>
    public void LoadToMenu()
    {
        sceneMng.LoadToMenu();
        currentGameState = GameStateType.None;
    }

    /// <summary>
    /// 加载游戏场景
    /// </summary>
    public void LoadToGame()
    {
        sceneMng.LoadToGame();
        currentGameState = GameStateType.Start;
        gameUIUpdate = false;
    }

    /// <summary>
    /// 加载教学场景
    /// </summary>
    public void LoadToInstruct()
    {
        sceneMng.LoadToInstruct();
        currentGameState = GameStateType.Start;
        gameUIUpdate = false;

    }
    /// <summary>
    /// 加载登录场景
    /// </summary>
    public void LoadToLogin()
    {
        sceneMng.LoadToLogin();
        currentGameState = GameStateType.None;
    }
    #endregion

    #region UI管理器的方法
    /// <summary>
    /// 调用UI管理器中的ShowMessage
    /// </summary>
    /// <param name="msg">显示的内容</param>
    public void ShowMessage(string msg)
    {
        uiMng.ShowMessage(msg);
    }

    /// <summary>
    /// 初始化UI
    /// </summary>
    public void InitUI()
    {
        if (uiMng != null) uiMng.OnInit();
    }

    /// <summary>
    /// 加载UI面板
    /// </summary>
    /// <param name="type"></param>
    public void LoadPanel(UIPanelType type)
    {
        uiMng.PushPanel(type);
    }

    /// <summary>
    /// 关闭UI面板
    /// </summary>
    public void ClosePanel()
    {
        uiMng.PopPanel();
    }

    /// <summary>
    /// 异步消息展示
    /// </summary>
    /// <param name="msg">消息内容</param>
    public void ShowMessageSync(string msg)
    {
        uiMng.ShowMessageSync(msg);
    }

    /// <summary>
    /// 设置消息窗口
    /// </summary>
    /// <param name="msgPanel">MessagePanel消息窗口</param>
    public void InjectMsgPanel(MessagePanel msgPanel)
    {
        uiMng.InjectMsgPanel(msgPanel);
    }
    #endregion

    #region 排名管理器的方法
    /// <summary>
    /// 异步从服务器获取排名信息
    /// </summary>
    /// <param name="score">排名json字符串</param>
    public void SetRankSync(List<RankItemData> rankList)
    {
        rankMng.SetRank(rankList);
        rankUpdate = true;
    }
    /// <summary>
    /// 获取排名
    /// </summary>
    /// <returns>排名数组</returns>
    public List<RankItemData> GetRank()
    {
        return rankMng.RankList;
    }
    #endregion

    #region 声音管理器的方法
    /// <summary>
    /// 初始化声音管理器
    /// </summary>
    public void InitAudio()
    {
        audioMng.OnInit();
    }

    /// <summary>
    /// 调用声音管理器中的PlayBgSound
    /// </summary>
    /// <param name="soundName">声音文件的名字</param>
    public void PlayBgSound(string soundName)
    {
        audioMng.PlayBgSound(soundName);
    }

    /// <summary>
    /// 调用声音管理器中的PlayNormalSound
    /// </summary>
    /// <param name="soundName">声音文件的名字</param>
    public void PlayNormalSound(string soundName)
    {
        audioMng.PlayNormalSound(soundName);
    }
    #endregion

    #region 摄像机控制器的方法
    /// <summary>
    /// 让中介者获取到摄像机控制器
    /// </summary>
    /// <param name="cameraCtr">场景中的摄像机控制器脚本</param>
    public void GetCamera(CameraController cameraCtr)
    {
        this.cameraCtr = cameraCtr;
    }

    /// <summary>
    /// 摄像机初始化
    /// </summary>
    public void CameraInit()
    {
        cameraCtr.Init();
    }

    /// <summary>
    /// 设置摄像机的位置
    /// </summary>
    /// <param name="pos">摄像机位置</param>
    public void SetCameraPos(Vector3 pos)
    {
        cameraCtr.transform.position = pos;
    }

    /// <summary>
    /// 设置摄像机的方向
    /// </summary>
    /// <param name="pos">摄像机看向的方向</param>
    public void SetCameraLookAt(Vector3 pos)
    {
        cameraCtr.CameraAutoLookAt(pos);
    }

    /// <summary>
    /// 摄像机左右旋转
    /// </summary>
    /// <param name="type">旋转方式，0=左旋 1=右旋</param>
    public void SetCameraRotate(float speed)
    {
        cameraCtr.SetCameraRotate(speed);
    }

    /// <summary>
    /// 摄像机上下旋转
    /// </summary>
    /// <param name="type">0=上旋 1=下旋</param>
    public void SetCameraUpAndDown(float speed)
    {
        cameraCtr.SetCameraUpAndDown(speed);
    }

    /// <summary>
    /// 摄像机缩放
    /// </summary>
    /// <param name="type">0=拉近 1=拉远</param>
    public void SetCameraZoom(float speed)
    {
        cameraCtr.SetCameraZoom(speed);
    }

    /// <summary>
    /// 获取摄像机的z轴方向
    /// </summary>
    /// <returns>摄像机的z轴方向</returns>
    public Vector3 GetCameraForward()
    {
        return cameraCtr.GetCameraForward();
    }

    /// <summary>
    /// 获取摄像机的x轴方向
    /// </summary>
    /// <returns>摄像机的x轴方向</returns>
    public Vector3 GetCameraRight()
    {
        return cameraCtr.GetCameraRight();
    }
    #endregion

    #region 用户管理器的方法
    /// <summary>
    /// 调用用户管理器中的SendRequest
    /// </summary>
    /// <param name="data">客户端发出请求的数据</param>
    public void SendRequest(string data)
    {
        clientMng.SendRequest(data);
    }
    #endregion

    #region 请求管理器的方法
    /// <summary>
    /// AddRequest
    /// 调用请求管理的AddRequest
    /// </summary>
    /// <param name="actionCode">传递对应服务器中的类名</param>
    /// <param name="request">传递对应服务器类中的方法名</param>
    public void AddRequest(ActionCode actionCode, BaseRequest request)
    {
        requestMng.AddRequest(actionCode, request);
    }

    /// <summary>
    /// 调用请求管理的RemoveRequest
    /// </summary>
    /// <param name="actionCode">传递对应服务器中的类名</param>
    public void RemoveRequest(ActionCode actionCode)
    {
        requestMng.RemoveRequest(actionCode);
    }

    /// <summary>
    /// 调用请求管理的HandleReponse
    /// 用于处理服务器发给客户端的信息
    /// </summary>
    /// <param name="actionCode">传递对应客户端中的类名</param>
    /// <param name="data">服务器返回的数据</param>
    public void HandleReponse(ActionCode actionCode, string data)
    {
        requestMng.HandleReponse(actionCode, data);
    }
    #endregion

    #region 用户信息管理
    /// <summary>
    /// 设置用户信息（仅一次）
    /// </summary>
    /// <param name="userName">用户名</param>
    /// <param name="userPW">用户密码</param>
    public void SetUserData(UserData userData)
    {
        if (this.userData != null) return;
        this.userData = userData;
    }

    /// <summary>
    /// 获取用户名
    /// </summary>
    /// <returns></returns>
    public string GetUserName()
    {
        return userData.UserName;
    }
    #endregion
    #endregion

    #region 私有方法
    /// <summary>
    /// InitManager
    /// 初始化管理器
    /// </summary>
    private void InitManager()
    {
        //创建所有需要的管理器，把facade中介传递过去
        sceneMng = new SceneManager(this);
        uiMng = new UIManager(this);
        rankMng = new RankManager(this);
        audioMng = new AudioManager(this);
        clientMng = new ClientManager(this);
        requestMng = new RequestManager(this);

        //运行各自管理器的初始化
        sceneMng.OnInit();
        uiMng.OnInit();
        rankMng.OnInit();
        audioMng.OnInit();
        clientMng.OnInit();
        requestMng.OnInit();
    }

    /// <summary>
    /// UpdateManager
    /// 更新管理器中的Update
    /// </summary>
    private void UpdateManager()
    {
        //调用各个管理器的更新
        sceneMng.Update();
        uiMng.Update();
        rankMng.Update();
        audioMng.Update();
        clientMng.Update();
        requestMng.Update();
    }

    /// <summary>
    /// DestroyManager
    /// 销毁所有管理器
    /// </summary>
    private void DestroyManager()
    {
        //调用各个管理器的Destroy
        sceneMng.OnDestroy();
        uiMng.OnDestroy();
        rankMng.OnDestroy();
        audioMng.OnDestroy();
        clientMng.OnDestroy();
        requestMng.OnDestroy();
    }
    #endregion
}
