using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiTest : MonoBehaviour {
    public GameObject main;
    public GameObject equip;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void OpenMain() {
        main.SetActive(true);
        equip.SetActive(false);
    }
    public void OpenEquip() {
        main.SetActive(false);
        equip.SetActive(true);
    }
}
