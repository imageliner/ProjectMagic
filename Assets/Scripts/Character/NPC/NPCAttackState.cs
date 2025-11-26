using UnityEngine;

public class NPCAttackState : NPCState
{
    public override void OnStateEnter()
    {
        int randomInt = Random.Range(0, 100);
        if (randomInt >= 25)
            character.StartCoroutine(character.Attack(character.enemyType.enemyWeapon.GetWeaponObject(), character.targetDir, 0, character.enemyType.GetCharacterType()));
        if (randomInt <= 25)
            if (randomInt <= 15)
                character.StartCoroutine(character.CastTargetAbility());
            else
                character.StartCoroutine(character.CastAbility());
        
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
