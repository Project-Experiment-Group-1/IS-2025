using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Npc_Sly_ally : NPCBase
{
    [Header("Sly Settings")]
    public float TurtleIntensityThreshold = 0.6f;
    public float FleeArousalThreshold = 0.8f;

    private Node _topNode;

    protected override void Start()
    {
        base.Start(); 
        ConstructBehaviorTree();
    }

    protected override void Update()
    {
        
        base.Update(); 

        // Run BehaviorTree
        if (_topNode != null) 
            _topNode.Evaluate(); 
    }

    private void ConstructBehaviorTree()
    {
        // 1. Defend Mode 
        Sequence turtleSequence = new Sequence(new List<Node> {
            new CheckIntensity(this), 
            new TaskTurtle(this)
        });

        // 2. flee mode
        Sequence followSequence = new Sequence(new List<Node> {
            new CheckPlayerDistance(this),
            new TaskMoveToPlayer(this)
        });

        // 3. idlemode
        TaskIdle idleNode = new TaskIdle(this);

        // root node
        _topNode = new Selector(new List<Node> {
            turtleSequence,
            followSequence,
            idleNode
        });
    }
}