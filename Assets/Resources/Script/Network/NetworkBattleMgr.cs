using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkBattleMgr : Photon.PunBehaviour {

	// Use this for initialization
	void Start () {

        StartCoroutine(ShowPlayer());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator ShowPlayer()
    {
        yield return new WaitForSeconds(1);
        GameObject avatar = PhotonNetwork.Instantiate("NetworkCreator", new Vector3(0, 5, 0), Quaternion.identity, 0);

    }

}
