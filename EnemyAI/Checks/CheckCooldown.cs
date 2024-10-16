using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckCooldown : Node
{
    private float _timer = 0;
    private float _totalTime;

    public CheckCooldown(float cooldown)
    {
        _totalTime = cooldown;
    }

    public override NodeState Evaluate()
    {
        if(_timer >= _totalTime)
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
