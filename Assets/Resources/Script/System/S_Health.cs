using Unity.Entities;
using UnityEngine;
using UiEvent;

public class S_Health : ComponentSystem {
    struct Group {
        public C_Velocity _Velocity;
        public C_AttackListener _AttackListener;
        public C_Attributes _Attributes;
        public CS_StateMgr _StateMgr;
        public C_BattleMgr _BattleMgr;
    }

    protected override void OnUpdate()
    {
        //foreach (var e in GetEntities<Group>())
        //{
        //    var _attribute = e._Attributes;

        //    e._AttackListener.isActive = _attribute.HP > 0;
        //}
    }
    
}
