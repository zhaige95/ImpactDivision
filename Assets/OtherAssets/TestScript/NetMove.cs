using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Actor;
using Snapshots = Game.Actor.Snapshots;
using Game.Network;

public class NetMove : MonoBehaviour {

    public float speed = 5;

    public bool Dfwd = false;
    public bool Dbwd = false;
    public bool Dright = false;
    public bool Dleft = false;

    public float fwd = 0;
    public float right = 0;

    public CharacterController controller;
    public Identity identity;

    private void Awake()
    {
        identity = GetComponent<Identity>();
        controller = GetComponent<CharacterController>();
        this.identity.BindEvent(typeof(Snapshots.Move), this.Move);

    }

    protected void FixedUpdate()
    {
        if (this.identity.IsPlayer)
        {
            Dfwd = Input.GetKey("w");
            Dbwd = Input.GetKey("s");
            Dleft = Input.GetKey("a");
            Dright = Input.GetKey("d");
            if (Dfwd || Dbwd || Dright || Dleft)
            {
                var move = new Snapshots.Move()
                {
                    Dfwd = this.Dfwd,
                    Dbwd = this.Dbwd,
                    Dleft = this.Dleft,
                    Dright = this.Dright,
                    position = this.transform.position
                };
                this.identity.Input(move);
            }
        }

    }

    public void Simulate()
    {
        this.fwd = (Dfwd ? 1f : 0) + (Dbwd ? -1f : 0);
        this.right = (Dright ? 1f : 0) + (Dleft ? -1f : 0);
        var currentSpeed = this.speed;
        controller.Move(transform.forward * currentSpeed * fwd * Time.deltaTime +
                    transform.right * currentSpeed * right * Time.deltaTime);
    }

    private void Move(Snapshot snapshot)
    {
        var move = snapshot as Snapshots.Move;

        if (ServerMgr.Active)
        {
            move.position = this.transform.position;
        }
        else
        {
            this.transform.position = move.position;
        }
        Dfwd = move.Dfwd;
        Dbwd = move.Dbwd;
        Dleft = move.Dleft;
        Dright = move.Dright;
    }

}
