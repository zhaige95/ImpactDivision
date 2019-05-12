using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarMeshRanderMgr : MonoBehaviour {

    public MeshRenderer[] meshRenderers;
    public SkinnedMeshRenderer[] skinnedMeshRenderers;

    public void SetEnabled(bool enable)
    {
        foreach (var item in meshRenderers)
        {
            item.enabled = enable;
        }
        foreach (var item in skinnedMeshRenderers)
        {
            item.enabled = enable;
        }
    }

}
