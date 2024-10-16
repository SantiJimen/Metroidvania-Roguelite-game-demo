using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskFlee : Node
{
    private EnemyController _controller;    
    private Transform _transform;
    private Transform _groundCheck;
    private Transform _forwardCheck;
    private Rigidbody2D _rb;

    private float h;
    private float fleeSpeed;

    public TaskFlee(Transform transform, Transform groundCheck, Transform forwardCheck, Rigidbody2D rb, EnemyController enemy, float speed)
    {
        _transform = transform;
        _groundCheck = groundCheck;
        _forwardCheck = forwardCheck;
        _rb = rb;
        _controller = enemy;
        fleeSpeed = speed;
    }

    public override NodeState Evaluate()
    {

        if(_controller.KBCounter > 0) {
            state = NodeState.FAILURE;
            return state;
        }

        Transform player = (Transform)GetData("player");

        if(Vector2.Distance(_transform.position, player.position) > _controller.disengageRange)
        {
            parent.parent.ClearData("player");
            state = NodeState.FAILURE;
            return state;
        }

        if(_transform.position.x <= player.position.x) h = -1;
        else h = 1;
        
        if(h != _controller.h) _controller.changeH();

        if(_controller.checkForGound(_groundCheck) && !_controller.checkForGound(_forwardCheck))
        {
            _rb.velocity = new Vector2(fleeSpeed * h, _rb.velocity.y);
            state = NodeState.RUNNING;
            return state;
        }

        state = NodeState.FAILURE;
        return state;
    }
}
