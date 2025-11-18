using UnityEngine;

public class NPCAttackState : NPCState
{
    public override void OnStateEnter()
    {
        character.StartMovement(character.Attack());
    }

    public override void OnStateExit()
    {
        character.StopMovement();
    }

    public override void OnStateRun()
    {

    }

    public NPCAttackState(CharacterAI owner) : base(owner)
    {

    }
}
