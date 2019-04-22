using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;

public class BloodScreenMgr : MonoBehaviour {
    public Image image;
    public AnimationCurve curve;
    Timer timer = new Timer();
    public float animTime = 2f;
    
    public void Enter()
    {
        Debug.Log("enter blood screen");
        timer.Enter(animTime);
    }

	// Update is called once per frame
	void Update () {
        timer.Update();
        if (timer.isRunning)
        {
            var alpha = curve.Evaluate(timer.rate);
            image.color = new Color(1, 1, 1, alpha);
        }
	}
}
