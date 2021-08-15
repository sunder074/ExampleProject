using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIdle_Monster : State
{
    float _maxIdleTime = 2f;

    public StateIdle_Monster()
    {
        StateID = (int)eState_Monster.E_IDLE;
    }

    public override void OnEnter(BaseCharacter character)
    {
        base.OnEnter(character);

        Monster monster = (Monster)character;
        monster.Agent.isStopped = true;
        monster.Animator.SetBool("bMove", false);

    }

    public override void OnUpdate(BaseCharacter character)
    {
        base.OnUpdate(character);

        if(CheckStayMaxTime(_maxIdleTime))
        {
            Monster monster = (Monster)character;

            // 공격할 타겟이 있을때
            if (monster.Check_Target())
            {

            }

            // 공격할 타겟이 없을때
            else
            {
                monster.ChangeState(new StateWalk_Monster());
            }
        }
    }

    public override void OnLeave(BaseCharacter character)
    {
        base.OnLeave(character);
    }
}
