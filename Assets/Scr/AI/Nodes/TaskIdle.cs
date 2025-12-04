using UnityEngine;

public class TaskIdle : Node
{
    private NPCBase _npc;

    public TaskIdle(NPCBase npc) 
    { 
        _npc = npc; 
    }

    public override NodeState Evaluate()
    {
        _npc.StopMoving();
        return NodeState.Running; 
    }
}