using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckBombThrow : Node
{
    private Transform _transform;
    private Transform _player;
    private float _timer;
    private float _totalTime;
    private float _range;

    public CheckBombThrow(Transform transform, Transform player, float timer, float range)
    {
        _transform = transform;
        _player = player;
        _totalTime = timer;
        _range = range;
    }

    public override NodeState Evaluate()
    {
        Transform p = (Transform)GetData("player");
        if(p == null)
        {
            parent.parent.SetData("player", _player);
            _timer = 0;
        }
        
        if(_timer >= _totalTime && (Vector2.Distance(_transform.position, p.position) <= _range))
        {
            _timer = 0;
            state = NodeState.SUCCESS;
            return state;
        }
        _timer += Time.deltaTime;
        state = NodeState.FAILURE;
        return state;
    }
}
