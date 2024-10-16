using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskSpawnObj : Node
{
    private GameObject _obj;
    private Transform _objPos;

    public TaskSpawnObj(GameObject obj, Transform objPos)
    {
        _obj = obj;
        _objPos = objPos;
    }

    public override NodeState Evaluate()
    {
        GameObject newObj = MonoBehaviour.Instantiate(_obj, _objPos.position, Quaternion.identity);

        state = NodeState.SUCCESS;
        return state;
    }
}
