using Unity.Entities;
using System;
using UnityEngine;
using UiEvent;

public class S_AttackListener : ComponentSystem {
	struct Group{
		public C_AttackListener _AttackListener;
        public C_Velocity _Velocity;
        public C_Attributes _Attributes;
        public C_BattleMgr _BattleMgr;
        public CS_StateMgr _StateMgr;
        public AudioSource _Audio;
    }
    
	protected override void OnUpdate()
	{
		foreach (var e in GetEntities<Group>())
		{
            var _attackListener = e._AttackListener;
            var _attackList = e._AttackListener.attackList;
            var _attribute = e._Attributes;

            if (e._AttackListener.isActive)
            {
                foreach (Attack attack in _attackList)
                {
                    _attackListener.photonView.RPC("Demaged", PhotonTargets.All, attack.demage);

                    var source = attack.source;
                    var sourceAudio = source.GetComponent<AudioSource>();
                    var sourceBattleMgr = source.GetComponent<C_BattleMgr>();

                    sourceBattleMgr.AddDemage(attack.demage, attack.headShot);
                    sourceBattleMgr.AddHit();

                    var hitMsg = new UiEvent.UiMsgs.Hit()
                    {
                        HeadShot = attack.headShot
                    };
                    source.GetComponent<C_UiEventMgr>().SendEvent(hitMsg);
                    Sound.PlayOneShot(sourceAudio, _attackListener.hitFeedBackSounds);

                    _attackListener.photonView.RPC("AddAttackSource", PhotonTargets.Others, attack.source.roomID);
                    
                }
                if (_attribute.HP <= 0)
                {
                    if (e._Velocity.isLocalPlayer)
                    {
                        var battleMgr = e._BattleMgr;
                        e._BattleMgr.AddKillerMsg(_attackListener.lastHitPlayer.nickName);
                        _attackListener.lastHitPlayer.photonView.RPC("AddKill", PhotonTargets.All);

                        foreach (var item in _attackListener.sourceList.Values)
                        {
                            if (item.roomID != _attackListener.lastHitPlayer.roomID)
                            {
                                item.photonView.RPC("AddAssists", PhotonTargets.Others);
                            }
                        }

                        e._StateMgr.EnterState("dead");
                        e._BattleMgr.AddDead();
                    }

                    _attackListener.isActive = false;

                    break;
                }
                _attackListener.attackList.Clear();
            }
        }
    }
}
