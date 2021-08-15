using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateResetPosition_Monster : State
{
    public StateResetPosition_Monster()
    {
        StateID = (int)eState_Monster.E_RESET_POSITION;
    }

    public override void OnEnter(BaseCharacter character)
    {
        Monster monster = (Monster)character;

        monster.Agent.destination = monster.OriginalPos;
        monster.Agent.speed = monster.Speed * 2;
        monster.Animator.SetBool("bMove", true);
        monster.Agent.isStopped = false;
    }

    public override void OnUpdate(BaseCharacter character)
    {
        Monster monster = (Monster)character;

        // 처음위치로 돌아왔을때
        if (Vector3.Distance(monster.transform.position, monster.OriginalPos) <= 2f)
        {
            monster.ChangeState(new StateIdle_Monster());
        }
    }

    public override void OnLeave(BaseCharacter character)
    {
        Monster monster = (Monster)character;

        // 처음위치로 돌아갔을때 이동속도와 HP 리셋
        monster.Agent.speed = monster.Speed;
        monster.CurrentHP = monster.MaxHP;
    }
}
