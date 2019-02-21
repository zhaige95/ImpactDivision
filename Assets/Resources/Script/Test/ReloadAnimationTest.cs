using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadAnimationTest : MonoBehaviour {

    public Animator roleAnimator;
    public Animator WeaponAnimator;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("r"))
        {
            roleAnimator.SetTrigger("reload");
            WeaponAnimator.SetTrigger("reload");
            Debug.Log("reload");
        }
	}
}
