using Unity.Entities;
using UnityEngine;
using UiEvent;

public class S_BattleKillMsg : ComponentSystem {
    struct Group {
        public BattleKillMsgItem msgItem;
    }

    protected override void OnUpdate()
    {
        foreach (var e in GetEntities<Group>())
        {
            var msgItem = e.msgItem;
            if (msgItem.isActive)
            {
                msgItem.timer.Update();
                if (!msgItem.timer.isRunning)
                {
                    msgItem.Active(false);
                }
            }
        }
    }
    
}
