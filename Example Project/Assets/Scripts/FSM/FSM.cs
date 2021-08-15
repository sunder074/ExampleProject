using System.Collections;
using System.Collections.Generic;

public class FSM
{
    public enum eFSM_Type
    {
        PLAYER,
        MONSTER,
    }

    protected Dictionary<int, State> _dicState = new Dictionary<int, State>();
    protected State _current_State = null;
    protected eFSM_Type _current_FSM_Type;
    protected BaseCharacter _character;

    /// <summary>
    /// 상태머신을 세팅하는 매서드
    /// </summary>
    /// <param name="character">상태머신을 사용할 오브젝트</param>
    /// <param name="Event_OnEnter">처음 상태를 들어왔을때 이벤트</param>
    /// <param name="Event_OnUpdate">상태가 진행중일때 이벤트</param>
    /// <param name="Event_OnLeave">상태를 나갈때 이벤트</param>
    /// <param name="eFSM_Type">상태머신을 사용하는 오브젝트의 타입</param>
    public virtual void InIt(BaseCharacter character, eFSM_Type eFSM_Type)
    {

    }

    public virtual void Update()
    {

    }

    public virtual void SetState(State state)
    {

    }

    public virtual State GetCurrentState()
    {
        return _current_State;
    }

    public virtual eFSM_Type GetCurrentFSM_Type()
    {
        return _current_FSM_Type;
    }
}
