using UnityEngine;

public class NPCIdleState : NPCState
{
    private float timer = 0;

    public override void OnStateEnter()
    {
        timer = Random.Range(2f, 8f);
        character.transform.rotation = character.transform.rotation;
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateRun()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            character.ChangeState(new NPCWanderingState(character));
        }
    }

    public NPCIdleState(CharacterAI owner) : base(owner)
    {

    }
}
