using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillMsgMgr : MonoBehaviour {
    public Transform iconPanel;
    public Transform msgPanel;
    public GameObject killIcon;
    public GameObject killMsg;
    public GameObject assMsg;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddKillMsg()
    {
        Instantiate(killIcon, iconPanel);
        Instantiate(killMsg, msgPanel);
    }

    public void AddAssMsg()
    {
        Instantiate(assMsg, msgPanel);
    }

}
