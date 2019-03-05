using UnityEngine;
using System.Collections;

namespace UiEvent
{
    public class UiMsg { }

    namespace UiMsgs
    {
        public class Hp : UiMsg
        {
            public float hp;
            public float hpMax;
        }

        public class Ammo : UiMsg
        {
            public float ammo;
            public float mag;
        }

    }
}
