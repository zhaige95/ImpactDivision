using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VersionText : MonoBehaviour {
    public StringData versionConfig;
    public Text versionText;

    private void Start()
    {
        versionText.text = "Alpha Test " + versionConfig.value;
    }

}
