using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAttack_Monster : State
{
    public StateAttack_Monster()
    {
        StateID = (int)eState_Monster.E_ATTACK;
    }

    public override void OnEnter(BaseCharacter character)
    {
        Monster monster = (Monster)character;

        monster.Agent.isStopped = true;
        monster.Animator.SetBool("bMove", false);

        // 공격할 타겟이 있을때
        if (monster.Check_Target())
        {
            // 공격할 타겟이 공격범위 안에 있을때
            if(Vector3.Distance(monster.transform.position, monster.Target.transform.position) <= monster.CurrentPattern.AttackRange)
            {
                monster.transform.LookAt(monster.Target.transform);
                monster.Animator.SetTrigger(monster.CurrentPattern.Trigger);
                monster.CurrentPattern.OnPattern();
            }

            else
            {
                monster.ChangeState(new StateChase_Monster());
            }
        }

        else
        {
            monster.ChangeState(new StateIdle_Monster());
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
