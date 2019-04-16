using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SettleScorePanel : Photon.PunBehaviour {
    public Text title;
    public Text kill;
    public Text dead;
    public Text assists;
    public Text miltikill;
    public Text hitRate;
    public Text demageCount;
    public Text score;
    [Header("--------------------------------")]
    public Image matchTimerCicle;
    public Text PlayerCount;
    public float waitTime = 5f;
    Timer timer = new Timer();
    public NetworkEvent OnOpenPanel;
    public NetworkEvent OnTimeOut;

    // Use this for initialization
    void Start () {
        timer.OnComplet = OnTimerComplate;
    }
	
    public void OpenPanel()
    {
        this.timer.Enter(waitTime);

        var playerInfo = Battle.localPlayerBattleInfo;
        this.kill.text = playerInfo.kill.ToString();
        this.assists.text = playerInfo.assists.ToString();
        this.miltikill.text = playerInfo.miltikill.ToString();
        if (playerInfo.fireCount != 0 && playerInfo.hitCount != 0)
        {
            this.hitRate.text = ((float)playerInfo.hitCount / (float)playerInfo.fireCount).ToString();
        }
        else
        {
            this.hitRate.text = "0";
        }
        this.demageCount.text = playerInfo.demageCount.ToString();
        this.score.text = playerInfo.score.ToString();

        this.transform.localScale = Vector3.one;
        PlayerCount.text = PhotonNetwork.playerList.Length.ToString();

    }

    // Update is called once per frame
    void FixedUpdate () {
        matchTimerCicle.fillAmount = timer.rate;
        timer.FixedUpdate();
    }

    public void OnTimerComplate()
    {
        OnTimeOut.Invoke();
    }

    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        PlayerCount.text = PhotonNetwork.playerList.Length.ToString();
    }

    public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
    {
        PlayerCount.text = PhotonNetwork.playerList.Length.ToString();
    }

}
