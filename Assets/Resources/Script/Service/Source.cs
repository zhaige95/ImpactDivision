using UnityEngine;
using System.IO;


public class Source {
    public static bool FileExists(string path){
        var p = Application.dataPath + "/" + path;
        if (!File.Exists(p))
        {
            Debug.Log("file is not exist. path:" + path);
            return false;
        }
        return true;
    }
    public static GameObject InstantiatePrefab(GameObject prefab){
        return (GameObject) GameObject.Instantiate(prefab);
    }

    public static GameObject InstantiatePrefab(GameObject prefab, Vector3 position, Quaternion rotation){
        return (GameObject) GameObject.Instantiate(prefab, position, rotation);
    }
    public static GameObject InstantiatePrefab(GameObject prefab,Transform parent){
        return (GameObject) GameObject.Instantiate(prefab, parent);
    }
    public static GameObject InstantiatePrefab(string prefabName){
        string path = "resource/prefab/" + prefabName;
        return (GameObject) GameObject.Instantiate(Resources.Load(path));
    }
    public static GameObject InstantiatePrefab(string config, Vector3 position, Quaternion rotation){
        string path = "resource/prefab/" + config;
        return (GameObject) GameObject.Instantiate(Resources.Load(path), position, rotation);
    }

    public static T ReadConfig<T>(string config) where T : ScriptableObject
    {
        string path = "config/" + config;
        return (T) Resources.Load(path);
    }
    public static T ReadConfig<T>(int config) where T : ScriptableObject
    {
        string path = "config/" + config;
        return (T)Resources.Load(path);
    }

    public static ConfigWeapon ReadWeaponConfig(int weaponID)
    {
        return ReadConfig<ConfigWeapon>("Weapon/" + weaponID);
    }

}
