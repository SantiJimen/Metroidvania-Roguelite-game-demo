using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskChase : Node
{

    private EnemyController _controller;    
    private Transform _transform;
    private Rigidbody2D _rb;

    private float _turnDelay;
    private float _delayTotal = 0.4f;
    private float h;

    public TaskChase(Transform transform, Rigidbody2D rb, EnemyController controller, float turnDelay)
    {
        _transform = transform;
        _rb = rb;
        _controller = controller;
        _turnDelay = turnDelay;
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
        else if(_controller.Health() < 0)
        {
            _rb.velocity = new Vector2(0,_rb.velocity.y);
            state = NodeState.FAILURE;
            return state;
        }

        if(_transform.position.x <= player.position.x) h = 1;
        else h = -1;

        if(h != _controller.h)
        {
            if(_turnDelay <= 0)
            {
                _turnDelay = _delayTotal;
                _controller.changeH();
            }
            _turnDelay -= 0.01f;
        }
        
        _rb.velocity = new Vector2(_controller.speedChase * _controller.h, _rb.velocity.y);
        
        state = NodeState.RUNNING;
        return state;
    }
}
