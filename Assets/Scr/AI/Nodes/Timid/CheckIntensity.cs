using UnityEngine;

public class CheckIntensity : Node
{
    private Npc_Timid _timidNPC; 

    public CheckIntensity(NPCBase npc) 
    { 
        _timidNPC = npc as Npc_Timid; 
    }

    public override NodeState Evaluate()
    {
        if (_timidNPC == null) return NodeState.Failure;

        var emotion = _timidNPC.GetEmotionData();
        
        // 使用子类特有的阈值参数
        if (emotion.intensity > _timidNPC.TurtleIntensityThreshold)
            return NodeState.Success;
            
        return NodeState.Failure;
    }
}