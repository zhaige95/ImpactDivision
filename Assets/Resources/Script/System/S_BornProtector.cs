using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class S_BornProtector : ComponentSystem
{
    struct Group
    {
        public C_BornProtector _bornProtect;
        public C_AttackListener _attackListener;
    }
    
    protected override void OnUpdate()
    {
        foreach (var e in GetEntities<Group>())
        {
            var bornProtect = e._bornProtect;
            if (bornProtect.active)
            {
                bornProtect.timer.Update();
                bornProtect.tipRingTrans.Rotate(Vector3.up * 2f);
                if (!bornProtect.timer.isRunning) {
                    e._attackListener.isActive = true;
                    bornProtect.Exit();
                }
            }
            
        }
    }
}
