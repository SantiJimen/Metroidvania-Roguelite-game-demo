using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckTooClose : Node
{
    private EnemyController _controller;
    private Transform _transform;
    private Transform _forwardCheck;

    private float tooCloseRange;
    private float timer = 0;
    private float totalTime;

    public CheckTooClose(Transform transform, EnemyController enemy, Transform forwardCheck, float range, float fleeTime)
    {
        _transform = transform;
        _forwardCheck = forwardCheck;
        _controller = enemy;
        tooCloseRange = range;
        totalTime = fleeTime;
    }

    public override NodeState Evaluate()
    {
        
        if(_controller.checkForGound(_forwardCheck))
        {
            state = NodeState.FAILURE;
            return state;
        }
        
        Transform p = (Transform)GetData("player");
        if(p == null)
        {
            state = NodeState.FAILURE;
            return state;
        }

        if(Vector2.Distance(_transform.position, p.position) <= tooCloseRange) 
        {
            timer += Time.deltaTime;
            if(timer <= totalTime)
            {
                state = NodeState.SUCCESS;
                return state;
            }

            if(timer > totalTime+2) timer = 0;
            state = NodeState.FAILURE;
            return state;
            
        }
        
        timer = 0;
        state = NodeState.FAILURE;
        return state;
    }
}
