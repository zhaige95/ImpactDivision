using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MatchMgr : MonoBehaviour {
    public Image frontRing;
    public GameObject backBtn;
    public Text processRate;
    private AsyncOperation async = null;
    private float progressValue;
    private float lastTime;
    // Use this for initialization
    private void OnEnable () {
        StartCoroutine(Load());
        lastTime = Time.time;
    }

    IEnumerator Load()
    {
        async = SceneManager.LoadSceneAsync("SampleTestScene");
        async.allowSceneActivation = false;

        while (!async.isDone)
        {
            if (async.progress < 0.9f)
                progressValue = async.progress;
            else
                progressValue = 1.0f;

            frontRing.fillAmount = progressValue;
            processRate.text = (int)(progressValue * 100) + " %";

            yield return null;
        }
    }
    
	// Update is called once per frame
	void Update () {
        
        if (async.progress >= 0.9)
        {
            if (Time.time - lastTime >= 3f)
            {
                Debug.Log(Time.time - lastTime);
                //async.allowSceneActivation = true;
            }
        }
        
    }
    
}
