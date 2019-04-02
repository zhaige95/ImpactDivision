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
    public ConfigLayer layerConfig;
    public bool isGrounded = true;

    void Start () {
        radius = controller.radius;
        height = controller.height / 2;
        skinWidth = controller.skinWidth;
    }

}
