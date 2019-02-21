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
            var att = e.barMgr._attributes;
            e.barMgr.hpBar.fillAmount = att.HP / att.HPMax;
            e.transform.rotation = Battle.cameraTrans.rotation;
        }
    }
	
}
