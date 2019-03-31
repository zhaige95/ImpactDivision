using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Velocity : MonoBehaviour, IPunObservable {
    public bool isActive = true;
    public bool Dfwd = false;
    public bool Dbwd = false;
    public bool Dright = false;
    public bool Dleft = false;
    public bool Djump = false;
    public float Dmouse_x = 0;
    public float Dmouse_y = 0;
    public bool Dflash = false;
    public bool DfirePressed = false;
    public bool DfireHold = false;
    public bool Dreload = false;
    public bool Daim = false;
    public bool DswitchWeapon = false;
    public bool Dcrouch = false;
    public bool DpickRifle = false;
    public bool DpickPistol = false;
    public bool Dcombat = false;
    public bool Drun = false;
    public bool DcutCameraSide = false;

    [Header("-------------------------------------------")]
    public bool isLocalPlayer = false;
	
	public float fwd = 0;
	public float right = 0;
	public float mouse = 0;
    
    // ------------------------------
    public bool idle = true;
    public bool crouch = false;
	public bool jumping = false;
    
    public bool aiming = false;
    public bool armed = false;

    public float currentSpeed = 0;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(this.fwd);
            stream.SendNext(this.right);
        }
        else if (stream.isReading)
        {
            this.fwd = (float)stream.ReceiveNext();
            this.right = (float)stream.ReceiveNext();
        }
    }

    public void Reset()
    {
        Dfwd = false;
        Dbwd = false;
        Dright = false;
        Dleft = false;
        Djump = false;
        Dmouse_x = 0;
        Dmouse_y = 0;
        Dflash = false;
        DfirePressed = false;
        DfireHold = false;
        Dreload = false;
        Daim = false;
        DswitchWeapon = false;
        Dcrouch = false;
        DpickRifle = false;
        DpickPistol = false;
        Dcombat = false;
        Drun = false;
    
        fwd = 0;
        right = 0;
        mouse = 0;
        
        idle = true;
        crouch = false;
        jumping = false;

        aiming = false;
        //armed = false;
        DcutCameraSide = false;
    }
}
