using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MatchMgr : Photon.PunBehaviour
{
    public Image frontRing;
    public GameObject backBtn;
    public Text ringText;
    private AsyncOperation async = null;
    private float progressValue;

    [Header("Photon Event")]
    public OnJoinedRoom onJoinedRoom;
    public OnPlayStart onPlayStart;
    //public OnPlayerConnected onPlayerConnected;

    // Update is called once per frame
    void Update () {
        
    }

    public void StartMatch()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public void CancleMatch()
    {
        if (Battle.inRoom)
        {
            PhotonNetwork.LeaveRoom();
        }
    }
    
    // photon event -------------------------------

    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        ringText.text = PhotonNetwork.playerList.Length + "";

        if (!Battle.started && PhotonNetwork.playerList.Length >= 3)
        {
            if (PhotonNetwork.isMasterClient)
            {
                async = PhotonNetwork.LoadLevelAsync("Battle001");
                async.allowSceneActivation = true;
                onPlayStart.Invoke();
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
    }
    
    


    
}
