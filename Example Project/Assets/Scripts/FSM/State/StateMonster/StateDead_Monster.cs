using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDead_Monster : State
{
    public StateDead_Monster()
    {
        StateID = (int)eState_Monster.E_DEAD;
    }

    public override void OnEnter(BaseCharacter character)
    {
        Monster monster = (Monster)character;

        monster.Agent.isStopped = true;
        monster.Animator.SetTrigger("tDie");
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
