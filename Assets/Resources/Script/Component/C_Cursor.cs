using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Cursor : MonoBehaviour {

    public bool isLocked = true;

    private void Start()
    {
        Cursor.lockState = isLocked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !isLocked;
    }

}
