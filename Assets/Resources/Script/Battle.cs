using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;

public class Battle : MonoBehaviour
{
    public static Dictionary<int, int> campNumber = new Dictionary<int, int>();
    //public static Dictionary<int, List<C_BattleMgr>> playerList = new Dictionary<int, List<C_BattleMgr>>();
    public static Dictionary<int, C_BattleMgr> playerListCamp1 = new Dictionary<int, C_BattleMgr>();
    public static Dictionary<int, C_BattleMgr> playerListCamp2 = new Dictionary<int, C_BattleMgr>();

    public static Transform localPlayerCameraTrans;
    public static Camera localPlayerCamera;
    public static int localPlayerCamp;
    public static bool started = false;
    public static bool inRoom = false;
    public static PlaneHUDMgr planeHUDMgr;
    public static HUDMgr hudMgr;
    public static BornPointsMgr bornMgr;


    // save
    public static bool login = false;
    public static PlayerBasic playerBasicSave;
    public static PlayerBattle playerBattleSave;
    public static SystemSetting systemSettingSave;
    public static string savePath = "";

    public static void SavePlayerBasicData()
    {
        string str = JsonConvert.SerializeObject(playerBasicSave);
        File.WriteAllText(savePath + "/PlayerBasic.cfg", str);
    }
    public static void SavePlayerBattleData()
    {
        string str = JsonConvert.SerializeObject(playerBattleSave);
        File.WriteAllText(savePath + "/PlayerBattle.cfg", str);
    }
    public static void SaveSystemSettingData()
    {
        string str = JsonConvert.SerializeObject(systemSettingSave);
        File.WriteAllText(savePath + "/SystemSetting.cfg", str);
    }

    //public static void PlayerJoin(int camp)
    //{
    //    if (campNumber.ContainsKey(camp))
    //    {
    //        campNumber[camp] += 1;
    //    }
    //    else
    //    {
    //        campNumber.Add(camp, 1);
    //    }
    //}

    public static void PlayerJoin(int camp, int roomID, C_BattleMgr battleMgr)
    {
        if (camp == 1)
        {
            playerListCamp1.Add(roomID, battleMgr);
        }
        else if (camp == 2)
        {
            playerListCamp1.Add(roomID, battleMgr);
        }
        if (campNumber.ContainsKey(camp))
        {
            campNumber[camp] += 1;
        }
        else
        {
            campNumber.Add(camp, 1);
        }
    }

    public static void playerExit(int camp)
    {
        if (campNumber.ContainsKey(camp))
        {
            campNumber[camp] -= 1;
        }
        else
        {
            campNumber.Add(camp, 0);
        }
    }

    public static int GetWeakCamp()
    {
        var camp1 = campNumber.ContainsKey(1) ? campNumber[1] : 0;
        var camp2 = campNumber.ContainsKey(2) ? campNumber[1] : 0;
        return camp1 <= camp2 ? 1 : 2;
    }


}



