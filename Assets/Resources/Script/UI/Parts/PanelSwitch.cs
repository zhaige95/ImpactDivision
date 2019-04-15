using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelSwitch : MonoBehaviour {
    
	public virtual void SwitchPanel(bool isOpen)
    {
        this.transform.localScale = isOpen ? Vector3.one : Vector3.zero;
    }
    public virtual void SwitchPanel()
    {
        transform.localScale = transform.localScale == Vector3.zero ? Vector3.one : Vector3.zero;
    }
}
