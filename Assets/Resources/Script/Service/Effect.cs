using UnityEngine;
using Unity.Entities;
using System.Collections.Generic;

public class Effect
{
    public static Dictionary<string, List<GameObject>> _effectPool = new Dictionary<string, List<GameObject>>();

    public static bool AddBullet(GameObject obj, Attack attack, Vector3 start, Vector3 target, int camp, bool visible = true)
    {
        var bulletData = obj.GetComponent<C_Bullet>();
        List<GameObject> pool = GetPool(bulletData.effectTag);

        foreach (GameObject item in pool)
        {
            if (!item.GetComponent<C_Bullet>().isActive)
            {
                item.transform.position = start;
                item.transform.LookAt(target);
                ActiveBullet(item, attack, visible);

                return true;
            }
        }

        // 实例化
        var bullet = GameObject.Instantiate(obj, start, Quaternion.Euler(Vector3.zero));
        // 放入对象池
        pool.Add(bullet);
        
        bullet.transform.LookAt(target);
        bullet.layer = 14 + camp;
        bulletData = bullet.GetComponent<C_Bullet>();

        bulletData.visible = true;

        // 设置attack
        bulletData.attack = attack;
        // 设置layer mask
        switch (camp)
        {
            case 1: bulletData.layerMask = 1 << 16 | 1 << 17 | 1 << 20; break;
            case 2: bulletData.layerMask = 1 << 15 | 1 << 17 | 1 << 20; break;
            case 3: bulletData.layerMask = 1 << 16 | 1 << 15 | 1 << 20; break;
        }
        // 激活子弹
        ActiveBullet(bulletData, attack, visible);
        // 激活Entity组件
        //bullet.GetComponent<GameObjectEntity>().enabled = true;
        return true;
    }

    public static void ActiveBullet(GameObject obj, Attack attack, bool visible)
    {
        var bulletData = obj.GetComponent<C_Bullet>();
        ActiveBullet(bulletData, attack, visible);
    }
    public static void ActiveBullet(C_Bullet bullet, Attack attack, bool visible)
    {
        bullet.SetActive(attack);
        bullet.SetVisible(visible);
    }

    public static bool AddEffect(GameObject obj, Vector3 position, Quaternion rotation)
    {
        var effectData = obj.GetComponent<C_Effect>();
        List<GameObject> pool = GetPool(effectData.effectTag);
        foreach (GameObject item in pool)
        {
            if (effectData.useModel.Equals("one"))
            {
                item.transform.GetChild(0).gameObject.SetActive(false);
                item.transform.GetChild(0).gameObject.SetActive(true);
                ActiveEffect(item, position, rotation);
                return true;
            }
            var itemData = item.GetComponent<C_Effect>();
            if (!itemData.isActive)
            {
                item.transform.GetChild(0).gameObject.SetActive(false);
                item.transform.GetChild(0).gameObject.SetActive(true);
                ActiveEffect(item, position, rotation);
                return true;
            }
        }

        var instantiateObj = GameObject.Instantiate(obj);
        ActiveEffect(instantiateObj, position, rotation);
        pool.Add(instantiateObj);
        return true;
    }

    public static bool AddEffect(GameObject obj, RaycastHit hit)
    {
        var effectData = obj.GetComponent<C_Effect>();
        List<GameObject> pool = GetPool(effectData.effectTag);
        foreach (GameObject item in pool)
        {
            if (effectData.useModel.Equals("one"))
            {
                item.transform.GetChild(0).gameObject.SetActive(false);
                item.transform.GetChild(0).gameObject.SetActive(true);
                ActiveEffect(item, hit);
                return true;
            }
            var itemData = item.GetComponent<C_Effect>();
            if (!itemData.isActive)
            {
                item.transform.GetChild(0).gameObject.SetActive(false);
                item.transform.GetChild(0).gameObject.SetActive(true);
                ActiveEffect(item, hit);
                return true;
            }
        }

        var instantiateObj = GameObject.Instantiate(obj);
        pool.Add(instantiateObj);
        ActiveEffect(instantiateObj, hit);
        obj.GetComponent<GameObjectEntity>().enabled = true;
        return true;
    }

    public static void ActiveEffect(GameObject obj, Vector3 position, Quaternion rotation)
    {
        var _data = obj.GetComponent<C_Effect>();
        obj.transform.SetPositionAndRotation(position, rotation);
        _data.timer.Enter(_data.time);
        _data.isActive = true;
    }
    public static void ActiveEffect(GameObject obj, RaycastHit hit)
    {
        var _data = obj.GetComponent<C_Effect>();
        obj.transform.position = hit.point;
        obj.transform.rotation = Quaternion.LookRotation(hit.normal);
        _data.timer.Enter(_data.time);
        _data.isActive = true;
    }
    // 取得指定tag的对象池，如果没有则新建一个并返回新对象池
    public static List<GameObject> GetPool(string tag)
    {
        List<GameObject> pool;
        // 以子弹的tag属性值作为对象池的key；
        if (!_effectPool.TryGetValue(tag, out pool))
        {
            // 对象池不存在，新建一个对象池
            _effectPool.Add(tag, new List<GameObject>());
            _effectPool.TryGetValue(tag, out pool);
        }

        return pool;

    }

}
