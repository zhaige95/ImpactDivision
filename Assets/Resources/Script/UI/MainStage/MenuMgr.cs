using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MenuMgr : Photon.PunBehaviour
{
    public List<GameObject> menus;
    public List<WindowBasic> windows;
    public GameObject photonEngine;
    [Header("Lobby Event")]
    public NetworkEvent onJoinedLobby;
    public NetworkEvent onDisconnFromPhoton;
    // Use this for initialization
    void Start () {
        foreach (var item in windows)
        {
            item.Init();
        }
        if (Battle.photonEngine == null)
        {
            GameObject.Instantiate(photonEngine);
        }

        if (PhotonNetwork.isMasterClient)
        {
            PhotonNetwork.automaticallySyncScene = false;
        }

        if (Battle.inRoom)
        {
            OpenMenu(3);
        }


	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Input.GetKeyDown("k"))
        {
            Debug.Log("player num : " + PhotonNetwork.playerList.Length);
            Debug.Log("room num : " + PhotonNetwork.GetRoomList().Length);
        }

    }

    public void OpenMenu(int index)
    {
        for (int i = 0; i < menus.Count; i++)
        {
            if (i == index)
                menus[i].SetActive(true);
            else
                menus[i].SetActive(false);
        }
    }

    public override void OnJoinedLobby()
    {
        onJoinedLobby.Invoke();
        OpenMenu(0);
    }

    public override void OnDisconnectedFromPhoton()
    {
        onDisconnFromPhoton.Invoke();
    }

}

