using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern
{
    float _coolTime;
    float _attackRange;
    float _currentTime;
    string _trigger;

    public float CoolTime { get { return _coolTime; } }
    public float AttackRange { get { return _attackRange; } }
    public float CurrentTime { get{ return _currentTime; } set { _currentTime = value; } }
    public string Trigger { get { return _trigger; } }

    public void SetPattern(float coolTime, float attackRange, string trigger)
    {
        _coolTime = coolTime;
        _attackRange = attackRange;
        _currentTime = 0f;
        _trigger = trigger;
    }

    public void OnPattern()
    {
        _currentTime = _coolTime;
    }
}
