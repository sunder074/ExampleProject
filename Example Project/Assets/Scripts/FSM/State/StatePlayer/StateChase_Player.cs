using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateChase_Player : State
{
    BaseCharacter _target;

    public StateChase_Player()
    {
        StateID = (int)eState_Player.E_CHASE;
    }

    public override void OnEnter(BaseCharacter character)
    {
        base.OnEnter(character);

        Player player = (Player)character;

        // 쫒아야할 타겟이 있을때
        if (player.Check_Target())
        {
            // 공격 범위안에 있을때
            if (Vector3.Distance(player.Target.transform.position, character.transform.position) <= player.AttackRange)
            {
                player.ChangeState(new StateAttack_Player());
                return;
            }

            _target = player.Target;
            player.Agent.isStopped = false;
            player.Agent.SetDestination(_target.transform.position);
            player.Animator.SetBool("bMove", true);
            player.Animator.ResetTrigger("tAttack1");
        }

        else
        {
            player.ChangeState(new StateIdle_Player());
        }
    }

    public override void OnUpdate(BaseCharacter character)
    {
        Player player = (Player)character;

        if (player.Check_Target())
        {
            player.Agent.SetDestination(_target.transform.position);

            // 해당위치에 도착했을때
            if (Vector3.Distance(player.Target.transform.position, character.transform.position) <= player.AttackRange)
            {
                player.ChangeState(new StateAttack_Player());
            }
        }

        else
        {
            player.ChangeState(new StateIdle_Player());
        }
    }

    public override void OnLeave(BaseCharacter character)
    {
        base.OnLeave(character);
    }
}
