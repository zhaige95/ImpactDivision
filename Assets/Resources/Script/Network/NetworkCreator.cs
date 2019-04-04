using System.Collections;
using System.Collections.Generic;
using UiEvent;
using UnityEngine;

public class NetworkCreator : Photon.PunBehaviour {
    PhotonView pView;
    public bool isLocal = false;
	// Use this for initialization
	void Start () {

        pView = GetComponent<PhotonView>();
        this.isLocal = pView.isMine;

        if (isLocal)
        {
            var save = Battle.playerBattleSave;
            pView.RPC("Create", PhotonTargets.AllBuffered, PhotonNetwork.AllocateViewID(), Battle.localPlayerCamp, save.characterId, save.mainWeaponId, save.secondWeaponId);
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    [PunRPC]
    public void Create(int vID, int team, int characterID, int mainWeaponID, int secondWeaponID)
    {
        GameObject model = Resources.Load("Prefab/Avatar/Avatar_" + characterID) as GameObject;
        ConfigWeapon main = Source.ReadWeaponConfig(mainWeaponID);
        ConfigWeapon second = Source.ReadWeaponConfig(secondWeaponID);
        GameObject avatar = Factory.CreateAvatar(model, team, this.isLocal, this.transform.position, this.transform.rotation, main, second);
        avatar.GetComponent<PhotonView>().viewID = vID;

        if (this.isLocal)
        {
            var uiMgr = avatar.GetComponent<C_UiEventMgr>();
            Battle.hudMgr.Init(uiMgr);
            Battle.planeHUDMgr.Init(uiMgr);
        }

    }

}
