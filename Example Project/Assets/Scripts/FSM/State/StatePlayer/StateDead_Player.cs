using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDead_Player : State
{
    public StateDead_Player()
    {
        StateID = (int)eState_Player.E_DEAD;
    }

    public override void OnEnter(BaseCharacter character)
    {
        Player player = (Player)character;

        player.Agent.isStopped = true;
        player.Animator.SetTrigger("tDie");
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
