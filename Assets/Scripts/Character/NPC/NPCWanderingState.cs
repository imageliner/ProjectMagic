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
        RaycastHit hit;
        if (Physics.Raycast(character.transform.position, character.transform.forward, out hit, 2f))
        {
            character.StartMovement(character.Wander());
        }

        if (!character.isMoving)
        {
            character.ChangeState(new NPCIdleState(character));
        }
    }

    public NPCWanderingState(CharacterAI owner) : base(owner)
    {

    }
}
