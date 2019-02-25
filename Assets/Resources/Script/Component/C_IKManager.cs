using System.Collections;
using System.Collections.Generic;
using UnityEngine.Animations;
using UnityEngine;
using RootMotion.FinalIK;

public class C_IKManager : MonoBehaviour {
    public BipedIK bipedIK;
    public AimIK aimIK;
    public RagdollMgr ragdollMgr;

    public IKHandle LeftHandIKHandle;

    public bool isAim;      //是否是瞄准
    public bool isHold;     // 是否持枪，使左手IK生效

    public float targetAimWidth = 0;
    public float targetBodyWeight = 0.2f;
    public float targetHold = 1;
    public Vector3 targetAxis;

    public void SetAim(bool aim)
    {
        isAim = aim;
        targetAimWidth = aim ? 1f : 0;
        targetBodyWeight = aim ? 0 : 0.2f;
    }

    public void SetHold(bool hold)
    {
        isHold = hold;
        targetHold = hold ? 1 : 0;
    }

    public void SetHoldTarget(Transform t)
    {
        bipedIK.solvers.leftHand.target = t;
    }
    //public void SetHoldTarget(Transform target)
    //{
    //    LeftHandIKHandle.constraint.SetSource(0, new ConstraintSource()
    //    {
    //        sourceTransform = target,
    //        weight = 1
    //    });

    //}

    public void SetDead(bool active)
    {
        bipedIK.enabled = !active;
        aimIK.enabled = !active;
        //ragdollMgr.SetRagdollActive(active);
}
    
}
