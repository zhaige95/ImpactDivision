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
    public Text multikill;
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

        if (Battle.battleMgr.lastKillCamp == Battle.localPlayerCamp)
            title.text = "胜利";
        else
            title.text = "失败";
        
        var playerInfo = Battle.localPlayerBattleInfo;
        this.kill.text = playerInfo.kill.ToString();
        this.assists.text = playerInfo.assists.ToString();
        this.multikill.text = playerInfo.multikill.ToString();

        string hit;
        if (playerInfo.fireCount != 0 && playerInfo.hitCount != 0)
        {
            hit = (((float)playerInfo.hitCount / (float)playerInfo.fireCount) * 100f).ToString();
            if (hit.Length > 4)
            {
                this.hitRate.text = hit.Substring(0, 4) + " %";
            }
            else
            {
                this.hitRate.text = hit + " %";
            }
        }
        else
        {
            this.hitRate.text = "0";
        }
        this.demageCount.text = playerInfo.demageCount.ToString();
        this.score.text = playerInfo.score.ToString();

        this.transform.localScale = Vector3.one;
        PlayerCount.text = PhotonNetwork.playerList.Length.ToString();

        // upgrade player basic data
        Battle.playerBasicSave.exp += (playerInfo.score + playerInfo.demageCount * 0.5f);

        // upgrade player battle data
        var playerBattleSave = Battle.playerBattleSave;
        playerBattleSave.kill += playerInfo.kill;
        playerBattleSave.die += playerInfo.dead;
        playerBattleSave.assists += playerInfo.assists;
        playerBattleSave.headshotKill += playerInfo.headShot;

        if (playerBattleSave.maxMultiKill < playerInfo.multikill)
            playerBattleSave.maxMultiKill = playerInfo.multikill;

        playerBattleSave.fireCount += playerInfo.fireCount;
        playerBattleSave.hitCount += playerInfo.hitCount;
        
        playerBattleSave.demage += playerInfo.demageCount;

        if (playerBattleSave.maxOneBattleKill < playerInfo.kill)
            playerBattleSave.maxOneBattleKill = playerInfo.kill;

        playerBattleSave.playTime += Battle.battleMgr.gameTime;

        Battle.SavePlayerBasicData();
        Battle.SavePlayerBattleData();

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
