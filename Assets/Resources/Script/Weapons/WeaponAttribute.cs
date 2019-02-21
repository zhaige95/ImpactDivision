using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Animations;
using Data;

public class WeaponAttribute : MonoBehaviour
{
    public bool active = false;
    public int id;
    public string wname;
    public WeaponType type;
    public MagType magType;
    public string defaultState;
    public string runningState = "";
    public float mag;
    public float runtimeMag;
    public bool bore = false; // 枪膛，上膛的子弹
    public float fireSpeed = 600f;
    public float demage = 32f;
    public float reloadTime = 1.3f;
    public float interval = 0f;
    public float spread = 0.5f;
    public float recoilX = 0.5f;
    public float recoilY = 0.5f;
    public bool reload = false;
    public bool ready = false;
    public TransformMark holdOffset;
    public Texture2D cutTexture;
    public Transform holdPoint;
    public Transform shootPoint;
    public GameObject magObj;
    public ParentConstraint constraint;
    public Dictionary<string, WeaponState> states = new Dictionary<string, WeaponState>();
    public Dictionary<string, int> statesLayer = new Dictionary<string, int>();
    public Dictionary<int, List<string>> layerState = new Dictionary<int, List<string>>();

    private void Awake()
    {
        interval = 60f / fireSpeed;
    }

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

    public void Init(GameObject obj)
    {
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
