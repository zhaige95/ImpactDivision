using UnityEngine;
using System.Collections;
using System;

public class ControlSettingMgr : MonoBehaviour
{
    public SliderOption mouseSpeedPri;
    public SliderOption mouseSpeedAim;
    public void Init()
    {
        var setting = Battle.systemSettingSave;
        mouseSpeedPri.Init(setting.mouseSpeed);
        mouseSpeedAim.Init(setting.aimSpeed);

        mouseSpeedPri.OnChanged = SetPrimarySpeed;
        mouseSpeedAim.OnChanged = SetAimingSpeed;

        SetPrimarySpeed(setting.mouseSpeed);
        SetAimingSpeed(setting.aimSpeed);
    }
    
    public void SetPrimarySpeed(float val)
    {
        Battle.mouseSpeedPrimary = val * 0.05f;
        Battle.systemSettingSave.mouseSpeed = val;
    }

    public void SetAimingSpeed(float val)
    {
        Battle.mouseSpeedAiming = val * 0.05f;
        Battle.systemSettingSave.aimSpeed = val;
    }

}
