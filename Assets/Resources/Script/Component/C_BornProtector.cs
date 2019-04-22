using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_BornProtector : MonoBehaviour {
    public Timer timer = new Timer();
    public FloatData protectTime;
    public Transform tipRingTrans;
    public bool active = false;

    public void Enter()
    {
        this.timer.Enter(protectTime.value);
        this.tipRingTrans.localScale = Vector3.one;
        this.active = true;
    }

    public void Exit()
    {
        this.timer.Exit();
        this.tipRingTrans.localScale = Vector3.zero;
        this.active = false;
    }
}
