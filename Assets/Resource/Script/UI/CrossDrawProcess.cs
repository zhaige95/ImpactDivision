using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossDrawProcess : MonoBehaviour {
    public GameObject crossIdle;
    public GameObject crossRun;
    public GameObject crossAim;
    public IntData crossType;
    public int lastType;

    // Use this for initialization
    void Start () {
        lastType = crossType.value;
        Process(crossType.value);
    }
	
	// Update is called once per frame
	void Update () {
        if (lastType != crossType.value)
        {
            Process(crossType.value);
        }
	}

    void Process(int type)
    {

        switch (lastType)
        {
            case 1: crossIdle.SetActive(false); break;
            case 2: crossRun.SetActive(false); break;
            case 3: crossAim.SetActive(false); break;
            default:
                break;
        }

        switch (type)
        {
            case 1: crossIdle.SetActive(true); break;
            case 2: crossRun.SetActive(true); break;
            case 3: crossAim.SetActive(true); break;
            default:
                break;
        }
        
        lastType = crossType.value;
    }

}
