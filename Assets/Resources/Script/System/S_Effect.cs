using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class S_Effect : ComponentSystem {
	struct Group{
		public C_Effect _Effect;
        public Transform _Transform;
	}
    public RaycastHit hit;
	protected override void OnUpdate()
	{
		foreach (var e in GetEntities<Group>())
		{
            var _effect = e._Effect;
            if (_effect.isActive)
            {
                _effect.timer.Update();
                
                if (!_effect.timer.isRunning)
                {
                    _effect.isActive = false;
                }
            }
        }
    }

}
