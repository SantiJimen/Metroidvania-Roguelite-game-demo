using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckMinions : Node
{
    private float _maxMinions;
    public float numMinions = 0;
    private GameObject [] minions;

    public CheckMinions(float maxMinions)
    {
        _maxMinions = maxMinions;
        
    }

    public override NodeState Evaluate()
    {

        minions = GameObject.FindGameObjectsWithTag("Enemy");

        foreach(GameObject minion in minions)
        {
            numMinions++;
        }

        if(numMinions <= _maxMinions)
        {
            numMinions = 0;
            state = NodeState.SUCCESS;
            return state;
        }
        numMinions = 0;

        state = NodeState.FAILURE;
        return state;
    }
}
