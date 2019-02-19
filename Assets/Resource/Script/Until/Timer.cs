using UnityEngine;
using DG.Tweening;
using System;

public class Timer {
    public bool isRunning;
    public float time = 0;
    public float rate = 0;
    public Action OnComplet;

    public Timer(){}
    public Timer(float t){
        this.time = t;
    }

    public void Update(){
        if (this.isRunning)
        {
            if (Time.time - this.rate >= this.time)
            {
                Stop();
            }
        }
    }

    public void Enter(float t){
        this.time = t;
        this.rate = Time.time;
        this.isRunning = true;
    }
    public void Enter(){
        if (this.time == 0)
        {
            Debug.LogWarning("timer is 0!");
        }
        this.rate = Time.time;
        this.isRunning = true;
    }

    public void Exit()
    {
        this.Stop();
    }
    void Stop(){
        this.isRunning = false;
        this.rate = 0;
    }
}