using Unity.Entities;
using System;
using UnityEngine;

public class SS_StateMgr : ComponentSystem {
	struct Group{
        public CS_StateMgr stateMgr;
        public C_Attributes _Attributes;
	}

	protected override void OnUpdate()
	{
		foreach (var e in GetEntities<Group>())
		{
            if (!e._Attributes.isDead)
            {
                var _stateMgr = e.stateMgr;

                var currentState = _stateMgr.avatarStates[_stateMgr.runningState];

                if (!currentState._unique)
                {
                    ListenerPeocess(_stateMgr);
                }
                else if (!currentState._active)
                {
                    ListenerPeocess(_stateMgr);
                }

                foreach (AvatarState state in _stateMgr.avatarStates.Values)
                {
                    if (state._active)
                    {
                        state.OnUpdate();
                        if (state._exitTick)
                        {
                            state.Exit();
                            state._active = false;
                            state._exitTick = false;
                            state._enterTick = false;
                        }
                    }
                }

            }

            
		}
	}

    void ListenerPeocess(CS_StateMgr _stateMgr)
    {
        foreach (AvatarState state in _stateMgr.avatarStates.Values)
        {
            if (!state._active)
            {
                if (state.Listener())
                {
                    // exit last state
                    var lastState = _stateMgr.avatarStates[_stateMgr.runningState];
                    lastState.Exit();
                    lastState._active = false;
                    lastState._exitTick = false;
                    lastState._enterTick = false;

                    _stateMgr.runningState = state._name;
                    state._active = true;
                    state.Enter();
                    break;
                }
            }
        }
    }

}
