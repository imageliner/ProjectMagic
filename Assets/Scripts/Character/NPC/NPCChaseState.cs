using UnityEngine;

public class NPCChaseState : NPCState
{
    private float chaseAttackTimer;

    public override void OnStateEnter()
    {
        character.StartMovement(character.Chase());

        chaseAttackTimer = Random.Range(1.0f, 4.0f);
    }

    public override void OnStateExit()
    {
        character.StopMovement();
    }

    public override void OnStateRun()
    {
        chaseAttackTimer -= Time.fixedDeltaTime;
        if (chaseAttackTimer <= 0)
        {
            character.StartCoroutine(character.CastAbility());
            chaseAttackTimer = Random.Range(1.0f, 4.0f);
        }

        if (!character.targetInRange)
        {
            character.ChangeState(new NPCWanderingState(character));
        }

        
    }

    public NPCChaseState(CharacterAI owner) : base(owner)
    {

    }
}
