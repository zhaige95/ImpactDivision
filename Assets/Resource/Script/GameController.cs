using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class GameController : MonoBehaviour {
    
    public GameObject avatar;
    public ConfigWeapon mainWeapon;
    public ConfigWeapon secondWeapon;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        
    }

    void Start () {
        

    }

    // Update is called once per frame
    void Update () {
		
	}
}
