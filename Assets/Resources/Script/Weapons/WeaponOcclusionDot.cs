using UnityEngine;
using System.Collections;
using UiEvent;
using UiEvent.UiMsgs;

public class WeaponOcclusionDot : MonoBehaviour
{
    public float OcclusionSensorDistance = 2f;
    public ConfigLayer occlusionLayer;
    C_UiEventMgr uiMgr;
    RaycastHit hitInfo;
    Transform occlusionPoint;
    // Use this for initialization
    void Start()
    {
        occlusionPoint = GetComponentInParent<C_WeaponHandle>().OcclusionPoint;
        uiMgr = GetComponentInParent<C_UiEventMgr>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        var visi = false;
        var posi = Vector3.zero;
        if (Physics.Raycast(occlusionPoint.position, occlusionPoint.forward, out hitInfo, OcclusionSensorDistance, occlusionLayer.layerMask))
        {
            visi = true;
            posi = hitInfo.point;
        }
        
        var msg = new Dot()
        {
            visible = visi,
            position = posi
        };
        uiMgr.SendEvent(msg);

    }

}
