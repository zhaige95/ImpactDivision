using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillerNamePanel : MonoBehaviour {
    public Text killerName;
    Timer timer;
    public float vanshTime;
	// Use this for initialization
	void Start () {
        timer = new Timer(vanshTime);
        timer.OnComplet = Vanish;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        timer.FixedUpdate();
	}

    public void Active(string kName)
    {
        Debug.Log(kName);
        killerName.text = kName;
        this.transform.localScale = Vector3.one;
        timer.Enter();
    }

    void Vanish()
    {
        this.transform.localScale = Vector3.zero;
    }

}
