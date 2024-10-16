using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class Necromancer : BehaviorTree.Tree
{
    public EnemyController controller;
    public LowerWalls lowerWalls;
    [SerializeField] private GameObject zombie;
    public Transform zombiePos;
    public GameObject bomb;
    public Transform bombPos;
    public float maxMinions;
    public float zombieCooldown;
    public float bombRate;
    public float enragedBombRate;

    public float secondPhase;

    [SerializeField] private Transform player;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform forwardCheck;
    [SerializeField] private Rigidbody2D rb;

    protected override Node SetUpTree()
    {
        
        Node root = new Selector(new List<Node>
        {
            
            new Sequence(new List<Node>
            {
                new CheckEnraged(controller, secondPhase, true),
                new Selector(new List<Node>
                {
                    new Sequence(new List<Node>
                    {
                        new CheckMinions(maxMinions),
                        new Sequence(new List<Node>
                        {
                            new CheckCooldown(zombieCooldown),
                            new TaskSpawnObj(zombie, zombiePos),
                        }),
                    }),
                    new Sequence(new List<Node>
                    {
                        new CheckBombThrow(transform, player, enragedBombRate, controller.agroRange),
                        new TaskSpawnObj(bomb, bombPos),
                    }),
                }),
            }),

            new Sequence(new List<Node>
            {
                new CheckEnraged(controller, secondPhase, false),
                new Sequence(new List<Node>
                {
                    new CheckBombThrow(transform, player, bombRate, controller.agroRange),
                    new TaskSpawnObj(bomb, bombPos),
                })
            }),

            new TaskPatrol(groundCheck, forwardCheck, rb, controller),
        });
        return root;
        
    }
}
