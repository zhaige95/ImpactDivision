using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class S_OnGroundSensor : ComponentSystem {
	struct Group{
		public C_OnGroundSensor _Sensor;
        public Transform _Transform;
	}

	protected override void OnUpdate()
	{
		foreach (var e in GetEntities<Group>())
		{
            var _sensor = e._Sensor;
            var _trans = e._Transform;

            _sensor.point1 = _trans.position + _trans.up * (_sensor.radius - _sensor.skinWidth * 2);
            _sensor.point2 = _sensor.point1 + _trans.up * _sensor.height - _trans.up * _sensor.radius;
            Collider[] outputColliders = Physics.OverlapCapsule(_sensor.point1, _sensor.point2, _sensor.radius, LayerMask.GetMask("Collide"));

            _sensor.isGrounded = outputColliders.Length > 0;
        }
	}
	
}

            
