using UnityEngine;
using System.Collections;

public class VideoMgr : WindowBasic
{
    public SwitchOption syncOption;
    public SwitchOption frameOption;

    override public void Init()
    {
        SetFromSave();
        syncOption.OnChangeCall = UpdateFrameRateOptionInteractable;
    }

    private void OnEnable()
    {
        Debug.Log(syncOption.currentIndex);
        UpdateFrameRateOptionInteractable();
    }

    public void ResetSetting()
    {
        syncOption.ResetSetting();
        frameOption.ResetSetting();
    }

    void SetFromSave()
    {
        int syncIndex = Battle.systemSettingSave.sync;
        SetSync(syncIndex);

        int frameRateIndex = Battle.systemSettingSave.frameRate;
        SetFrameRate(frameRateIndex);
    }

    void SetFromGame()
    {
        int syncIndex = Battle.systemSettingSave.sync;
        SetSync(syncIndex);

        int frameRateIndex = Battle.systemSettingSave.frameRate;
        SetFrameRate(frameRateIndex);
    }

    public void ApplyChanges()
    {
        var setting = Battle.systemSettingSave;
        setting.sync = syncOption.currentIndex;
        setting.frameRate = frameOption.currentIndex;

        Battle.SaveSystemSettingData();

        SetFromGame();
    }

    // 更新帧率设置UI的可操作状态，开启垂直同步时禁用帧率设置
    public void UpdateFrameRateOptionInteractable()
    {
        if (syncOption.currentIndex == 0)
        {
            frameOption.SetInteractable(true);
        }
        else if (syncOption.currentIndex == 1)
        {
            frameOption.SetInteractable(false);
        }
    }

    public void SetSync(int index)
    {
        syncOption.SetValue(index);
        QualitySettings.vSyncCount = index;
    }

    public void SetFrameRate(int index)
    {
        frameOption.SetValue(index);
        switch (index)
        {
            case 0:Application.targetFrameRate = 30;
                break;
            case 1:
                Application.targetFrameRate = 40;
                break;
            case 2:
                Application.targetFrameRate = 45;
                break;
            case 3:
                Application.targetFrameRate = 50;
                break;
            case 4:
                Application.targetFrameRate = 60;
                break;
            case 5:
                Application.targetFrameRate = 80;
                break;
            case 6:
                Application.targetFrameRate = 90;
                break;
            case 7:
                Application.targetFrameRate = 120;
                break;
            default:
                break;
        }
    }


}
