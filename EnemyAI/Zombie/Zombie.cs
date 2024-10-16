using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class Zombie : BehaviorTree.Tree
{

    public EnemyController controller;

    private Transform player;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform forwardCheck;
    [SerializeField] private Rigidbody2D rb;

    protected override Node SetUpTree()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckInRange(transform, player, controller),
                new TaskChase(transform, rb, controller, 0),
            }),
            new TaskPatrol(groundCheck, forwardCheck, rb, controller),
        });
        return root;
    }

}
