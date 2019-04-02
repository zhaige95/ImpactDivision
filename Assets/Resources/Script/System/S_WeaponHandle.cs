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
                    _handle.pView.RPC("PickWeapon", PhotonTargets.All);
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
                if (_velocity.isLocalPlayer)
                {
                    foreach (var weaponAtt in _handle.weaponAttributes.Values)
                    {
                        if (weaponAtt.active)
                        {
                            var currentState = weaponAtt.states[weaponAtt.runningState];
                            int currentStateLayer = (int)currentState.layer;

                            // 取状态层级
                            foreach (int layerIndex in weaponAtt.layerState.Keys)
                            {
                                // 取当前层的状态列表
                                var nameList = weaponAtt.layerState[layerIndex];

                                if (layerIndex == currentStateLayer)
                                {
                                    if (!currentState._active)
                                    {
                                        ListenerProcess(nameList, weaponAtt);
                                    }
                                    else if (!currentState._unique)
                                    {
                                        ListenerProcess(nameList, weaponAtt);
                                    }
                                }
                                else
                                {
                                    ListenerProcess(nameList, weaponAtt);
                                }
                            }
                        }
                        else
                        {
                            ListenerProcess(weaponAtt.defaultState, weaponAtt);
                        }
                    }
                }
                
                
            }
            
        }
    }
    
    bool ListenerProcess(List<string> nameList, WeaponAttribute attribute)
    {
        foreach (string name in nameList)
        {
            if (ListenerProcess(name, attribute) )
            {
                return true;
            }
        }
        return false;
    }
    bool ListenerProcess(string name, WeaponAttribute attribute)
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
                    }
                }

                attribute.runningState = state._name;
                state.Enter();
                return true;
            }
        }

        return false;
    }

}
