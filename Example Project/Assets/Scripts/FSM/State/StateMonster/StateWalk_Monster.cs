using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateWalk_Monster : State
{
    float _maxIdleTime = 10f;
    Vector3 _targetPos;

    public StateWalk_Monster()
    {
        StateID = (int)eState_Monster.E_WALK;
    }

    public override void OnEnter(BaseCharacter character)
    {
        base.OnEnter(character);

        Monster monster = (Monster)character;

        _targetPos = monster.GetRandomPos();

        // 이동할 위치 지정
        monster.Agent.isStopped = false;
        monster.Agent.SetDestination(_targetPos);
        monster.Animator.SetBool("bMove", true);
    }

    public override void OnUpdate(BaseCharacter character)
    {
        base.OnUpdate(character);

        Monster monster = (Monster)character;

        // 해당위치에 도착했을때
        if (Vector3.Distance(_targetPos, character.transform.position) < 2f)
        {
            monster = (Monster)character;
            monster.ChangeState(new StateIdle_Monster());
        }

        // 몬스터가 리셋되는 범위를 벗어났을때
        else if (monster.Check_ResetRange())
        {
            monster = (Monster)character;
            monster.ChangeState(new StateResetPosition_Monster());
        }

        else if (CheckStayMaxTime(_maxIdleTime))
        {
            monster = (Monster)character;
            monster.ChangeState(new StateIdle_Monster());
        }
    }

    public override void OnLeave(BaseCharacter character)
    {
        base.OnLeave(character);
    }
}
