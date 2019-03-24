using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using UiEvent;

public class GameController : MonoBehaviour {
    public bool isLocal;
    public int camp;
    public GameObject avatar;
    public ConfigWeapon mainWeapon;
    public ConfigWeapon secondWeapon;

    public List<AvatarCreator> creators;
    public List<GameObject> avatars = new List<GameObject>();
    public GameObject localPlayer;

    public HUDMgr hudMgr;
    public PlaneHUDMgr planeHUDMgr;

    public int frameRate = 60;
    
    void Awake()
    {
        Application.targetFrameRate = frameRate;

        //localPlayer = Factory.CreateAvatar(avatar, camp, isLocal, transform.position, transform.rotation, mainWeapon, secondWeapon);
        //var uiEventMgr = localPlayer.GetComponent<C_UiEventMgr>();
        //hudMgr.Init(uiEventMgr);
        //planeHUDMgr.Init(uiEventMgr);

        //foreach (var item in creators)
        //{
        //    var avatar = item.Create();
        //    var _attr = avatar.GetComponent<C_Attributes>();
        //    var _uiMgr = avatar.GetComponent<C_UiEventMgr>();

        //    if (_attr.camp != Battle.localPlayerCamp)
        //    {
        //        _attr.friendMark.localScale = Vector3.zero;
        //    }
            
        //    avatars.Add(avatar);
        //}

    }

    public void Exit()
    {
        foreach (var item in avatars)
        {
            Object.Destroy(item);
        }
        avatars.Clear();
        Object.Destroy(localPlayer);
        Effect.Clear();
    }

}
