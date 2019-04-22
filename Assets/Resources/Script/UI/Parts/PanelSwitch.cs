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
        SwitchPanel(transform.localScale == Vector3.zero);
    }
}
