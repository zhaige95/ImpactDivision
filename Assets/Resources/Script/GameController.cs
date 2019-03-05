using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UiEvent;

public class GameController : MonoBehaviour {
    public bool isLocal;
    public int camp;
    public GameObject avatar;
    public ConfigWeapon mainWeapon;
    public ConfigWeapon secondWeapon;

    public HUDMgr hudMgr;



    void Awake()
    {
        Application.targetFrameRate = 60;
        
        var avatarObj = Factory.CreateAvatar(avatar, camp, isLocal, transform.position, transform.rotation, mainWeapon, secondWeapon);
        var uiEventMgr = avatarObj.GetComponent<C_UiEventMgr>();
        hudMgr.Init(uiEventMgr);

    }

    void Start () {
        

    }

    // Update is called once per frame
    void Update () {
		
	}
}
