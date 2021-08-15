using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateWalk_Player : State
{
    Vector3 _targetPos;

    public StateWalk_Player()
    {
        StateID = (int)eState_Player.E_WALK;
    }

    public override void OnEnter(BaseCharacter character)
    {
        base.OnEnter(character);

        Player player = (Player)character;

        _targetPos = player.TargetPos;

        // 이동할 위치 지정
        player.Agent.isStopped = false;
        player.Agent.SetDestination(_targetPos);
        player.Animator.SetBool("bMove", true);
        player.Animator.ResetTrigger("tAttack1");
    }

    public override void OnUpdate(BaseCharacter character)
    {
        base.OnUpdate(character);

        Player player = (Player)character;

        _targetPos = player.TargetPos;
        player.Agent.SetDestination(_targetPos);

        // 해당위치에 도착했을때
        if (Vector3.Distance(_targetPos, character.transform.position) < 2f)
        {
            player.ChangeState(new StateIdle_Player());
        }
    }

    public override void OnLeave(BaseCharacter character)
    {
        base.OnLeave(character);
    }
}
