using UnityEngine;
using DG.Tweening;
using System;

public class Timer {
    public bool isRunning;
    float time = 0;
    float run = 0;
    public float rate = 0f;
    public Action OnComplet;

    public Timer(){}
    public Timer(float t){
        this.time = t;
    }

    public void Update(float dt){
        if (this.isRunning)
        {
            this.run += dt;
            this.rate = this.run / this.time;
            if (this.run >= this.time)
            {
                Stop();
                OnComplet?.Invoke();
            }
        }
    }

    public void Update()
    {
        this.Update(Time.deltaTime);
    }
    public void FixedUpdate()
    {
        this.Update(Time.fixedDeltaTime);
    }

    public void Enter(float t){
        this.time = t;
        this.rate = 0;
        this.run = 0;
        this.isRunning = true;
    }
    public void Enter(){
        if (this.time <= 0)
        {
            return;
        }
        this.run = 0;
        this.isRunning = true;
    }

    public void Exit()
    {
        this.Stop();
    }
    void Stop(){
        this.isRunning = false;
        this.run -= this.time;
    }
}