using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_OnGroundSensor : MonoBehaviour {
    public CharacterController controller;

    [HideInInspector]
    public Vector3 point1;
    [HideInInspector]
    public Vector3 point2;
    [HideInInspector]
    public float radius;
    [HideInInspector]
    public float height;
    [HideInInspector]
    public float skinWidth;
    public LayerMask layer;
    public bool isGrounded = true;

    void Start () {
        radius = controller.radius;
        height = controller.height;
        skinWidth = controller.skinWidth;
    }

}
