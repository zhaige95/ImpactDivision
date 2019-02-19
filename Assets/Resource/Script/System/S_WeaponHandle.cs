using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class S_WeaponHandle : ComponentSystem {
	struct Group{
		public C_WeaponHandle _WeaponHandle;
        public C_Velocity _Velocity;
	}

	protected override void OnUpdate()
	{
		foreach (var e in GetEntities<Group>())
		{
            var _handle = e._WeaponHandle;
            var _velocity = e._Velocity;

            if (_handle.active)
            {
                if (_velocity.DswitchWeapon && !_handle.locked)
                {
                    if (_handle.currentWeapon == 1)
                    {
                        if (_handle.secondWeapon)
                        {
                            _handle.targetWeapon = 2;
                        }
                    }
                    if (_handle.currentWeapon == 2)
                    {
                        if (_handle.mainWeapon)
                        {
                            _handle.targetWeapon = 1;
                        }
                    }

                }

                int currentWeapon = _handle.currentWeapon;
                int targetWeapon = _handle.targetWeapon;

                if (currentWeapon != targetWeapon)
                {

                    foreach (var item in _handle.weaponAttributes.Values)
                    {
                        item.active = false;
                    }

                    _handle.weaponAttributes[targetWeapon].active = true;

                    _handle.currentWeapon = targetWeapon;
                }



                // weapon state process
                foreach (var weaponAtt in _handle.weaponAttributes.Values)
                {
                    if (weaponAtt.active)
                    {
                        var currentState = weaponAtt.states[weaponAtt.runningState];
                        int currentStateLayer = (int)currentState.layer;


                        foreach (int layerIndex in weaponAtt.layerState.Keys)
                        {
                            var nameList = weaponAtt.layerState[layerIndex];
                            if (layerIndex == currentStateLayer)
                            {
                                if (!currentState._unique)
                                {
                                    ListenerPeocess(nameList, weaponAtt);
                                }
                                else if (!currentState._active)
                                {
                                    ListenerPeocess(nameList, weaponAtt);
                                }
                            }
                            else
                            {
                                ListenerPeocess(nameList, weaponAtt);
                            }

                            foreach (string name in nameList)
                            {
                                StateProcess(weaponAtt.states[name]);
                            }

                        }
                    }
                    else
                    {
                        ListenerPeocess(weaponAtt.defaultState, weaponAtt);
                        StateProcess(weaponAtt.states[weaponAtt.defaultState]);
                    }

                }
            }
            
        }
    }
	
    void StateProcess(WeaponState state)
    {
        if (state._active)
        {
            if (state._exitTick)
            {
                state.Exit();
                state._active = false;
                state._enterTick = false;
                state._exitTick = false;
            }
            else
            {
                state.OnUpdate();
            }
        }
    }



    void ListenerPeocess(List<string> nameList, WeaponAttribute attribute)
    {
        foreach (string name in nameList)
        {
            var state = attribute.states[name];

            if (!state._active)
            {
                if (state.Listener())
                {
                    if (attribute.statesLayer[name] == attribute.statesLayer[attribute.runningState])
                    {
                        // exit last state
                        var lastState = attribute.states[attribute.runningState];
                        if (lastState._active)
                        {
                            lastState.Exit();
                            lastState._active = false;
                            lastState._exitTick = false;
                            lastState._enterTick = false;
                        }
                    }
                    
                    attribute.runningState = state._name;
                    state._active = true;
                    state.Enter();
                    break;
                }
            }
        }
    }
    void ListenerPeocess(string name, WeaponAttribute attribute)
    {
        ListenerPeocess(new List<string>() { name }, attribute);
    }

}
