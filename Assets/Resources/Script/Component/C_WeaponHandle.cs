using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class C_WeaponHandle : MonoBehaviour
{
    public ConfigWeapon mainWeapon;
    public ConfigWeapon secondWeapon;

    //public Dictionary<int, WeaponData> weaponDataList = new Dictionary<int, WeaponData>();
    public Dictionary<int, WeaponAttribute> weaponAttributes = new Dictionary<int, WeaponAttribute>();

    public bool active = true;
    public Transform handPoint;
    public Transform pistolPoint;
    public Transform riflePoint;
    public Transform shootPoint;
    public Transform leftHand;
    public int currentWeapon = 0;
    public int targetWeapon = 0;
    public bool locked = false;

    public float spreadAdd = 4f;
    public float spreadDec = 5f;
    
    private void Start()
    {
        if (mainWeapon)
        {
            InstallWeapon(1, mainWeapon, true);
            targetWeapon = 1;
        }
        if (secondWeapon)
        {
            InstallWeapon(2, secondWeapon, !(targetWeapon == 0));
            if (targetWeapon == 0)
            {
                targetWeapon = 2;
            }
        }
    }

    public void InstallWeapon(int index, ConfigWeapon config, bool isActive)
    {
        var weapon = GameObject.Instantiate(config.model, handPoint);
        var weaponAtt = weapon.GetComponent<WeaponAttribute>();
        
        weaponAtt.runtimeMag = weaponAtt.mag - 1;
        weaponAtt.bore = true;

        weaponAtt.constraint.SetSource(0, new ConstraintSource()
        {
            sourceTransform = (int)weaponAtt.type == 1 ? riflePoint : pistolPoint,
            weight = 1
        });
        
        weaponAtt.constraint.weight = 1;
        weaponAtt.interval = 60f / weaponAtt.fireSpeed;
        weaponAtt.Init(this.gameObject, config);

        weaponAtt.active = isActive;

        weaponAttributes.Add(index, weaponAtt);

    }

    public void Reset()
    {
        if (weaponAttributes.ContainsKey(currentWeapon))
        {
            var att = weaponAttributes[currentWeapon];
            var stateName = att.runningState;
            att.states[stateName].Exit();
            att.states[stateName]._active = false;
            att.states[stateName]._enterTick = false;
            att.states[stateName]._exitTick = false;
        }
        foreach (var weapon in weaponAttributes.Values)
        {
            weapon.runtimeMag = weapon.mag;
        }

        locked = false;

    }

}


