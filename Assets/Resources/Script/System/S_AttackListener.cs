using Unity.Entities;
using System;
using UnityEngine;
using Random = UnityEngine.Random;
using UiEvent;

public class S_AttackListener : ComponentSystem {
	struct Group{
		public C_AttackListener _AttackListener;
        public C_Velocity _Velocity;
        public C_Attributes _Attributes;
        public C_Animator _Animator;
        public AudioSource _Audio;
        public C_UiEventMgr _uiMgr;
    }

	protected override void OnUpdate()
	{
		foreach (var e in GetEntities<Group>())
		{
            var _attackListener = e._AttackListener;
            var _attackList = e._AttackListener.attackList;
            var _attribute = e._Attributes;
            var _velocity = e._Velocity;
            if (e._AttackListener.isActive)
            {
                foreach (Attack attack in _attackList)
                {
                    Attribute.Add(ref _attribute, "HP", attack.demage, "-");

                    var uiMsg = new UiEvent.UiMsgs.Hp()
                    {
                        hp = _attribute.HP,
                        hpMax = _attribute.HPMax
                    };
                    e._uiMgr.SendEvent(uiMsg);
                    e._Animator.animator.SetTrigger("hit");

                    if (_velocity.isLocalPlayer)
                    {
                        Sound.PlayOneShot(e._Audio, _attackListener.beAttackedSounds);
                    }
                    else
                    {
                        var source = attack.source;
                        var sourceAudio = source.GetComponent<AudioSource>();
                        var sourceIsLocalPlayer = source.GetComponent<C_Velocity>().isLocalPlayer;

                        if (sourceIsLocalPlayer)
                        {
                            Sound.PlayOneShot(sourceAudio, _attackListener.hitFeedBackSounds);

                            if (_attribute.HP <= 0)
                            {
                                source.GetComponent<AudioSource>().PlayOneShot(_attackListener.killSound);
                                Sound.PlayOneShot(source.GetComponent<AudioSource>(), _attackListener.killSound);
                            }
                        }
                    }
                    
                }
                _attackList.Clear();
            }
        }
    }
}
