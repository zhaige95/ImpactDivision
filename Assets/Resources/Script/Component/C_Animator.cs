using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class C_Animator : MonoBehaviour, IPunObservable {
	public Animator animator;
    C_Velocity velocity;
    public Dictionary<string, float> targetFloat;
    public Dictionary<int, float> layerTransform;

    void Awake()
    {
        velocity = GetComponent<C_Velocity>();
        targetFloat = new Dictionary<string, float>();
        layerTransform = new Dictionary<int, float>();
    }

    //public void Start()
    //{
    //    this.animator = GetComponentInChildren<Animator>();
    //}

    public void AddEvent(string name, float target)
    {
        if (!targetFloat.ContainsKey(name))
        {
            targetFloat.Add(name, target);
        }
        else
        {
            targetFloat[name] = target;
        }
    }

    public void AddLayerTrans(int layerIndex, float targetWeight)
    {
        if (!layerTransform.ContainsKey(layerIndex))
        {
            layerTransform.Add(layerIndex, targetWeight);
        }
        else
        {
            layerTransform[layerIndex] = targetWeight;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(this.animator.GetFloat("aim"));
        }
        else if (stream.isReading)
        {
            var aimVal = (float)stream.ReceiveNext();
            this.animator.SetFloat("aim", aimVal);
            velocity.aiming = aimVal >= 0 ? true : false;

        }
    }
}
