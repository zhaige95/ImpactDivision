using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class C_Attributes : MonoBehaviour {
    
    public Entity entity;
    public float HP = 100f;
    public float HPMax = 100f;
    public float runSpeed = 4f;
    public float jogSpeed = 3.5f;
    public float walkSpeed = 3f;
    public float rate = 1.5f;
    public float jumpForce = 3.2f;
    public float jumpForce2 = 3.2f;
    public int camp = 2;
    public bool isDead = false;
    [Header("[Node]")]
    public Transform tinyHpBarNode;
    public Transform friendMark;
    //private void Start()
    //{
    //    entity = GetComponent<GameObjectEntity>().Entity;
    //}
}
