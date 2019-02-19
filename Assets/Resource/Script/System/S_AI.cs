using Unity.Entities;
using UnityEngine;
using System.Collections;

public class S_AI : ComponentSystem {
	struct Group{
        public C_AI _AI;
        public C_Velocity _Velocity;
    }

	protected override void OnUpdate()
	{
		foreach (var e in GetEntities<Group>())
		{
            var _ai = e._AI;
            var _velocity = e._Velocity;

            var dir = Random.Range(_ai.direction[0], _ai.direction[1]);

            if (!_ai.timer.isRunning)
            {
                

                _ai.timer.Enter();

            }

            

        }
    }
}
