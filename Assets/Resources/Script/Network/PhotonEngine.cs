using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Random = UnityEngine.Random;

public class PhotonEngine : Photon.PunBehaviour {


    public byte MaxPlayersPerRoom = 4;
    public StringData version;
    public StringData versionAddress;
    [Header("Connect Event------------")]
    public OnVersionOld onVersionOld;
    public OnConnStart onConnStart;
    public OnConnToMaster onConnToMaster;
    public OnConnToMasterFiled onConnToMasterFiled;
    public OnConnToPhoton onConnToPhoton;
    public OnConnToPhotonFiled onConnToPhotonFiled;
    public OnDisconnFromPhoton onDisconnFromPhoton;

    [Header("Lobby Event------------")]
    public OnStartJoinLobby onStartJoinLobby;
    public OnJoinedLobby onJoinedLobby;
    public OnLeftLobby onLeftLobby;

    // Use this for initialization
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        
        StartCoroutine(GetPhotos());
    }

    // 从网络获取版本设置，旧版本不进行网络连接
    IEnumerator GetPhotos()
    {
        WWW w = new WWW(versionAddress.value);
        while (!w.isDone) { yield return new WaitForEndOfFrame(); }
        var versionSettting = JsonConvert.DeserializeObject<VersionSetting>(w.text);
        if (versionSettting.version.Equals(version.value))
        {
            StartConnect();
        }
        else
        {
            onVersionOld.Invoke();
            Debug.Log("游戏版本旧");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartConnect()
    {
        onConnStart.Invoke();
        PhotonNetwork.ConnectUsingSettings("v0.1");
    }

    public void JoinLobby()
    {
        onStartJoinLobby.Invoke();
        PhotonNetwork.JoinLobby();
    }

    // Primatry Connection Operation-----------------------------------------------------------------
    public override void OnConnectedToMaster()
    {
        onConnToMaster.Invoke();
    }

    public override void OnConnectionFail(DisconnectCause cause)
    {
        onConnToMasterFiled.Invoke();
    }
    

    // Photon Server Connection Operation-----------------------------------------------------------------
    // 连接成功
    public override void OnConnectedToPhoton()
    {
        onConnToPhoton.Invoke();
    }

    // 连接失败
    public override void OnFailedToConnectToPhoton(DisconnectCause cause)
    {
        Debug.LogWarning("connect photon filed");
        onConnToPhotonFiled.Invoke();
    }

    // 连接断开
    public override void OnDisconnectedFromPhoton()
    {
        onDisconnFromPhoton.Invoke();
    }

    // Lobby Operation-----------------------------------------------------------------

    public override void OnJoinedLobby()
    {
        onJoinedLobby.Invoke();
        Debug.Log("joind lobby~~~~~~~~~~");
    }

    public override void OnLeftLobby()
    {
        onLeftLobby.Invoke();

    }

    public override void OnLobbyStatisticsUpdate()
    {

    }

    // Room Operation-----------------------------------------------------------------

    public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
    {
        // 随机取一种对战模式设定
        
        // 设置房间信息
        PhotonNetwork.CreateRoom(DateTime.Now.GetHashCode()+"", new RoomOptions() { MaxPlayers = MaxPlayersPerRoom }, null);
        Debug.Log("join random filed");
    }

    public override void OnJoinedRoom()
    {
        Battle.inRoom = true;
        PhotonNetwork.automaticallySyncScene = true;
        Debug.Log("joined");
    }

    public override void OnLeftRoom()
    {
        Battle.inRoom = false;
        PhotonNetwork.automaticallySyncScene = false;
        Debug.Log("left room");
    }

    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {

    }

    public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
    {

    }


    //private void OnLevelWasLoaded(int level)
    //{
    //    if (level == 1)
    //    {
    //        Started = true;
    //        OnGameStart.Invoke();
    //    }
    //}
}
[Serializable]
public class OnVersionOld : UnityEvent { }
[Serializable]
public class OnConnStart : UnityEvent { }
[Serializable]
public class OnConnToPhoton : UnityEvent { }
[Serializable]
public class OnConnToPhotonFiled : UnityEvent { }
[Serializable]
public class OnConnToMaster : UnityEvent { }
[Serializable]
public class OnConnToMasterFiled : UnityEvent { }
[Serializable]
public class OnDisconnFromPhoton : UnityEvent { }
[Serializable]
public class OnStartJoinLobby : UnityEvent { }
[Serializable]
public class OnJoinedLobby : UnityEvent { }
[Serializable]
public class OnLeftLobby : UnityEvent { }
[Serializable]
public class OnJoinedRoom : UnityEvent { }
[Serializable]
public class OnLeftRoom : UnityEvent { }
[Serializable]
public class OnPlayerConnected : UnityEvent { }
[Serializable]
public class OnPlayStart : UnityEvent { }