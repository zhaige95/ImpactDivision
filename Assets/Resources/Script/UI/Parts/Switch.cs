using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[SerializeField]
public class Switch : MonoBehaviour {
    public Button leftArrow;
    public Button rightArrow;
    public Text content;
    public int CurrentIndex = 0;
    public List<string> options;

    private void Awake()
    {
        content.text = options[0];

        leftArrow.onClick.AddListener(ArrowLeftClick);
        rightArrow.onClick.AddListener(ArrowRightClick);

    }

    public void ArrowLeftClick()
    {
        CurrentIndex--;
        CurrentIndex = Mathf.Clamp(CurrentIndex, 0, options.Count - 1);
        if (options[CurrentIndex] != null)
            content.text = options[CurrentIndex];
    }
    public void ArrowRightClick()
    {
        CurrentIndex++;
        CurrentIndex = Mathf.Clamp(CurrentIndex, 0, options.Count - 1);
        if (options[CurrentIndex] != null)
            content.text = options[CurrentIndex];
    }
}
