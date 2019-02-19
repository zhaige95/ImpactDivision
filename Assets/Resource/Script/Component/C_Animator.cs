using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class C_Animator : MonoBehaviour {
	public Animator animator;
    public Dictionary<string, float> targetFloat;
    public Dictionary<int, float> layerTransform;

    void Awake()
    {
        targetFloat = new Dictionary<string, float>();
        layerTransform = new Dictionary<int, float>();
    }

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

    //public void Start()
    //{
    //    this.animator = GetComponentInChildren<Animator>();
    //}
}
