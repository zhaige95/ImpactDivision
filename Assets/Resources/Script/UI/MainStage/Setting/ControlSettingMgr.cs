using UnityEngine;
using System.Collections;
using System;

public class ControlSettingMgr : MonoBehaviour
{
    public SliderOption mouseSpeedPri;
    public SliderOption mouseSpeedAim;
    public FloatData mouseSpeedPrimary;
    public FloatData mouseSpeedAiming;
    public void Init()
    {
        var setting = Battle.systemSettingSave;
        mouseSpeedPri.Init(setting.mouseSpeed);
        mouseSpeedAim.Init(setting.aimSpeed);

        mouseSpeedPri.OnChanged = SetPrimarySpeed;
        mouseSpeedAim.OnChanged = SetAimingSpeed;

        mouseSpeedPrimary.value = setting.mouseSpeed * 0.1f;
        mouseSpeedAiming.value = setting.aimSpeed * 0.1f;
    }
    
    public void SetPrimarySpeed(float val)
    {
        mouseSpeedPrimary.value = val * 0.1f;
        Battle.systemSettingSave.mouseSpeed = val;
    }

    public void SetAimingSpeed(float val)
    {
        mouseSpeedAiming.value = val * 0.1f;
        Battle.systemSettingSave.aimSpeed = val;
    }

}
