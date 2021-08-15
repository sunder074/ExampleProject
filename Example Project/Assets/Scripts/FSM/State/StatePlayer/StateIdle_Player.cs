using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIdle_Player : State
{
    public StateIdle_Player()
    {
        StateID = (int)eState_Player.E_IDLE;
    }

    public override void OnEnter(BaseCharacter character)
    {
        base.OnEnter(character);

        Player player = (Player)character;
        player.Agent.isStopped = true;
        player.Animator.SetBool("bMove", false);

    }

    public override void OnUpdate(BaseCharacter character)
    {
        base.OnUpdate(character);
    }

    public override void OnLeave(BaseCharacter character)
    {
        base.OnLeave(character);
    }
}
