using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBattleIdle_Player : State
{
    public StateBattleIdle_Player()
    {
        StateID = (int)eState_Player.E_BATTLE_IDLE;
    }

    public override void OnEnter(BaseCharacter character)
    {
        base.OnEnter(character);

        Player player = (Player)character;

        player.Agent.isStopped = true;

        if (player.Target)
        {
            if(player.Target.IsDead)
            {
                player.Animator.ResetTrigger("tAttack1");
            }
        }
    }

    public override void OnUpdate(BaseCharacter character)
    {
        base.OnUpdate(character);

        Player player = (Player)character;

        if (_stay_Time > 1f)
        {
            // 공격할 타겟이 없을때
            if (!player.Check_Target())
            {
                player.ChangeState(new StateIdle_Player());
            }

            else
            {
                player.ChangeState(new StateChase_Player());
            }
        }
    }

    public override void OnLeave(BaseCharacter character)
    {
        base.OnLeave(character);
    }
}
