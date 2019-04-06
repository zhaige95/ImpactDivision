using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Random = UnityEngine.Random;
using Hashtable = ExitGames.Client.Photon.Hashtable;

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
        Application.targetFrameRate = 90;
        DontDestroyOnLoad(this.gameObject);

        // check game version
        //StartCoroutine(GetPhotos());

        StartConnect();

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
        PhotonNetwork.player.NickName = Battle.playerBasicSave.playerName + "#" + DateTime.Now.GetHashCode().ToString().Substring(0,5);
        Debug.Log(PhotonNetwork.player.NickName);
        onJoinedLobby.Invoke();
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
        //PhotonNetwork.CreateRoom("Impact", new RoomOptions() { MaxPlayers = MaxPlayersPerRoom }, null);
        Debug.Log("join random filed");
    }

    public override void OnJoinedRoom()
    {
        Battle.inRoom = true;
        PhotonNetwork.automaticallySyncScene = true;
    }

    public override void OnPhotonJoinRoomFailed(object[] codeAndMsg)
    {
        PhotonNetwork.CreateRoom("Impact", new RoomOptions() { MaxPlayers = MaxPlayersPerRoom }, null);
    }

    public override void OnLeftRoom()
    {
        Battle.inRoom = false;
        PhotonNetwork.automaticallySyncScene = false;
    }

    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        if (Battle.started)
        {
            if (PhotonNetwork.isMasterClient)
            {
                string tCamp = Battle.GetWeakCamp().ToString();
                Hashtable p = new Hashtable
                {
                    { "team", tCamp }
                };
                newPlayer.SetCustomProperties(p, null, false);
            }
        }
    }

    public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
    {
        var camp = int.Parse(otherPlayer.CustomProperties["team"].ToString());
        Battle.playerExit(camp);
    }
    
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