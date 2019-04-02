using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkBattleMgr : Photon.PunBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log("scene loaded");
        StartCoroutine(ShowPlayer());
    }

    // Update is called once per frame
    void Update () {
		
	}

    IEnumerator ShowPlayer()
    {
        yield return new WaitForSeconds(1);

        Battle.localPlayerCamp = int.Parse(PhotonNetwork.player.CustomProperties["team"].ToString());
        PhotonNetwork.Instantiate("NetworkCreator", new Vector3(0, 5, 0), Quaternion.identity, 0);
        Debug.Log("created");
        if (PhotonNetwork.isMasterClient)
        {
            PhotonNetwork.room.IsVisible = true;
        }

    }

}
