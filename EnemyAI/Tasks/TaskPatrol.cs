using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskPatrol : Node
{

    public EnemyController _controller;
    private Transform _groundCheck;
    private Transform _forwardCheck;
    private Rigidbody2D _rb;

    private float _turnDelay = 1;

    public TaskPatrol(Transform groundCheck, Transform forwardCheck, Rigidbody2D rb, EnemyController controller)
    {
        _groundCheck = groundCheck;
        _forwardCheck = forwardCheck;
        _rb = rb;
        _controller = controller;
    }

    public override NodeState Evaluate()
    {
        if(_controller.checkForGound(_groundCheck) && !_controller.checkForGound(_forwardCheck))
        {
            _rb.velocity = new Vector2(_controller.speed * _controller.h, _rb.velocity.y);
        }
        else
        {
            if(_turnDelay <= 0)
            {
                _controller.changeH();
                _rb.velocity = new Vector2(_controller.speed * _controller.h, _rb.velocity.y);
                _turnDelay = _controller.turnDelayTime;
            }
            _turnDelay -= Time.deltaTime;
        }

        state = NodeState.SUCCESS;
        return state;
    }
}
