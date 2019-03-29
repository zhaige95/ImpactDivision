using UnityEngine;
using System.Collections;
using UnityEngine.Animations;

public class ASR_Pick : WeaponState
{
    [Header("[Extra Properties]")]
    public bool _isPicked = false;
    public C_Animator _animator;
    public C_Velocity _velocity;
    

    public override void Init(GameObject obj)
    {
        

    }

    public override bool Listener() {


        return false;
    }

    public override void Enter() {
        

    }

    public override void OnUpdate()
    {
       
    }
    public override void Exit() {
        
        
    }
}
 