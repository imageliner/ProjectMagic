using UnityEngine;

public class NPCAttackState : NPCState
{
    public override void OnStateEnter()
    {
        if (character.enemyType.useAbilities)
        {
            int randomInt = Random.Range(0, 100);
            if (randomInt >= 20)
                character.StartCoroutine(character.Attack(character.enemyType.enemyWeapon.GetGearObject(), character.targetDir, 0, character.enemyType.GetCharacterType()));
            if (randomInt <= 20)
                if (randomInt <= 10)
                    character.StartCoroutine(character.CastAbility());
                else
                    character.StartCoroutine(character.CastTargetAbility());
        }
        else character.StartCoroutine(character.Attack(character.enemyType.enemyWeapon.GetGearObject(), character.targetDir, 0, character.enemyType.GetCharacterType()));


    }

    public override void OnStateExit()
    {
        
    }

    public override void OnStateRun()
    {

    }

    public NPCAttackState(CharacterAI owner) : base(owner)
    {

    }
}
