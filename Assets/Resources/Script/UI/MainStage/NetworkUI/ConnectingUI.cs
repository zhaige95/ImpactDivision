using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ConnectingUI : MonoBehaviour {
    public Image image;
    public DOTweenAnimation tweenAnimation;
	
    public void StopAnimation()
    {
        image.fillAmount = 0;
        tweenAnimation.DOPause();
    }

    public void PlayAnimation()
    {
        image.fillAmount = 0.2f;
        tweenAnimation.DOPlay();
    }

}
