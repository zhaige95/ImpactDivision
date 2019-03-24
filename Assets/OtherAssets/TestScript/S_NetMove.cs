using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class S_NetMove : ComponentSystem {

    struct Group
    {
        public NetMove _move;
        public CharacterController _controller;
    }

    protected override void OnUpdate()
    {

        //foreach (var e in GetEntities<Group>())
        //{
        //    var move = e._move;
        //    var controller = e._controller;

        //    //move.OnUpdate();

        //}
    }
    

}
