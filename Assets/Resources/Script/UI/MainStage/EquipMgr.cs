using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipMgr : MonoBehaviour {

    public WeaponSelectItem mainSelect;
    public WeaponSelectItem secondSelect;

    [Header("-----------------------------------")]
    public bool initialized = false;
    public List<GameObject> weaponList;
    public GameObject weaponItemPrefab;
    public Transform mainContent;
    public Transform secondContent;
    public List<ConfigWeapon> mainWeaponList;
    public List<ConfigWeapon> secondWeaponList;
    // Use this for initialization
    void Start () {
        if (!this.initialized)
        {
            for (int i = 0; i < mainWeaponList.Count; i++)
            {
                var wItem = GameObject.Instantiate(weaponItemPrefab, mainContent);
                var itemData = wItem.GetComponent<WeaponListItem>();
                itemData.Init(mainWeaponList[i], 1);
                itemData.OnClickCall = OnWeaponItemClick;
            }
            for (int i = 0; i < secondWeaponList.Count; i++)
            {
                var wItem = GameObject.Instantiate(weaponItemPrefab, secondContent);
                var itemData = wItem.GetComponent<WeaponListItem>();
                itemData.Init(mainWeaponList[i], 2);
                itemData.OnClickCall = OnWeaponItemClick;
            }
            this.initialized = true;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SelectWeapon(int index)
    {
        for (int i = 0; i < weaponList.Count; i++)
        {
            if (i == index)
                weaponList[i].SetActive(true);
            else
                weaponList[i].SetActive(false);
        }
    }

    public void OnWeaponItemClick(ConfigWeapon config, byte type)
    {
        if (type == 1)
        {
            mainSelect.UpdateData(config);
        }
        else if (type == 2)
        {
            secondSelect.UpdateData(config);
        }


    }

}
