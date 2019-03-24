using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;

public class RagdollMgr : MonoBehaviour {
    public Rigidbody[] rigidbodys;
    public CapsuleCollider[] capsuleColliders;
    public BoxCollider[] boxColliders;
    public SphereCollider sphereCollider;

    public RagdollUtility ragdollUtility;
	
    public void SetRagdollActive(bool active)
    {
        for (int i = 0; i < rigidbodys.Length; i++)
        {
            rigidbodys[i].useGravity = active;
            rigidbodys[i].isKinematic = !active;
        }
        //for (int i = 0; i < capsuleColliders.Length; i++)
        //{
        //    capsuleColliders[i].isTrigger = !active;
        //}
        //for (int i = 0; i < boxColliders.Length; i++)
        //{
        //    boxColliders[i].isTrigger = !active;
        //}

        //sphereCollider.isTrigger = !active;
    }
    
}
