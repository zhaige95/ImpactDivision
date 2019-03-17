using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarCreator : MonoBehaviour {

    public bool isLocal;
    public int camp;
    public GameObject avatar;
    public ConfigWeapon mainWeapon;
    public ConfigWeapon secondWeapon;


    public GameObject Create() {
        return Factory.CreateAvatar(avatar, camp, isLocal, transform.position, transform.rotation, mainWeapon, secondWeapon);
	}
	
}
