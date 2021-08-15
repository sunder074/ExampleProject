using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAttack_Player : State
{
    public StateAttack_Player()
    {
        StateID = (int)eState_Player.E_ATTACK;
    }

    public override void OnEnter(BaseCharacter character)
    {
        Player player = (Player)character;

        player.Agent.isStopped = true;
        player.Animator.SetBool("bMove", false);

        // 공격할 타겟이 있을때
        if (player.Check_Target())
        {
            // 공격할 타겟이 공격범위 안에 있을때
            if(Vector3.Distance(player.transform.position, player.Target.transform.position) <= player.AttackRange)
            {
                player.transform.LookAt(player.Target.transform);
                player.Animator.SetTrigger("tAttack1");
            }

            else
            {
                player.ChangeState(new StateChase_Player());
            }
        }

        else
        {
            player.ChangeState(new StateIdle_Player());
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
