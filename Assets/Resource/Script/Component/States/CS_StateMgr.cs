using System.Collections;
using System.Collections.Generic;
using System;
using Unity.Entities;
using UnityEngine;

public class CS_StateMgr : MonoBehaviour {

    public Dictionary<string, AvatarState> avatarStates = new Dictionary<string, AvatarState>();
    public string defaultState = "";
    public string runningState = "";

    public void RegState(string name, AvatarState state)
    {
        if (!avatarStates.ContainsKey(name))
        {
            this.avatarStates.Add(name, state);
        }
    }


    public void ExitState(string sName)
    {
        if (avatarStates.ContainsKey(sName))
        {
            this.avatarStates[sName]._exitTick = true;
        }
    }

    public void PauseState(string sName, bool isPause)
    {
        if (avatarStates.ContainsKey(sName))
        {
            if (this.avatarStates[sName]._active)
            {
                this.avatarStates[sName]._exitTick = isPause;
            }

            this.avatarStates[sName]._pause = isPause;
        }
    }

}
