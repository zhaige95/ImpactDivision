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
    public int minMember = 4;
    public bool single;
    [Header("---------------------")]
    bool preparing = false;
    public float prepareTime = 5f;
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
        PhotonNetwork.automaticallySyncScene = true;
        this.preparing = false;

        if (PhotonNetwork.isMasterClient)
        {
            PhotonNetwork.room.IsOpen = true;
        }
        StartBattleCheck();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (backBtn.interactable)
            {
                backBtn.onClick.Invoke();
            }
        }

        //if (Input.GetKeyDown("l"))
        //{
        //    this.single = !this.single;
        //    Debug.Log("switch match model to single = " + this.single);
        //}
    }

    void FixedUpdate () {

        if (this.preparing)
        {
            matchTimeSecond -= Time.deltaTime;
        }
        else
        {
            matchTimeSecond += Time.deltaTime;
        }
        if (matchTimeSecond >= 60f)
        {
            matchTimeMinute += 1;
            matchTimeSecond -= 60f;
        }
        var second = "00";
        if (matchTimeSecond > 0)
        {
            second = (int)matchTimeSecond < 10 ? ("0" + (int)matchTimeSecond) : (int)matchTimeSecond + "";
        }
        matchTimeText.text = "0" + matchTimeMinute + ":" + second + "";
        
    }

    public void StartMatch()
    {
        PhotonNetwork.JoinRandomRoom();

        // test 
        //PhotonNetwork.JoinOrCreateRoom("Impact zibifang", new RoomOptions() { MaxPlayers = 4 }, null);
    }

    public void CancleMatch()
    {
        this.preparing = false;
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
        onDisconnFromPhoton.Invoke();
    }

    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        this.UpdatePlayerNumber();
        StartBattleCheck();
    }

    public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
    {
        this.UpdatePlayerNumber();
        StartBattleCheck();
    }

    public override void OnJoinedRoom()
    {
        onJoinedRoom.Invoke();
        this.UpdatePlayerNumber();
        StartBattleCheck();

        // test code 
        if (this.single)
        {
            onPlayStart.Invoke();
            async = PhotonNetwork.LoadLevelAsync("Battle002");
            async.allowSceneActivation = true;
            Hashtable p = new Hashtable();
            p.Add("team", "1");
            PhotonNetwork.playerList[0].SetCustomProperties(p, null, false);
        }
        //if (PhotonNetwork.isMasterClient)
        //{
        //    PhotonNetwork.room.IsVisible = false;
        //}


    }

    void UpdatePlayerNumber()
    {
        ringText.text = PhotonNetwork.playerList.Length + "";
    }

    void StartBattleCheck()
    {
        if (!Battle.started && PhotonNetwork.playerList.Length >= minMember)
        {
            onPlayStart.Invoke();
            this.preparing = true;
            matchTimeSecond = this.prepareTime;
            if (PhotonNetwork.isMasterClient)
            {
                StartCoroutine(StartBattleCoroutines());
            }
        }
        else
        {
            this.preparing = false;
        }
    }

    IEnumerator StartBattleCoroutines()
    {
        yield return new WaitForSeconds(this.prepareTime);

        async = PhotonNetwork.LoadLevelAsync("Battle002");
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

        // test
        //if (PhotonNetwork.isMasterClient)
        //{
        //    PhotonNetwork.room.IsVisible = false;
        //}
    }

}
