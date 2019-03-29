using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UiEvent
{
    public class C_UiEventMgr : MonoBehaviour
    {
        private Dictionary<Type, List<Action<UiMsg>>> eventList = new Dictionary<Type, List<Action<UiMsg>>>();

        public void BindEvent(Type type, Action<UiMsg> func)
        {
            if (eventList.ContainsKey(type))
            {
                var actions = new List<Action<UiMsg>>();
                eventList.TryGetValue(type, out actions);
                actions.Add(func);
            }
            else
            {
                eventList.Add(type, new List<Action<UiMsg>>());
                BindEvent(type, func);
            }
        }

        public void SendEvent(UiMsg msg)
        {
            var type = msg.GetType();

            if (eventList.ContainsKey(type))
            {
                foreach (var func in eventList[type])
                {
                    func(msg);
                }
            }
        }
    }

}



