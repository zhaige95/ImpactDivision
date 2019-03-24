using System;
using UnityEngine;

public class C_Input : MonoBehaviour{

    public bool isActive = true;
    public string fwd = "w";
    public string bwd = "s";
    public string right = "d";
    public string left = "a";
    public string jump = "space";
    public string mouse_x = "Mouse X";
    public string mouse_y = "Mouse Y";
    public string flash = "left shift";
    public string openFire = "mouse 0";
    public string reload = "r";
    public string aim = "mouse 1";
    public string switchWeapon = "q";
    public string crouch = "left ctrl";
    public string pickRifle = "1";
    public string pickPistol = "2";
    public string combat = "e";
    public string run = "left shift";
    public string cutCameraSide = "tab";

    public C_Velocity _velocity;

    private void Start()
    {
        _velocity = GetComponent<C_Velocity>();
    }

    private void FixedUpdate()
    {
        if (_velocity.isActive)
        {
            _velocity.Dfwd = Input.GetKey(this.fwd);
            _velocity.Dbwd = Input.GetKey(this.bwd);
            _velocity.Dleft = Input.GetKey(this.left);
            _velocity.Dright = Input.GetKey(this.right);
            _velocity.Djump = Input.GetKeyDown(this.jump);
            _velocity.Dmouse_x = Input.GetAxis(this.mouse_x);
            _velocity.Dmouse_y = Input.GetAxis(this.mouse_y);
            _velocity.Dflash = Input.GetKey(this.flash);
            _velocity.DfireHold = Input.GetKey(this.openFire);
            _velocity.DfirePressed = Input.GetKeyDown(this.openFire);
            _velocity.Dreload = Input.GetKey(this.reload);
            _velocity.Daim = Input.GetKey(this.aim);
            _velocity.DswitchWeapon = Input.GetKeyDown(this.switchWeapon);
            _velocity.DpickRifle = Input.GetKeyDown(this.pickRifle);
            _velocity.DpickPistol = Input.GetKeyDown(this.pickPistol);
            _velocity.Dcombat = Input.GetKeyDown(this.combat);
            _velocity.Dcrouch = Input.GetKeyDown(this.crouch);
            _velocity.DcutCameraSide = Input.GetKeyDown(this.cutCameraSide);

            if (Input.GetKeyDown(this.run))
            {
                _velocity.Drun = !_velocity.Drun;
            }

        }
    }

}


