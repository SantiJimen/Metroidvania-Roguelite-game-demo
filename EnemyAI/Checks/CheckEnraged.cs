using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckEnraged : Node
{
    private EnemyController _controller;
    private float _enrageThreshold;
    private bool _enraged;

    public CheckEnraged(EnemyController controller, float et, bool enraged)
    {
        _controller = controller;
        _enrageThreshold = et;
        _enraged = enraged;
    }

    public override NodeState Evaluate()
    {
        if((_enraged && _controller.Health() <= _enrageThreshold) || (!_enraged && _controller.Health() > _enrageThreshold))
        {
            state = NodeState.SUCCESS;
            return state;
        }
        state = NodeState.FAILURE;
        return state;
    }
}
