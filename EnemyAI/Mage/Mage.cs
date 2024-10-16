using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class Mage : BehaviorTree.Tree
{
    public EnemyController controller;
    public GameObject bullet;
    public Transform bulletPos;

    public float tooCloseRange;
    public float fleeTime;
    public float fleeSpeed;

    [SerializeField] private Transform player;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform forwardCheck;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private AudioSource magicSound;

    protected override Node SetUpTree()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckTooClose(transform, controller, forwardCheck, tooCloseRange, fleeTime),
                new TaskFlee(transform, groundCheck, forwardCheck, rb, controller, fleeSpeed),
            }),
            new Sequence(new List<Node>
            {
                new CheckInRange(transform, player, controller),
                new TaskShoot(transform, bullet, bulletPos, controller, tooCloseRange, magicSound),
            }),
            
            new TaskPatrol(groundCheck, forwardCheck, rb, controller),
        });

        return root;
    }
}
