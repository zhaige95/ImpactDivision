using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MenuMgr : Photon.PunBehaviour
{
    public List<GameObject> menus;
    public List<WindowBasic> windows;
    [Header("Lobby Event")]
    public OnJoinedLobby onJoinedLobby;
    // Use this for initialization
    void Start () {
        foreach (var item in windows)
        {
            item.Init();
        }
        
	}
	
	// Update is called once per frame
	void Update () {
		
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
    

}

