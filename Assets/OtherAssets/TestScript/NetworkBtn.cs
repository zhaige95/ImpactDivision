using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Network;

public class NetworkBtn : MonoBehaviour {
    public ServerMgr server;
    public ClientMgr client;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OpenNetwork(int code)
    {
        if (code == 1)
        {
            server.enabled = true;
        }
        client.enabled = true;
    }

}
