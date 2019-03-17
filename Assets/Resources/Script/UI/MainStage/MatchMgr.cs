using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MatchMgr : MonoBehaviour {
    public Image frontRing;
    public GameObject backBtn;
    private AsyncOperation async = null;
    // Use this for initialization
    void Start () {
        StartCoroutine(Load());
    }

    IEnumerator Load()
    {
        async = SceneManager.LoadSceneAsync("SampleTestScene");
        async.allowSceneActivation = false;

        while (!async.isDone && async.progress < 0.8f)
        {
            yield return async;
        }
    }
    
	// Update is called once per frame
	void Update () {

        frontRing.fillAmount = async.progress;
        if (async.progress >= 0.9)
        {
            if (Input.anyKeyDown)
                async.allowSceneActivation = true;
        }


    }



}
