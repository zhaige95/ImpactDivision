using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillMsgAnima : MonoBehaviour {
    public float dieTime;
	// Use this for initialization
	void Start () {
        GameObject.Destroy(this.gameObject, this.dieTime);
	}
}
