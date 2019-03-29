using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class S_IKmgr : ComponentSystem {
	struct Group{
        public C_IKManager iKManager;
	}

	protected override void OnUpdate()
	{
		foreach (var e in GetEntities<Group>())
		{
			var _ik = e.iKManager;
            var _ikAim = _ik.aimIK;
            var _ikBiped = _ik.bipedIK;

            _ikAim.solver.IKPositionWeight = Mathf.Lerp(_ikAim.solver.IKPositionWeight, _ik.targetAimWidth, 0.3f);
            _ikAim.solver.axis = Vector3.Lerp(_ikAim.solver.axis, _ik.targetAxis, 0.1f);
           
            _ikBiped.solvers.leftHand.IKPositionWeight = Mathf.Lerp(_ikBiped.solvers.leftHand.IKPositionWeight, _ik.targetHold, 0.3f);
            _ikBiped.solvers.leftHand.IKRotationWeight = Mathf.Lerp(_ikBiped.solvers.leftHand.IKRotationWeight, _ik.targetHold, 0.3f);
            _ikBiped.solvers.leftHand.bendModifierWeight = Mathf.Lerp(_ikBiped.solvers.leftHand.bendModifierWeight, _ik.targetHold, 0.3f);



        }
    }
	
}
