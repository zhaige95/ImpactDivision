using System.Collections;
using System.Collections.Generic;
using System;
using Unity.Entities;
using UnityEngine;
using Data;

public class CS_StateMgr : MonoBehaviour, IPunObservable {

    public Dictionary<string, AvatarState> avatarStates = new Dictionary<string, AvatarState>();
    public string defaultState = "";
    public string lastState = "";
    public string runningState = "";


    public void RegState(string name, AvatarState state)
    {
        if (!avatarStates.ContainsKey(name))
        {
            this.avatarStates.Add(name, state);
        }
    }


    public void EnterState(string sName)
    {
        if (avatarStates.ContainsKey(sName))
        {
            if (avatarStates[runningState]._active)
            {
                avatarStates[runningState].Exit();
            }
            this.avatarStates[sName].Enter();
            runningState = sName;
        }
    }

    public void ExitState(string sName)
    {
        if (avatarStates.ContainsKey(sName))
        {
            this.avatarStates[sName].Exit();
        }
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            var nextState = this.lastState.Equals(this.runningState) ? "" : this.runningState;

            stream.SendNext(nextState);
            this.lastState = this.runningState;
        }
        else if (stream.isReading)
        {
            var nextState = (string)stream.ReceiveNext();

            if (!nextState.Equals(""))
            {
                this.ExitState(runningState);
                this.EnterState(nextState);
                this.lastState = this.runningState;
            }
        }
        
    }

}
