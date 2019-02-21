using System;
using UnityEngine;
using Unity.Entities;

[Serializable]
public class C_EntityHandle : MonoBehaviour{

    public Entity entity;
    private void Start()
    {
        entity = GetComponent<GameObjectEntity>().Entity;
    }
}


