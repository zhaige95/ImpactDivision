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
    public bool locked = false;

    public float targetAimWidth = 0f;
    public float targetHold = 1f;
    public float targetLookWeight = 1f;
    public float targetBodyWeight = 0.2f;
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

    public void SetLook(bool look)
    {
        bipedIK.solvers.lookAt.SetLookAtWeight(look ? 1f : 0f);
    }

    public void SetHoldTarget(Transform t)
    {
        bipedIK.solvers.leftHand.target = t;
    }


    public void SetDead(bool active)
    {
        bipedIK.enabled = !active;
        aimIK.enabled = !active;
        ragdollMgr.SetRagdollActive(active);
    }

}
