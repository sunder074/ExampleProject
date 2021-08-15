using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBattleIdle_Monster : State
{
    public StateBattleIdle_Monster()
    {
        StateID = (int)eState_Monster.E_BATTLE_IDLE;
    }

    public override void OnEnter(BaseCharacter character)
    {
        base.OnEnter(character);

        Monster monster = (Monster)character;

        monster.Agent.isStopped = true;

        if (monster.Target)
        {
            if (monster.Target.IsDead)
            {
                monster.Animator.ResetTrigger("tAttack1");
                monster.Animator.ResetTrigger("tAttack2");
                monster.Animator.ResetTrigger("tAttack3");
            }
        }
    }

    public override void OnUpdate(BaseCharacter character)
    {
        base.OnUpdate(character);

        Monster monster = (Monster)character;

        if (_stay_Time > 1.5f)
        {
            // 공격할 타겟이 없을때
            if (!monster.Check_Target())
            {
                monster.ChangeState(new StateIdle_Monster());
            }

            // 사용할 수 있는 패턴이 있을때
            else if (monster.GetCurrentPattern() != null)
            {
                monster.ChangeState(new StateChase_Monster());
            }
        }
    }

    public override void OnLeave(BaseCharacter character)
    {
        base.OnLeave(character);
    }
}
