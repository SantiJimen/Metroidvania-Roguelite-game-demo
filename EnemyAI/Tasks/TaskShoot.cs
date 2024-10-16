using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskShoot : Node
{

    private EnemyController _controller;
    private Transform _transform;
    private GameObject _bullet;
    private Transform _bulletPos;
    private AudioSource _magicSound;

    private float timer = 0;
    private float tooClose;

    public TaskShoot(Transform transform, GameObject bullet, Transform bulletPos, EnemyController enemy, float range, AudioSource magicSound) 
    {
        _transform = transform;
        _bullet = bullet;
        _bulletPos = bulletPos;
        _controller = enemy;
        tooClose = range;
        _magicSound = magicSound;
    }
    
    public override NodeState Evaluate()
    {

        Transform player = (Transform)GetData("player");

        timer += 0.01f;

        if(_transform.position.x > player.position.x) _controller.FlipLeft();
        else _controller.FlipRight();

        if(state == NodeState.RUNNING) _controller.SetBoolAnim("ShootReady", false);
        _controller.SetBoolAnim("ShootReady", true);
        
        if(Vector2.Distance(_transform.position, player.position) > _controller.disengageRange || _controller.getKBCounter() > 0)
        {
            if(_controller.getKBCounter() > 0) _controller.StartAnimation("Hurt");
            timer = 0;
            parent.parent.ClearData("player");
            state = NodeState.FAILURE;
            return state;
        }
        
        if(timer > 1.6f) 
        {
            _controller.StartAnimation("Shoot");
            _controller.SetBoolAnim("ShootReady", false);
            _magicSound.Play();
            MonoBehaviour.Instantiate(_bullet, _bulletPos.position, _bulletPos.rotation);
            timer = 0;
            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.RUNNING;
        return state;
    }
}
