using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarMgr : WindowBasic {

    public List<ConfigAvatar> AvatarConfigs = new List<ConfigAvatar>();
    public int currentAvatarID = 0;
    public Transform showNode;
    public Dictionary<int, GameObject> objPool = new Dictionary<int, GameObject>();
    [Header("List Parma")]
    public GameObject avatarListItem;
    public Transform ListNode;

    public override void Init()
    {
        // 初始化
        ConfigAvatar tconfig = null;
        int tempID = Battle.playerBattleSave.characterId;
        foreach (var config in AvatarConfigs)
        {
            // 构建角色列表
            var item = GameObject.Instantiate(avatarListItem, ListNode);
            var itemClass = item.GetComponent<AvatarListItem>();
            itemClass.Init(config);
            itemClass.action = LoadAvatar;

            // 取存档设置的角色
            if (config.id == tempID)
            {
                tconfig = config;
            }
        }
        if (tconfig != null)
        {
            this.LoadAvatar(tconfig);
        }
        
    }


    public void LoadAvatar(ConfigAvatar config)
    {
        
        if (objPool.ContainsKey(config.id))
        {
            if (currentAvatarID != 0)
            {
                objPool[currentAvatarID].SetActive(false);
            }
            objPool[config.id].SetActive(true);
        }
        else
        {
            var model = GameObject.Instantiate(config.IslandModel, showNode);
            objPool.Add(config.id, model);
            if (currentAvatarID != 0)
            {
                objPool[currentAvatarID].SetActive(false);
            }
            
        }
        currentAvatarID = config.id;

        // save change
        Battle.playerBattleSave.characterId = config.id;
        Battle.SavePlayerBattleData();
    }
 
}
