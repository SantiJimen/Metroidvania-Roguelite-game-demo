using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckInRange : Node
{
    private EnemyController _controller;
    private Transform _player;
    private Transform _transform;

    public CheckInRange(Transform transform, Transform player, EnemyController enemy)
    {
        _transform = transform;
        _player = player;
        _controller = enemy;
    }

    public override NodeState Evaluate()
    {
        Transform p = (Transform)GetData("player");
        if(p == null)
        {

            if(Vector2.Distance(_transform.position, _player.position) <= _controller.agroRange)
            {
                parent.parent.SetData("player", _player);

                state = NodeState.SUCCESS;
                return state;
            }

            state = NodeState.FAILURE;
            return state;
        }
        state = NodeState.SUCCESS;
        return state;
    }

}
