using UnityEngine;

public class NPCChaseState : NPCState
{
    public override void OnStateEnter()
    {
        character.StartMovement(character.Chase());
    }

    public override void OnStateExit()
    {
        character.StopMovement();
    }

    public override void OnStateRun()
    {
        if (!character.targetInRange)
        {
            character.ChangeState(new NPCWanderingState(character));
        }
    }

    public NPCChaseState(CharacterAI owner) : base(owner)
    {

    }
}
