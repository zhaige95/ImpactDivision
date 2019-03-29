using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MatchMgr : Photon.PunBehaviour
{
    public Image frontRing;
    public GameObject backBtn;
    public Text processRate;
    private AsyncOperation async = null;
    private float progressValue;

    [Header("Photon Event")]
    public OnJoinedRoom onJoinedRoom;

    // Use this for initialization
    private void OnEnable () {

    }

    IEnumerator Load()
    {
        async = PhotonNetwork.LoadLevelAsync("Battle001");
        async.allowSceneActivation = false;

        while (!async.isDone)
        {
            if (async.progress < 0.9f)
                progressValue = async.progress;
            else
                progressValue = 1.0f;

            frontRing.fillAmount = progressValue;
            processRate.text = (int)(progressValue * 100) + "%";

            yield return null;
        }

        yield return new WaitForSeconds(2);
    }
    
	// Update is called once per frame
	void Update () {
        
    }

    public override void OnJoinedRoom()
    {
        onJoinedRoom.Invoke();

    }
    

}
