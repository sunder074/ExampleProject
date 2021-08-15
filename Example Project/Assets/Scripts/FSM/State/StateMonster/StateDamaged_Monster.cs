using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDamaged_Monster : State
{
    public StateDamaged_Monster()
    {
        StateID = (int)eState_Monster.E_DAMAGED;
    }

    public override void OnEnter(BaseCharacter character)
    {
        base.OnEnter(character);

        Monster monster = (Monster)character;

        monster.Agent.isStopped = true;
        monster.Animator.SetBool("bMove", false);
        monster.Animator.SetTrigger("tDamaged");

        if (!monster.Check_Target())
        {
            monster.ChangeState(new StateIdle_Monster());
        }

        else
        {
            monster.ChangeState(new StateBattleIdle_Monster());
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
