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
            pView.RPC("Create", PhotonTargets.AllBuffered, PhotonNetwork.AllocateViewID(), PhotonNetwork.player.ID, PhotonNetwork.player.NickName, Battle.localPlayerCamp, save.characterId, save.mainWeaponId, save.secondWeaponId);
        }
    }

    [PunRPC]
    public void Create(int vID, int roomID, string nName, int team, int characterID, int mainWeaponID, int secondWeaponID)
    {
        GameObject model = Resources.Load("Prefab/Avatar/Avatar_" + characterID) as GameObject;
        ConfigWeapon main = Source.ReadWeaponConfig(mainWeaponID);
        ConfigWeapon second = Source.ReadWeaponConfig(secondWeaponID);
        GameObject avatar = Factory.CreateAvatar(model, team, this.isLocal, this.transform.position, this.transform.rotation, main, second);
        avatar.GetComponent<PhotonView>().viewID = vID;

        var battleMgr = avatar.GetComponent<C_BattleMgr>();
        battleMgr.nickName = nName;

        Battle.PlayerJoin(team, roomID, battleMgr);

        if (this.isLocal)
        {
            var uiMgr = avatar.GetComponent<C_UiEventMgr>();
            Battle.hudMgr.Init(uiMgr);
            Battle.planeHUDMgr.Init(uiMgr); 
        }
        

    }

}
