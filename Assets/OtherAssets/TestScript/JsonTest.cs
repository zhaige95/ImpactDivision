using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using UnityEngine.SceneManagement;

public class JsonTest : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "/My Games/Impact Division";

        if (!Directory.Exists(path))
        {
            
            SceneManager.LoadScene("LoginStage");
        }
        else
        {
            var str = File.ReadAllText(path);
            Battle.playerBasicSave = JsonConvert.DeserializeObject<PlayerBasic>(str);

            SceneManager.LoadScene("MainStage");
        }

        //var str = File.ReadAllText(Application.dataPath + "/Resources/Config/json/PlayerBasic.json");
        //PlayerBasic playerBasic = JsonConvert.DeserializeObject<PlayerBasic>(str);
        //Debug.Log(playerBasic.playerName);
        //Debug.Log(playerBasic.exp);

        //Battle.playerBasicSave = new PlayerBasic()
        //{
        //    playerName = "爱u说的话"
        //};
        //string str = JsonConvert.SerializeObject(Battle.playerBasicSave);
        //Debug.Log(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments));

        //string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "/My Games/Impact Division";
        //if (!Directory.Exists(path))
        //{
        //    Directory.CreateDirectory(path);
        //}
        //File.WriteAllText(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments)+ "/My Games/Impact Division/PlayerBasic.json", str);

    }

    // Update is called once per frame
    void Update () {

	}
}
