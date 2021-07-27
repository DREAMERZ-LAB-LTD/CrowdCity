using UnityEngine;

public abstract class NpcState : ScriptableObject
{
    public float stoppingDistance, speed;

    public virtual void OnStateStart(Npc npc)
    {
        npc.Agent.stoppingDistance = stoppingDistance;
        npc.Agent.speed = speed;
       
    }

    public abstract void Execute(Npc npc);
    public abstract void OnStateEnd(Npc npc);

    public virtual void OnNewLeader(Npc npc) { }
}