using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryTimer : MonoBehaviour {
    public float destoryTime = 3f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Destroy(this.gameObject, destoryTime);
	}
}
