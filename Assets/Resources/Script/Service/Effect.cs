using UnityEngine;
using Unity.Entities;
using System.Collections.Generic;

public class Effect
{
    public static Dictionary<string, List<GameObject>> _effectPool = new Dictionary<string, List<GameObject>>();

    public static bool AddBullet(GameObject obj, Attack attack, Vector3 start, Vector3 target, int camp, bool visible = true, bool isLocal = true)
    {
        var bulletData = obj.GetComponent<C_Bullet>();
        List<GameObject> pool = GetPool(bulletData.effectTag);

        // 查询缓存池中是否有未激活的子弹
        foreach (GameObject item in pool)
        {
            if (!item.GetComponent<C_Bullet>().isActive)
            {
                item.transform.position = start;
                item.transform.LookAt(target);
                ActivateBullet(item, attack, camp, target, visible, isLocal);
                return true;
            }
        }

        // 缓存池中没有可用的闲置子弹，额外生成
        // 实例化
        var bullet = GameObject.Instantiate(obj, start, Quaternion.identity);
        // 放入对象池
        pool.Add(bullet);

        // 激活子弹
        ActivateBullet(bullet, attack, camp, target, visible, isLocal);
        // 激活Entity组件
        //bullet.GetComponent<GameObjectEntity>().enabled = true;
        return true;
    }
    

    public static void ActivateBullet(GameObject bullet, Attack attack, int camp, Vector3 target, bool visible = true, bool isLocal = true)
    {
        // 将子弹指向目标
        bullet.transform.LookAt(target);
        
        var bulletData = bullet.GetComponent<C_Bullet>();
        // 设置射线检测的layer mask
        switch (camp)
        {
            case 1: bulletData.layerMask = 1 << 12 | 1 << 16 | 1 << 17 | 1 << 20; break;
            case 2: bulletData.layerMask = 1 << 12 | 1 << 15 | 1 << 17 | 1 << 20; break;
            case 3: bulletData.layerMask = 1 << 12 | 1 << 16 | 1 << 15 | 1 << 20; break;
        }
        // 设置子弹的可视属性
        bulletData.SetVisible(visible);
        // 使用传入Attack信息的方法直接设置伤害信息并激活子弹
        bulletData.SetActive(attack, isLocal);
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

    public static void Clear()
    {
        foreach (List<GameObject> list in _effectPool.Values)
        {
            foreach (var item in list)
            {
                Object.Destroy(item);
            }
        }
        _effectPool.Clear();
    }


}
