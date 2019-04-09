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

        public class Hit : UiMsg {
            public bool HeadShot = false;
        }

        public class WeaponCut : UiMsg {
            public Texture2D texture;
        }

        public class Spread : UiMsg
        {
            public float value;
        }

        public class Kill : UiMsg { }

        public class Assists : UiMsg { }

    }
}
