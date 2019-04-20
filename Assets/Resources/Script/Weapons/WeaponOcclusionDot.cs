using UnityEngine;
using System.Collections;
using UiEvent;
using UiEvent.UiMsgs;

public class WeaponOcclusionDot : MonoBehaviour
{
    C_Camera myCamera;
    C_Velocity velocity;
    WeaponAttribute weaponAttribute;
    public float OcclusionSensorDistance = 2f;
    public ConfigLayer occlusionLayer;
    C_UiEventMgr uiMgr;
    RaycastHit hitInfo;
    Transform occlusionPoint;
    // Use this for initialization
    void Awake()
    {
        velocity = GetComponentInParent<C_Velocity>();
        if (!velocity.isLocalPlayer)
        {
            this.enabled = false;
            return;
        }
        myCamera = GetComponentInParent<C_Camera>();
        weaponAttribute = GetComponent<WeaponAttribute>();
        occlusionPoint = GetComponentInParent<C_WeaponHandle>().OcclusionPoint;
        uiMgr = GetComponentInParent<C_UiEventMgr>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {

        
        if (weaponAttribute.active)
        {
            var visi = false;
            var posi = Vector3.zero;
            var aimPoint = myCamera.GetAimPoint();

            if (Physics.Raycast(occlusionPoint.position, aimPoint - occlusionPoint.position, out hitInfo, OcclusionSensorDistance, occlusionLayer.layerMask))
            {
                visi = true;
                posi = hitInfo.point;
            }
            if (!velocity.Daim)
            {
                visi = false;
            }
            var msg = new Dot()
            {
                visible = visi,
                position = posi
            };
            uiMgr.SendEvent(msg);
        }
        
    }

}
