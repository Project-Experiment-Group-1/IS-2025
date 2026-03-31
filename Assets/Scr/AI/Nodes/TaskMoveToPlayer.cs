using UnityEngine;

public class TaskMoveToPlayer : Node
{
    private NPCBase _npc;
    private float _stopDistance = 1.0f; 

    public TaskMoveToPlayer(NPCBase npc) 
    { 
        _npc = npc; 
    }

    public override NodeState Evaluate()
    {
        if (_npc.PlayerTransform == null) return NodeState.Failure;

        float dist = _npc.GetHorizontalDistanceToPlayer();

        
        if (dist < _stopDistance)
        {
            _npc.StopMoving();
            return NodeState.Success;
        }

        _npc.MoveTo(_npc.PlayerTransform.position.x);
        

        float heightDiff = _npc.PlayerTransform.position.y - _npc.transform.position.y;
        if (heightDiff > 1.5f && _npc.IsGrounded)
        {
            _npc.Jump();
        }

        return NodeState.Running; 
    }
}