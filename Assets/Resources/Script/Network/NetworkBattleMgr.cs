using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class NetworkBattleMgr : Photon.PunBehaviour {

    public ConfigBattle configBattle;
    public bool preparing = true;
    public float gameTime = 0f;
    public int lastKillCamp = 0;
    public Dictionary<int, int> score = new Dictionary<int, int>();
    Timer timer = new Timer();
    public BattleInfoMgr battleInfo;
    public PanelSwitch menuPanel;
    public ScoreboardMgr scoreboard;

    [Header("Event")]
    public NetworkEvent OnPrepareEnd;
    public NetworkEvent OnGameOver;

    private void Awake()
    {
        Battle.battleMgr = this;
        score.Add(1, 0);
        score.Add(2, 0);
    }

    void Start () {
        StartCoroutine(ShowPlayer());
        Battle.started = true;

        battleInfo.SetTarget(configBattle.targetKill);

        if (PhotonNetwork.isMasterClient)
        {
            PhotonNetwork.room.SetCustomProperties(new Hashtable() { { "time", configBattle.prepareTime.ToString() } });
            battleInfo.SetTimer(configBattle.prepareTime);
            timer.Enter(configBattle.prepareTime);
        }
        else
        {
            preparing = PhotonNetwork.room.CustomProperties["prepare"].ToString().Equals("1")? true : false;

            var time = float.Parse(PhotonNetwork.room.CustomProperties["time"].ToString());
            if (preparing)
            {
                var temp = configBattle.prepareTime * time;
                battleInfo.SetTimer(temp);
                timer.Enter(temp);
            }
            else
            {
                string[] syncScore = PhotonNetwork.room.CustomProperties["score"].ToString().Split('#');
                score[1] = int.Parse(syncScore[0]);
                score[2] = int.Parse(syncScore[1]);
                
                battleInfo.SetScore(1, syncScore[0]);
                battleInfo.SetScore(2, syncScore[1]);

                PrepareEnd(true);
                StartCoroutine(Minwayjoin());
                gameTime = time;
            }
        }

        timer.OnComplet = PrepareEnd;
    }

    private void Update()
    {

        if (Input.GetKeyDown("tab"))
        {
            scoreboard.SwitchPanel(true);
        }
        else if (Input.GetKeyUp("tab"))
        {
            scoreboard.SwitchPanel(false);
        }

        if (Input.GetKeyDown("escape"))
        {
            menuPanel.SwitchPanel();
        }
    }

    void FixedUpdate()
    {
        timer.FixedUpdate();
        if (timer.isRunning && preparing)
        {
            battleInfo.SetTimer(configBattle.prepareTime * (1 - timer.rate));
            if (PhotonNetwork.isMasterClient)
            {
                PhotonNetwork.room.SetCustomProperties(new Hashtable() { { "time", timer.rate.ToString() } });
            }
        }
        if (!preparing)
        {
            gameTime += Time.fixedDeltaTime;
            battleInfo.SetTimer(gameTime);
            if (gameTime >= configBattle.outTime)
            {
                GameEnd();
            }
            
            if (PhotonNetwork.isMasterClient)
            {
                PhotonNetwork.room.SetCustomProperties(new Hashtable() { { "time", gameTime.ToString() } });
            }
        }


    }

    IEnumerator Minwayjoin()
    {
        yield return new WaitForSeconds(2);
        Battle.localPlayerBattleInfo.SetPlayerEnable(true);
    }

    IEnumerator ShowPlayer()
    {
        yield return new WaitForSeconds(1);

        Battle.localPlayerCamp = int.Parse(PhotonNetwork.player.CustomProperties["team"].ToString());
       
        var t = Battle.bornMgr.GetPoint(Battle.localPlayerCamp);
        PhotonNetwork.Instantiate("NetworkCreator", t.position, t.rotation, 0);
        
    }

    public void AddScore(int camp)
    {
        score[camp]++;
        battleInfo.SetScore(camp, score[camp]);
        lastKillCamp = camp;
        PhotonNetwork.room.SetCustomProperties(new Hashtable() { { "score", score[1] + "#" + score[2] } });
        this.CheckFinishCondition();
    }

    public void PrepareEnd()
    {
        PrepareEnd(false);
    }
    public void PrepareEnd(bool midway)
    {
        if (PhotonNetwork.isMasterClient)
        {
            PhotonNetwork.room.SetCustomProperties(new Hashtable() { { "prepare", "0" } });
            PhotonNetwork.room.IsVisible = true;
        }
        OnPrepareEnd.Invoke();
        preparing = false;
        if (!midway)
        {
            Battle.localPlayerBattleInfo.SetPlayerEnable(true);
        }
        Battle.UpdateFriendlyMark();
    }

    public void GameEnd()
    {
        if (PhotonNetwork.isMasterClient)
        {
            PhotonNetwork.room.IsVisible = true;
        }

        this.preparing = true;
        Battle.localPlayerBattleInfo.SetPlayerEnable(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        OnGameOver.Invoke();
    }

    private void CheckFinishCondition()
    {
        if (score[1] >= configBattle.targetKill || score[2] >= configBattle.targetKill)
        {
            GameEnd();
        };

    }
    
    public void BackToMainStage(bool isLeave)
    {
        Battle.ClearBattlefield();
        Battle.started = false;
        if (isLeave)
        {
            PhotonNetwork.LeaveRoom();
        }
        else
        {
            if (PhotonNetwork.isMasterClient)
            {
                PhotonNetwork.LoadLevelAsync("MainStage");
                var p = new Hashtable() {
                    { "score", "0#0"},
                    { "time", "1" },
                    { "prepare", "1"}
                };
                PhotonNetwork.room.SetCustomProperties(p);
            }
        }
        

    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("MainStage");
        Battle.inRoom = false;
        Battle.started = false;
    }

}
