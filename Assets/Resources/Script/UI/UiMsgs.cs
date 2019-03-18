using UnityEngine.UI;
using System.Collections;

namespace UiEvent
{

    public class UiMsg { }

    namespace UiMsgs
    {
        using UnityEngine;
        using UnityEngine.UI;
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
        public class Hit : UiMsg { }
        public class WeaponCut : UiMsg {
            public Texture2D texture;
        }
    }
}
