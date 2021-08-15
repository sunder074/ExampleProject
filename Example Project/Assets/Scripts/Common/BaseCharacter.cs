using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseCharacter : MonoBehaviour
{
    [Header("Common")]
    [Header("Components")]
    [SerializeField] protected NavMeshAgent _agent;
    [SerializeField] protected Animator _anim;

    [Header("Status")]
    [SerializeField] protected int _characterHP;
    [SerializeField] protected float _attackRange;
    [SerializeField] protected float _moveSpeed;

    protected FSM _fsm;
    protected bool _isDead;

    public bool IsDead { get { return _isDead; } }

    public virtual void OnTargetAttack()
    {

    }

    public virtual void OnDamaged(BaseCharacter enemy)
    {

    }
}
