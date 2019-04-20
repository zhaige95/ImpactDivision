using UnityEngine;
using System.Collections;
using UiEvent;
using UiEvent.UiMsgs;

public class WeaponRecover : MonoBehaviour
{
    WeaponAttribute weaponAttribute;
    C_UiEventMgr uiMgr;
    C_Velocity velocity;
    public float waitTime = 0.3f;
    public float recoverSpeed = 0.3f;
    public Timer timer = new Timer();
    // Use this for initialization
    void Awake()
    {
        velocity = GetComponentInParent<C_Velocity>();
        if (!velocity.isLocalPlayer)
        {
            this.enabled = false;
            return;
        }
        uiMgr = GetComponentInParent<C_UiEventMgr>();
        weaponAttribute = GetComponent<WeaponAttribute>();
        weaponAttribute.OnFire = Reset;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (weaponAttribute.active)
        {
            timer.FixedUpdate();
            if (!timer.isRunning)
            {
                weaponAttribute.runingSpread = Mathf.Lerp(weaponAttribute.runingSpread, 0f, recoverSpeed);
                if (velocity.Daim)
                {
                    SendSpreadMsg(weaponAttribute.runingSpread + 5f);
                }
            }
        }
    }

    public void Reset()
    {
        if (waitTime > 0)
        {
            timer.Enter(waitTime);
        }
    }

    void SendSpreadMsg(float v)
    {
        var spreadMsg = new Spread()
        {
            value = v
        };
        uiMgr.SendEvent(spreadMsg);
    }
}
