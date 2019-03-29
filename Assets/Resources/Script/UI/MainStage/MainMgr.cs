using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMgr : WindowBasic
{
    public Text playerNameText;
    public Text playerLevelText;
    public Slider expSlider;

	// Use this for initialization
	override public void Init () {

        playerNameText.text = Battle.playerBasicSave.playerName;

        int playerLevel = (int)(Battle.playerBasicSave.exp / 1550) + 1;
        playerLevelText.text = playerLevel.ToString();
        expSlider.value = (Battle.playerBasicSave.exp % 1550) / 1550;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Match()
    {
        //SceneManager.LoadScene("SampleTestScene");
    }

}
