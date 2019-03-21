using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("escape"))
        {
            transform.localScale = transform.localScale == Vector3.zero ? Vector3.one : Vector3.zero;
        }
	}

    public void LoadMainStage()
    {
        SceneManager.LoadScene("MainStage");
    }

}
