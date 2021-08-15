using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDamaged_Player : State
{
    public StateDamaged_Player()
    {
        StateID = (int)eState_Player.E_DAMAGED;
    }

    public override void OnEnter(BaseCharacter character)
    {
        base.OnEnter(character);

        Player player = (Player)character;

        player.Agent.isStopped = true;
        player.Animator.SetBool("bMove", false);
        player.Animator.SetTrigger("tDamaged");

        if (player.Check_Target())
        {
            player.ChangeState(new StateBattleIdle_Player());
        }

        else
        {
            player.ChangeState(new StateIdle_Player());
        }
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
