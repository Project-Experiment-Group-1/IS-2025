using UnityEngine;

public class CheckPlayerDistance : Node
{
    private NPCBase _npc;

    public CheckPlayerDistance(NPCBase npc) 
    { 
        _npc = npc; 
    }

    public override NodeState Evaluate()
    {
        // 调用 NPCBase 里的新方法 (平台游戏通常只关心水平距离)
        float dist = _npc.GetHorizontalDistanceToPlayer();
        
        if (dist < _npc.DetectionRadius)
            return NodeState.Success; // 玩家在范围内
            
        return NodeState.Failure; // 玩家太远
    }
}