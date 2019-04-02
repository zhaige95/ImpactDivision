using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class MatchMgr : Photon.PunBehaviour
{
    public Image frontRing;
    public GameObject backBtn;
    public Text ringText;
    private AsyncOperation async = null;
    private float progressValue;
    public bool matching = false;
    // -----------------------
    public Text matchTimeText;
    public float matchTimeSecond = 0f;
    public int matchTimeMinute = 0;


    [Header("Photon Event")]
    public OnJoinedRoom onJoinedRoom;
    public OnPlayStart onPlayStart;

    //public OnPlayerConnected onPlayerConnected;

    // Update is called once per frame
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
            matchTimeText.text = "0" + matchTimeMinute + ":" + (int)matchTimeSecond + "";

        }
    }

    public void StartMatch()
    {
        matching = true;
        PhotonNetwork.JoinRandomRoom();
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

    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        ringText.text = PhotonNetwork.playerList.Length + "";

        if (!Battle.started && PhotonNetwork.playerList.Length >= 1)
        {
            if (PhotonNetwork.isMasterClient)
            {
                onPlayStart.Invoke();
                async = PhotonNetwork.LoadLevelAsync("Battle001");
                async.allowSceneActivation = true;

                for (int i = 0; i < PhotonNetwork.playerList.Length / 2; i++)
                {
                    Hashtable p = new Hashtable();
                    p.Add("team", "1");
                    PhotonNetwork.playerList[i].SetCustomProperties(p, null, false);
                }

                for (int i = PhotonNetwork.playerList.Length / 2; i < PhotonNetwork.playerList.Length; i++)
                {
                    Hashtable p = new Hashtable();
                    p.Add("team", "2");
                    PhotonNetwork.playerList[i].SetCustomProperties(p, null, false);
                }

                PhotonNetwork.room.IsVisible = false;

            }
        }
    }

    public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
    {
        ringText.text = PhotonNetwork.playerList.Length + "";
    }

    public override void OnJoinedRoom()
    {
        onJoinedRoom.Invoke();
        ringText.text = PhotonNetwork.playerList.Length + "";


        // test code 

        onPlayStart.Invoke();
        async = PhotonNetwork.LoadLevelAsync("Battle001");
        async.allowSceneActivation = true;
        Hashtable p = new Hashtable();
        p.Add("team", "1");
        PhotonNetwork.playerList[0].SetCustomProperties(p, null, false);


    }
    
    


    
}
