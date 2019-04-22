using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Animations;
using Data;
using System;

public class WeaponAttribute : MonoBehaviour
{
    public bool active = false;
    public int index;
    public string wname;
    public WeaponType type;
    public MagType magType;
    public int mag;
    public int runtimeMag;
    public bool bore = false; // 枪膛，上膛的子弹
    public float fireSpeed = 600f;
    public float damage = 32f;
    public float reloadTime = 1.3f;
    public float interval = 0f;
    public float spread = 0.5f;
    public float aimSpread = 2f;
    public float runingSpread = 0f;
    public float crouchSpreadRate = 0.5f;
    public float recoilX = 0.5f;
    public float recoilY = 0.5f;
    public bool reload = false;
    public bool ready = false;
    public TransformMark holdOffset;
    public Texture2D cutPicInBattle;
    public Transform holdPoint;
    public Transform shootPoint;
    public Transform OcclusionPoint;
    public GameObject magObj;
    public ParentConstraint constraint;
    public Dictionary<string, WeaponState> states = new Dictionary<string, WeaponState>();
    public Dictionary<string, int> statesLayer = new Dictionary<string, int>();
    public Dictionary<int, List<string>> layerState = new Dictionary<int, List<string>>();
    public Action OnFire;
    [Header("State Property")]
    public string defaultState = "";
    public string lastState = "";
    public string runningState = "";

 
    public void RegState(string name, WeaponState state)
    {
        this.states.Add(name, state);

        int tLayer = (int)state.layer;

        if (layerState.ContainsKey(tLayer))
        {
            layerState[tLayer].Add(name);
        }
        else
        {
            layerState.Add(tLayer, new List<string>() { name } );
        }

        this.statesLayer.Add(name, (int)state.layer);
    }

    public void Init(GameObject obj, ConfigWeapon config)
    {
        // 安装属性
        this.wname = config.wname;
        this.type = config.type;
        this.mag = config.mag;
        this.damage = config.damage;
        this.fireSpeed = config.fireSpeed;
        this.spread = config.spread;
        this.aimSpread = config.aimSpread;
        this.crouchSpreadRate = config.crouchSpreadRate;
        this.recoilX = config.recoilX;
        this.recoilY = config.recoilY;
        this.cutPicInBattle = config.cutPicInBattle;

        this.interval = 60f / fireSpeed;

        // 初始化武器状态组件，传递武器物体的组件等
        foreach (var state in states.Values)
        {
            state.Init(obj);
        }
        
    }

    public float GetRuntimeMag()
    {
        return runtimeMag + (bore ? 1 : 0);
    }
}
