using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class WeaponData : MonoBehaviour {
    
    public enum WeaponType
    {
        Rifle = 1, Pistol = 2
    }
    public enum MagType
    {
        withMag = 1, withoutMag = 2
    }
    public enum TriggerType
    {
        single = 1, automatic = 2
    }

    public bool active = false;
    public int id;
    public WeaponType wtype;
    public MagType hasMag;
    public TriggerType triggerType;
    public GameObject bullet;
    public AudioClip soundFire;
    public AudioClip soundReload;
    public string wname = "test";
    public float fireSpeed = 600;
    public float demage = 32;
    public float mag = 30;
    public float reloadTime = 1.3f;

    public Transform holdPoint;
    public Transform holdoffset;
    public Transform dropOffset;
    public Transform shootPoint;
    public Transform magPoint;
    public GameObject muzzleFlash;
    public GameObject magPrefab;
    public GameObject magObj;
    public ParentConstraint constraint;
    public Vector3 axis;
    public Vector3 fireAxis;
    public float runtimeMag = 0;
    public float interval = 0;
    public float spread = 0.5f;
    public float recoilX = 0.5f;
    public float recoilY = 0.5f;
    public Dictionary<string, WeaponState> states = new Dictionary<string, WeaponState>();
    
    public void RegState(string name, WeaponState state)
    {
        this.states.Add(name, state);
    }

    public void Init(GameObject obj)
    {
        foreach (var state in states.Values)
        {
            state.Init(obj);
        }
    }
    
}
