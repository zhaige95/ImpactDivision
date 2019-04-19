using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class MatchMgr : Photon.PunBehaviour
{
    public Image frontRing;
    public Button backBtn;
    public Text ringText;
    private AsyncOperation async = null;
    private float progressValue;
    public bool matching = false;
    public bool single;
    [Header("---------------------")]
    public Text matchTimeText;
    public float matchTimeSecond = 0f;
    public int matchTimeMinute = 0;


    [Header("Photon Event")]
    public NetworkEvent onJoinedRoom;
    public NetworkEvent onPlayStart;
    public NetworkEvent onDisconnFromPhoton;

    private void OnEnable()
    {
        this.UpdatePlayerNumber();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            backBtn.onClick.Invoke();
        }
    }

    void FixedUpdate () {
        if (matching)
        {
            matchTimeSecond += Time.deltaTime;
            if (matchTimeSecond >= 60f)
            {
                matchTimeMinute += 1;
                matchTimeSecond -= 60f;
            }
            
            var second = (int)matchTimeSecond < 10 ? ("0" + (int)matchTimeSecond) : (int)matchTimeSecond + "";
            matchTimeText.text = "0" + matchTimeMinute + ":" + second + "";

        }
    }

    public void StartMatch()
    {
        matching = true;
        //PhotonNetwork.JoinRandomRoom();

        // test 
        PhotonNetwork.JoinRoom("Impact");
    }

    public void CancleMatch()
    {
        matching = false;
        matchTimeText.text = "";
        matchTimeSecond = 0f;
        matchTimeMinute = 0;
        if (Battle.inRoom)
        {
            PhotonNetwork.LeaveRoom();
        }
    }

    // photon event -------------------------------

    public override void OnDisconnectedFromPhoton()
    {
        if (this.matching)
        {
            onDisconnFromPhoton.Invoke();
        }
    }

    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        ringText.text = PhotonNetwork.playerList.Length + "";

        if (!Battle.started && PhotonNetwork.playerList.Length >= 2)
        {
            if (PhotonNetwork.isMasterClient)
            {
                PhotonNetwork.room.IsVisible = false;
                PhotonNetwork.automaticallySyncScene = true;
                onPlayStart.Invoke();
                async = PhotonNetwork.LoadLevelAsync("Battle001");
                async.allowSceneActivation = true;

                for (int i = 0; i < PhotonNetwork.playerList.Length / 2; i++)
                {
                    Hashtable p = new Hashtable
                    {
                        { "team", "1" }
                    };
                    PhotonNetwork.playerList[i].SetCustomProperties(p, null, false);
                }

                for (int i = PhotonNetwork.playerList.Length / 2; i < PhotonNetwork.playerList.Length; i++)
                {
                    Hashtable p = new Hashtable
                    {
                        { "team", "2" }
                    };
                    PhotonNetwork.playerList[i].SetCustomProperties(p, null, false);
                }

                //PhotonNetwork.room.IsVisible = false;
            }
        }
    }

    public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
    {
        this.UpdatePlayerNumber();
    }

    public override void OnJoinedRoom()
    {
        onJoinedRoom.Invoke();
        this.UpdatePlayerNumber();

        // test code 
        if (this.single)
        {
            onPlayStart.Invoke();
            async = PhotonNetwork.LoadLevelAsync("Battle001");
            async.allowSceneActivation = true;
            Hashtable p = new Hashtable();
            p.Add("team", "1");
            PhotonNetwork.playerList[0].SetCustomProperties(p, null, false);
        }

    }

    void UpdatePlayerNumber()
    {
        ringText.text = PhotonNetwork.playerList.Length + "";
    }



}
