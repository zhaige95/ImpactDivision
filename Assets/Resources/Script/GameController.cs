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

    public HUDMgr hudMgr;
    public PlaneHUDMgr planeHUDMgr;
    
    void Awake()
    {
        //Application.targetFrameRate = 60;
        
        var avatarObj = Factory.CreateAvatar(avatar, camp, isLocal, transform.position, transform.rotation, mainWeapon, secondWeapon);
        var uiEventMgr = avatarObj.GetComponent<C_UiEventMgr>();
        hudMgr.Init(uiEventMgr);
        planeHUDMgr.Init(uiEventMgr);
        //foreach (var item in creators)
        //{
        //    var avatar = item.Create();
        //    var _attr = avatar.GetComponent<C_Attributes>();
        //    var _uiMgr = avatar.GetComponent<C_UiEventMgr>();

        //    if (_attr.camp != Battle.localPlayerCamp)
        //    {
        //        _attr.friendMark.localScale = Vector3.zero;
        //    }
            
        //    // 添加头顶血条
        //    //var tinyHpBar = GameObject.Instantiate(Resources.Load("Arts/Prefab/UI/TinyHpBarItem")) as GameObject;
        //    //tinyHpBar.GetComponent<TinyHpBarMgr>().Init(_attr, _uiMgr);
        //    //tinyHpBar.transform.parent = planeHUDMgr.tinyHpBarGroup;
        //    //tinyHpBar.GetComponent<GameObjectEntity>().enabled = true;

        //    avatars.Add(avatar);
        //}

    }
    
}
