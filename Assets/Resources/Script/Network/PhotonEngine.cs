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
    public NetworkEvent onNoNetwork;
    public NetworkEvent onVersionOld;
    public NetworkEvent onConnStart;
    public NetworkEvent onConnToMaster;
    public NetworkEvent onConnToMasterFiled;
    public NetworkEvent onConnToPhoton;
    public NetworkEvent onConnToPhotonFiled;
    public NetworkEvent onDisconnFromPhoton;

    [Header("Lobby Event------------")]
    public NetworkEvent onStartJoinLobby;
    public NetworkEvent onJoinedLobby;
    public NetworkEvent onLeftLobby;

    // Use this for initialization
    private void Start()
    {
        Application.targetFrameRate = 60;
        DontDestroyOnLoad(this.gameObject);
        Battle.photonEngine = this;

        CheckInternet();

        // test
        //StartConnect();
    }

    // 从网络获取版本设置，旧版本不进行网络连接
    IEnumerator CheckVersion()
    {
        WWW w = new WWW(versionAddress.value);
        while (!w.isDone) { yield return new WaitForEndOfFrame(); }
        var versionSettting = JsonConvert.DeserializeObject<VersionSetting>(w.text);
        Debug.Log("version -  "  + versionSettting.version);
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
    
    public void CheckInternet()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            // if no network
            onNoNetwork.Invoke();
        }
        else
        {
            // check game version
            StartCoroutine(CheckVersion());
        }
    }

    public void StartConnect()
    {
        onConnStart.Invoke();
        PhotonNetwork.ConnectUsingSettings(version.value);
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
        PhotonNetwork.player.NickName = Battle.playerBasicSave.playerName + "#" + DateTime.Now.GetHashCode().ToString().Substring(0,4);
        PhotonNetwork.player.SetCustomProperties(new Hashtable() { { "battle", "0#0#0#0" } });

        onJoinedLobby.Invoke();
    }

    public override void OnLeftLobby()
    {
        onLeftLobby.Invoke();

    }

    //public override void OnLobbyStatisticsUpdate()
    //{

    //}

    // Room Operation-----------------------------------------------------------------

    public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
    {
        // 设置房间信息
        //PhotonNetwork.CreateRoom(PhotonNetwork.player.NickName, new RoomOptions() { MaxPlayers = MaxPlayersPerRoom }, null);
    }

    public override void OnJoinedRoom()
    {
        Battle.inRoom = true;

        if (PhotonNetwork.isMasterClient)
        {
            var p = new Hashtable() {
                { "score", "0#0"},
                { "time", "1" },
                { "prepare", "1"}
            };
            PhotonNetwork.room.SetCustomProperties(p);
        }

        PhotonNetwork.automaticallySyncScene = true;
    }

    public override void OnPhotonJoinRoomFailed(object[] codeAndMsg)
    {
        // test
        PhotonNetwork.CreateRoom("Impact", new RoomOptions() { MaxPlayers = MaxPlayersPerRoom }, null);
    }

    public override void OnLeftRoom()
    {
        Battle.inRoom = false;

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
        Battle.playerExit(camp, otherPlayer.ID);
    }
    
}

[Serializable]
public class NetworkEvent : UnityEvent { }