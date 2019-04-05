using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class C_WeaponHandle : MonoBehaviour
{
    public PhotonView pView;
    public C_Velocity velocity;
    public ConfigWeapon mainWeapon;
    public ConfigWeapon secondWeapon;
    
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

    private void FixedUpdate()
    {
        if (this.active)
        {
            foreach (var weaponAtt in weaponAttributes.Values)
            {
                if (weaponAtt.active)
                {
                    foreach (WeaponState state in weaponAtt.states.Values)
                    {
                        if (state._active)
                        {
                            if (state._exitTick)
                            {
                                state.Exit();
                            }
                            else
                            {
                                state.OnUpdate();
                            }
                        }
                    }

                }
                else
                {
                    
                    var state = weaponAtt.states[weaponAtt.defaultState];
                    if (state._active)
                    {
                        if (state._exitTick)
                        {
                            state.Exit();
                        }
                        else
                        {
                            state.OnUpdate();
                        }
                    }
                }
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
        foreach (var att in weaponAttributes.Values)
        {
            att.states[att.runningState].Exit();
            att.active = false;
            att.ready = false;
            att.runtimeMag = att.mag - 1;
            att.bore = true;
        }
        weaponAttributes[currentWeapon].states["pick"].Enter();
        this.currentWeapon = 0;
        this.targetWeapon = 0;
        locked = false;
    }

    [PunRPC]
    public void NetworkFire(Vector3 targetPoint)
    {
        Fire state = (Fire)weaponAttributes[this.currentWeapon].states["fire"];
        state.targetPoint = targetPoint;
        state.Enter();
    }
    
    [PunRPC]
    public void EnterState(string sName)
    {
        if (weaponAttributes[this.currentWeapon].states.ContainsKey(sName))
        {
            var attr = weaponAttributes[this.currentWeapon];
            var state = attr.states[sName];
            var runningState = attr.states[attr.runningState];
            
            if (state.layer == runningState.layer && runningState._active)
            {
                runningState.Exit();
            }

            state.Enter();
            attr.lastState = attr.runningState;
            attr.runningState = sName;
        }
    }
    
    [PunRPC]
    public void ExitState(string sName)
    {
        if (weaponAttributes[this.currentWeapon].states.ContainsKey(sName))
        {
            var state = weaponAttributes[this.currentWeapon].states[sName];
            if (state._active)
            {
                state.Exit();
            }
        }
    }
    
    [PunRPC]
    public void PickWeapon()
    {
        Debug.Log("pick weapon");
        if (currentWeapon == 0)
        {
            targetWeapon = 1;
        }
        else if (currentWeapon == 1)
        {
            if (secondWeapon)
            {
                targetWeapon = 2;
            }
        }
        else if (currentWeapon == 2)
        {
            if (mainWeapon)
            {
                targetWeapon = 1;
            }
        }
    }
    
}


