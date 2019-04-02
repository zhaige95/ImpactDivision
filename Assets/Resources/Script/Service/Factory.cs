using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine.Animations;

public class Factory
{
    public static GameObject CreateAvatar(GameObject avatar, int camp, bool isLocal, Vector3 position, Quaternion rotation, ConfigWeapon main, ConfigWeapon second)
    {
        var avatarObj = GameObject.Instantiate(avatar);
        //Object.Destroy(avatarObj.GetComponent<C_UIData>(), 0);
        var _camera = avatarObj.GetComponent<C_Camera>();

        // 设置阵营
        var attributes = avatarObj.GetComponent<C_Attributes>();
        attributes.camp = camp;
        // 设置是否是本机用户
        avatarObj.GetComponent<C_Velocity>().isLocalPlayer = isLocal;
        // 本机用户的话开启相机组件，否则关闭
        avatarObj.GetComponentInChildren<Camera>().enabled = isLocal;
        // 设置射击时的射线判定需要碰撞的Layer
        _camera.shootLayerMask = 1 << 16 | 1 << 17 | 1 << 15 | 1 << 20 | 0 << (camp + 14);
        // 添加武器
        var weaponHandle = avatarObj.GetComponent<C_WeaponHandle>();
        weaponHandle.mainWeapon = main;
        weaponHandle.secondWeapon = second;

        // 如果是本机用户，进一步处理
        if (isLocal)
        {
            // 添加主UI显示的数据关联组件
            //avatarObj.AddComponent<C_UIData>();
            // 输入检测组件
            avatarObj.AddComponent<C_Input>();
            // 添加声音监听组件
            _camera.cameraObj.gameObject.AddComponent<AudioListener>();
            // 设置当前对局中的本地玩家相机
            Battle.localPlayerCameraTrans = _camera.cameraObj;
            Battle.localPlayerCamera = _camera.mainCamera;
            Battle.localPlayerCamp = camp;
        }

        Aspect.SetCamp(avatarObj, camp);

        avatarObj.transform.position = position;
        avatarObj.transform.rotation = rotation;
        avatarObj.GetComponent<GameObjectEntity>().enabled = true;

        return avatarObj;
    }


}
