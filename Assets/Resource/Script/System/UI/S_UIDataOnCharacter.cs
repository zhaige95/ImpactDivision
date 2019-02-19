using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class S_UIDataOnCharacter : ComponentSystem {
	struct Group{
        public C_UIData _UIData;
        public C_Attributes _Attributes;
        public C_WeaponHandle _WeaponHandle;
        public C_Velocity _Velocity;
	}

	protected override void OnUpdate()
	{
		foreach (var e in GetEntities<Group>())
		{
            
            var weaponHandle = e._WeaponHandle;
            var velocity = e._Velocity;
            var uiData = e._UIData;
            var attribute = e._Attributes;

            uiData.hp.value = attribute.HP / attribute.HPMax;

            if (weaponHandle.weaponAttributes.ContainsKey(weaponHandle.currentWeapon))
            {

                var weaponData = weaponHandle.weaponAttributes[weaponHandle.currentWeapon];
                
                uiData.ammoMag.value = weaponData.mag.ToString();
                uiData.ammoRuntime.value = weaponData.GetRuntimeMag().ToString();

            }
            else
            {
                uiData.ammoMag.value = "-";
                uiData.ammoRuntime.value = "-";
            }


            if (velocity.Drun)
            {
                if (velocity.jumping)
                {
                    uiData.crossType.value = 1;
                }
                else
                {
                    uiData.crossType.value = 2;
                }
            }
            else
            {
                if (velocity.aiming)
                {
                    uiData.crossType.value = 3;
                }
                else
                {
                    uiData.crossType.value = 1;
                }
            }
        }
    }
	


}
