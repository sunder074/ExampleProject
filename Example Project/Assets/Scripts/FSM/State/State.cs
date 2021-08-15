using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class State
{
    public enum eState_Monster
    {
        E_IDLE,

        // 이동관련
        E_WALK,
        E_CHASE,
        E_RESET_POSITION,

        // 전투관련
        E_BATTLE_IDLE,
        E_ATTACK,
        E_DAMAGED,
        E_DEAD,
    }

    public enum eState_Player
    {
        E_IDLE,

        // 이동관련
        E_WALK,
        E_CHASE,

        // 전투관련
        E_BATTLE_IDLE,
        E_ATTACK,
        E_DAMAGED,
        E_DEAD,
    }

    public int StateID { get; set; }

    protected bool _isEnter = false;
    protected float _stay_Time = 0f;

    public virtual void OnEnter(BaseCharacter character)
    {
        _stay_Time = 0f;

        _isEnter = true;
    }

    public virtual void OnUpdate(BaseCharacter character)
    {
        if (!_isEnter) return;

        _stay_Time += Time.deltaTime;
    }

    public virtual void OnLeave(BaseCharacter character)
    {
        _isEnter = false;
    }

    /// <summary>
    /// 현재 상태에 지속된 시간을 초기화
    /// </summary>
    public void ResetStayTime()
    {
        _stay_Time = 0f;
    }

    /// <summary>
    /// 현재 상태를 유지하는 최대시간이 지났는지 체크
    /// </summary>
    /// <param name="maxTime">현재 상태를 유지하는 최대시간</param>
    /// <returns>최대시간을 지났을때 true</returns>
    public bool CheckStayMaxTime(float maxTime)
    {
        return (_stay_Time > maxTime);
    }
}
