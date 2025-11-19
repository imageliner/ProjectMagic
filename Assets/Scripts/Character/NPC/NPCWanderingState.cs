using UnityEngine;

public class NPCWanderingState : NPCState
{
    public override void OnStateEnter()
    {
        character.StartMovement(character.Wander());
    }

    public override void OnStateExit()
    {
        character.StopMovement();
    }

    public override void OnStateRun()
    {
        if (!character.isMoving)
        {
            character.ChangeState(new NPCIdleState(character));
        }
    }

    public NPCWanderingState(CharacterAI owner) : base(owner)
    {

    }
}
