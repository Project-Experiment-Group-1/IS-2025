using System.Collections.Generic;
using UnityEngine;

public enum NodeState { Running, Success, Failure }

public abstract class Node
{
    protected NodeState state;
    public abstract NodeState Evaluate();
}

// Selector :優先度を判定する OR Gateに似ているの感じ，子ノードTであれば，Tになる
public class Selector : Node
{
    private List<Node> nodes = new List<Node>();
    public Selector(List<Node> nodes) { this.nodes = nodes; }

    public override NodeState Evaluate()
    {
        foreach (var node in nodes)
        {
            switch (node.Evaluate())
            {
                case NodeState.Running:
                    state = NodeState.Running;
                    return state;
                case NodeState.Success:
                    state = NodeState.Success;
                    return state;
                case NodeState.Failure:
                    continue; // すべて失敗すれば，次のノードに
            }
        }
        state = NodeState.Failure;
        return state;
    }
}

// Sequence:条件ー＞行動  AND Gate,すべて子ノードTであれば，Tになる
public class Sequence : Node
{
    private List<Node> nodes = new List<Node>();
    public Sequence(List<Node> nodes) { this.nodes = nodes; }

    public override NodeState Evaluate()
    {
        bool isAnyChildRunning = false;
        foreach (var node in nodes)
        {
            switch (node.Evaluate())
            {
                case NodeState.Failure:
                    state = NodeState.Failure;
                    return state; 
                case NodeState.Success:
                    continue;
                case NodeState.Running:
                    isAnyChildRunning = true;
                    continue;
            }
        }
        state = isAnyChildRunning ? NodeState.Running : NodeState.Success;
        return state;
    }
}