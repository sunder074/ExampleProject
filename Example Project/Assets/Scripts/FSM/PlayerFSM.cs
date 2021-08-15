using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFSM : FSM
{
    public override void InIt(BaseCharacter character, eFSM_Type eFSM_Type)
    {
        _character = character;
        _current_FSM_Type = eFSM_Type;

        // 아무상태도 아닐때
        State state = new StateIdle_Player();
        state.StateID = (int)State.eState_Player.E_IDLE;
        _dicState.Add(state.StateID, state);

        // 공격해야할 타겟이 없는 상태에서 움직일때
        state = new StateWalk_Player();
        state.StateID = (int)State.eState_Player.E_WALK;
        _dicState.Add(state.StateID, state);

        // 공격해야할 타겟이 있는 상태에서 움직일때
        state = new StateChase_Player();
        state.StateID = (int)State.eState_Player.E_CHASE;
        _dicState.Add(state.StateID, state);

        // 전투중 다음 패턴을 위해 대기중일때
        state = new StateBattleIdle_Player();
        state.StateID = (int)State.eState_Player.E_BATTLE_IDLE;
        _dicState.Add(state.StateID, state);

        // 타겟을 공격할때
        state = new StateAttack_Player();
        state.StateID = (int)State.eState_Player.E_ATTACK;
        _dicState.Add(state.StateID, state);

        // 공격을 받았을때
        state = new StateDamaged_Player();
        state.StateID = (int)State.eState_Player.E_DAMAGED;
        _dicState.Add(state.StateID, state);

        // 죽었을때
        state = new StateDead_Player();
        state.StateID = (int)State.eState_Player.E_DEAD;
        _dicState.Add(state.StateID, state);

        // 처음 상태는 Idle 상태로 세팅
        SetState(new StateIdle_Player());
    }

    public override void Update()
    {
        if (_character == null) return;

        if (_current_State != null)
        {
            _current_State.OnUpdate(_character);
        }
    }

    /// <summary>
    /// 현재상태와 바뀔상태를 비교후 바꿔주는 매서드
    /// </summary>
    /// <param name="state">바뀔 상태</param>
    public override void SetState(State state)
    {
        // 맨처음에는 상태가 없으므로 바로 세팅
        if (_current_State == null)
        {
            ChangeState(state.StateID);
        }

        // 현재 상태와 바뀔 상태가 같은 상태일때
        else if (_current_State.StateID == state.StateID)
        {
            return;
        }

        else
        {
            ChangeState(state.StateID);
        }
    }

    /// <summary>
    /// 상태를 바꿔주는 매서드
    /// </summary>
    /// <param name="stateID">바뀔 상태의 ID</param>
    private void ChangeState(int stateID)
    {
        if (_character == null) return;

        // 해당 상태가 현재 상태머신에 존재할때
        if (_dicState.ContainsKey(stateID))
        {
            if (_current_State != null)
            {
                _current_State.OnLeave(_character);
            }

            _current_State = _dicState[stateID];
            _current_State.OnEnter(_character);
        }
    }
}
