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
        DontDestroyOnLoad(this.gameObject);
        Battle.photonEngine = this;

        //CheckInternet();

        // test 
        StartConnect();
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

    // 从网络获取版本设置，旧版本不进行网络连接
    IEnumerator CheckVersion()
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

    // Primatry Connection Operation-------------------------------------------------------
    public override void OnConnectedToMaster()
    {
        onConnToMaster.Invoke();

    }

    public override void OnConnectionFail(DisconnectCause cause)
    {
        onConnToMasterFiled.Invoke();
    }
    

    // Photon Server Connection Operation--------------------------------------------------
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
        PhotonNetwork.player.SetCustomProperties(new Hashtable() { { "battle", "0#0#0#0" }, { "team", "0" } });
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
        // 随机加入失败则创建以玩家名字命名的房间
        PhotonNetwork.CreateRoom(PhotonNetwork.player.NickName, new RoomOptions() { MaxPlayers = MaxPlayersPerRoom }, null);
        PhotonNetwork.player.SetCustomProperties(new Hashtable() { { "team", "1" } });

    }

    public override void OnJoinedRoom()
    {
        Battle.inRoom = true;
        PhotonNetwork.automaticallySyncScene = true;

    }

    public override void OnPhotonJoinRoomFailed(object[] codeAndMsg)
    {

    }

    public override void OnLeftRoom()
    {
        Battle.inRoom = false;
        PhotonNetwork.automaticallySyncScene = false;

    }

    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        
        var players = PhotonNetwork.playerList;

        var count0 = 0;
        var count1 = 0;
        var count2 = 0;

        foreach (var item in players)
        {
            var camp = int.Parse(item.CustomProperties["team"].ToString());

            switch (camp)
            {
                case 0:
                    count0++;
                    break;
                case 1:
                    count1++;
                    break;
                case 2:
                    count2++;
                    break;
                default:
                    break;
            }
            
        }

        var tCamp = count1 <= count2 ? 1 : 2;

        newPlayer.SetCustomProperties(new Hashtable() { { "team", tCamp + "" } });

        //if (PhotonNetwork.isMasterClient)
        //{
        //    byte count1 = byte.Parse(PhotonNetwork.room.CustomProperties["count1"].ToString());
        //    byte count2 = byte.Parse(PhotonNetwork.room.CustomProperties["count2"].ToString());

        //    int tCamp = 0;
        //    int tNum = 0;
        //    if (count1 <= count2)
        //    {
        //        tCamp = 1;
        //        tNum = count1 + 1;
        //    }
        //    else
        //    {
        //        tCamp = 2;
        //        tNum = count2 + 1;
        //    }

        //    newPlayer.SetCustomProperties(new Hashtable() { { "team", tCamp + "" } });
        //    PhotonNetwork.room.SetCustomProperties(new Hashtable() { { "count" + tCamp, tNum + "" } });

        //    Debug.Log("count1 = " + count1 + 
        //              " | count2 = " + count2 + 
        //              " | targetCamp = " + tCamp +
        //              " | targetNum = " + tNum 
        //              );

        //}
    }

    public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
    {
        //var camp = int.Parse(otherPlayer.CustomProperties["team"].ToString());
        //string param = "count" + camp;

        //var num = int.Parse(PhotonNetwork.room.CustomProperties[param].ToString());
        
        //PhotonNetwork.room.SetCustomProperties(new Hashtable() { { param, num-- + "" } });

        //Debug.Log("room param = " + param + " | num = " + num);
    }
    


}

[Serializable]
public class NetworkEvent : UnityEvent { }