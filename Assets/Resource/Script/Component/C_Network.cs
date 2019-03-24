using UnityEngine;
using System.Collections;
using Game.Actor;
using Snapshots = Game.Actor.Snapshots;
using Game.Network;

public class C_Network : MonoBehaviour
{
    public C_Velocity velocity;
    public C_Camera cameraCarry;
    
    public Identity identity;

    // Use this for initialization
    void Start()
    {
        this.identity.BindEvent(typeof(Snapshots.Move), this.Move);
        this.identity.BindEvent(typeof(Snapshots.Rotate), this.Rotate);
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
        velocity.Dfwd = move.Dfwd;
        velocity.Dbwd = move.Dbwd;
        velocity.Dright = move.Dright;
        velocity.Dleft = move.Dleft;
    }

    private void Rotate(Snapshot snapshot)
    {
        var rotate = snapshot as Snapshots.Rotate;

        if (ServerMgr.Active)
        {
            rotate.rotationX = cameraCarry.camera_x.rotation;
            rotate.rotationY = cameraCarry.camera_y.rotation;
        }
        else
        {
            Debug.Log("set from server");
            cameraCarry.camera_x.rotation = rotate.rotationX;
            cameraCarry.camera_y.rotation = rotate.rotationY;
        }
        velocity.Dmouse_x = rotate.velocity.x;
        velocity.Dmouse_y = rotate.velocity.y;
    }


}
