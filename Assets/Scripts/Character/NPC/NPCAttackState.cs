using UnityEngine;

public class NPCAttackState : NPCState
{
    public override void OnStateEnter()
    {
       
        character.StartCoroutine(character.Attack(character.enemyType.enemyWeapon.GetWeaponObject(), character.targetDir, 0, character.enemyType.GetCharacterType()));
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
