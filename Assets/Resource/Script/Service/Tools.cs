using UnityEngine;
using UnityEditor;
using Unity.Entities;

public class Tools
{
    public static EntityManager _entityManager = World.Active.GetOrCreateManager<EntityManager>();
}