using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class S_Animator : ComponentSystem {

	struct Group{
        public C_Animator _Animator;
	}

	protected override void OnUpdate()
	{
		foreach (var e in GetEntities<Group>())
		{
            var _anim = e._Animator;
            var _animator = _anim.animator;

                

            //foreach (var name in _anim.targetFloat.Keys)
            //{
            //    _animator.SetFloat(name, _anim.targetFloat[name], 0.1f, Time.deltaTime);

            //    //Debug.Log(_animator.GetFloat(name) + " | " + _anim.targetFloat[name]);
            //    if (Mathf.Abs(_animator.GetFloat(name) - _anim.targetFloat[name]) <= 0.01f) {
            //        _anim.del.Add(name);
            //    }
            //}
            
            List<string> keys = new List<string>(_anim.targetFloat.Keys);
            for (int i = 0; i < keys.Count; i++)
            {
                _animator.SetFloat(keys[i], _anim.targetFloat[keys[i]], 0.05f, Time.deltaTime);
                if (Mathf.Abs(_animator.GetFloat(keys[i]) - _anim.targetFloat[keys[i]]) <= 0.01f)
                {
                    _anim.targetFloat.Remove(keys[i]);
                }
            }

            //List<int> index = new List<int>(_anim.layerTransform.Keys);
            //for (int i = 0; i < keys.Count; i++)
            //{
            //    var weight = _animator.GetLayerWeight(index[i]);
            //    weight = Mathf.Lerp(weight, _anim.layerTransform[index[i]], 0.3f * Time.deltaTime);
            //    _animator.SetLayerWeight(index[i], weight);
            //    if (Mathf.Abs(weight - _anim.layerTransform[index[i]]) <= 0.01f)
            //    {
            //        _anim.layerTransform.Remove(index[i]);
            //    }
            //}
        }
    }
	

}
