using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class S_TinyHpBarMgr : ComponentSystem {
	struct Group{
        public TinyHpBarMgr barMgr;
        public RectTransform transform;
	}

	protected override void OnUpdate()
	{
		foreach (var e in GetEntities<Group>())
		{
            e.transform.rotation = Battle.localPlayerCameraTrans.rotation;
        }
    }
	
}
