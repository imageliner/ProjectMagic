using UnityEngine;


public abstract class NPCState
{
    protected CharacterAI character;


    public abstract void OnStateEnter();

    public abstract void OnStateExit();

    public abstract void OnStateRun();

    public NPCState(CharacterAI owner)
    {
        character = owner;
    }
}
