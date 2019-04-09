using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillIconAnima : MonoBehaviour {

    public RectTransform ringTrans;
    public RawImage ringImage;
    public AnimationCurve curvesForSize;
    public AnimationCurve curvesForAlpha;
    public bool active;
    public float duration = 0f;
    public float dieTime = 0f;
    Timer timer;
	// Use this for initialization
	void Start () {
        timer = new Timer(duration);
        timer.Enter();
        GameObject.Destroy(this.gameObject, dieTime);
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        timer.FixedUpdate();
        if (this.active)
        {
            var timeRate = timer.rate;
            var size = curvesForSize.Evaluate(timeRate);
            ringTrans.sizeDelta = new Vector2(size, size);

            ringImage.color = new Color(255,255,255, curvesForAlpha.Evaluate(timeRate));
            active = timer.isRunning;
        }

	}

    public void SetActive(bool active)
    {
        if (active)
        {
            timer.Enter();
        }
        else
        {
            ringImage.color = new Color(255, 255, 255, curvesForAlpha.Evaluate(0));
            active = timer.isRunning;
        }
    }

}
