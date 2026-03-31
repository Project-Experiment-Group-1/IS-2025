using UnityEngine;

public class TaskTurtle : Node
{
    private NPCBase _npc;

    public TaskTurtle(NPCBase npc) 
    { 
        _npc = npc; 
    }

    public override NodeState Evaluate()
    {
        _npc.StopMoving();
        //set animation
        //_npc.SetAnimationBool("IsTurtle", true);
        
        return NodeState.Running;
    }
}