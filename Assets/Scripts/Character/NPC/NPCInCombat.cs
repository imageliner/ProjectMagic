using UnityEngine;

public class NPCInCombat : NPCState
{
    private float attackTimer = 0;

    public override void OnStateEnter()
    {
        if (character.canStrafe)
        {
            character.StartMovement(character.Strafe());
        }

        attackTimer = Random.Range(0.5f, 3f);
    }

    public override void OnStateExit()
    {
        character.StopMovement();
    }

    public override void OnStateRun()
    {
        character.WatchTarget();

        if (character.isAggressive)
        {
            if (character.distanceToTarget >= character.attackRange / 2)
            {
                character.transform.position += character.targetDir * Time.deltaTime * (character.moveSpeed / 2);

                if (character.targetDir.sqrMagnitude > 0.001f)
                {
                    Quaternion targetRot = Quaternion.LookRotation(character.targetDir);
                    targetRot.x = 0;
                    targetRot.z = 0;
                    character.transform.rotation = Quaternion.Slerp(character.transform.rotation, targetRot, character.rotationSpeed * Time.deltaTime);
                }
            }
        }
        else if (!character.isAggressive)
        {
            if (character.distanceToTarget <= character.attackRange / 1.2)
            {
                character.transform.position -= character.targetDir * Time.deltaTime * (character.moveSpeed * 2);

                if (character.targetDir.sqrMagnitude > 0.001f)
                {
                    Quaternion targetRot = Quaternion.LookRotation(character.targetDir);
                    targetRot.x = 0;
                    targetRot.z = 0;
                    character.transform.rotation = Quaternion.Slerp(character.transform.rotation, targetRot, character.rotationSpeed * Time.deltaTime);
                }
            }
        }


            attackTimer -= Time.deltaTime;
        if (attackTimer <= 0)
        {
            character.ChangeState(new NPCAttackState(character));
        }

        if (!character.targetInRange)
        {
            character.ChangeState(new NPCIdleState(character));
            return;
        }
    }

    public NPCInCombat(CharacterAI owner) : base(owner)
    {

    }
}
