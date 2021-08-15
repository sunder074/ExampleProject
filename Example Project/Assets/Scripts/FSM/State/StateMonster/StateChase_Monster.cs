using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateChase_Monster : State
{
    BaseCharacter _target;

    public StateChase_Monster()
    {
        StateID = (int)eState_Monster.E_CHASE;
    }

    public override void OnEnter(BaseCharacter character)
    {
        base.OnEnter(character);

        Monster monster = (Monster)character;

        // 몬스터가 리셋되는 범위를 벗어났을때
        if (monster.Check_ResetRange())
        {
            monster = (Monster)character;
            monster.ChangeState(new StateResetPosition_Monster());
        }

        // 쫒아야할 타겟이 있을때
        else if (monster.Check_Target())
        {
            _target = monster.Target;

            // 공격 범위안에 있을때
            if (Vector3.Distance(_target.transform.position, character.transform.position) <= monster.CurrentPattern.AttackRange)
            {
                monster.ChangeState(new StateAttack_Monster());
                return;
            }

            monster.Agent.isStopped = false;
            monster.Agent.SetDestination(_target.transform.position);
            monster.Animator.SetBool("bMove", true);
            monster.Animator.ResetTrigger(monster.CurrentPattern.Trigger);
        }

        else
        {
            monster.ChangeState(new StateIdle_Monster());
        }
    }

    public override void OnUpdate(BaseCharacter character)
    {
        Monster monster = (Monster)character;

        monster.Agent.SetDestination(_target.transform.position);

        // 해당위치에 도착했을때
        if (Vector3.Distance(_target.transform.position, character.transform.position) < 2f)
        {
            monster.ChangeState(new StateAttack_Monster());
        }
    }

    public override void OnLeave(BaseCharacter character)
    {
        base.OnLeave(character);
    }
}
