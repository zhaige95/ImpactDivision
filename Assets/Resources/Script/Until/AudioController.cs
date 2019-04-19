using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class AudioController : MonoBehaviour {
    public AudioSource audioSource;
    public AnimationCurve curve;
    public float animTime = 1f;
    public bool active = true;
    Timer timer = new Timer();

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start () {
        timer.Enter(this.animTime);
	}
	
    private void FixedUpdate()
    {
        timer.FixedUpdate();
        if (active)
        {
            audioSource.volume = curve.Evaluate(timer.rate);
            if (!timer.isRunning)
            {
                this.active = false;
            }
        }
    }
}
