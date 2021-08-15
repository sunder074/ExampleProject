using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : BaseCharacter
{
    [Header("Individual")]
    /// <summary>
    /// 정찰 가능한 범위
    /// </summary>
    [SerializeField] float _patrolRange;

    /// <summary>
    /// 쫒을 수 있는 최대 범위
    /// </summary>
    [SerializeField] float _resetRange;

    /// <summary>
    /// 공격했을때 이펙트가 나올 위치
    /// </summary>
    [SerializeField] Transform _trans_HitEffectPos;

    public Animator Animator { get { return _anim; } set { _anim = value; } }
    public NavMeshAgent Agent { get { return _agent; } set { _agent = value; } }
    public float AttackRange { get { return _attackRange; } set { _attackRange = value; } }
    public float Speed { get { return _moveSpeed; } set { _moveSpeed = value; } }
    public Vector3 OriginalPos { get; set; }

    BaseCharacter _target;
    public BaseCharacter Target { get { return _target; } }

    Dictionary<string, Pattern> _dicPattern = new Dictionary<string, Pattern>();
    public Pattern CurrentPattern { get; set; }

    public int MaxHP { get { return _characterHP; } }
    public int CurrentHP { get; set; }
    public bool IsAttack { get; set; }

    private void Start()
    {
        OriginalPos = transform.position;
        CurrentHP = _characterHP;
        Create_FSM();
        Create_Pattern();
    }

    private void Update()
    {
        Check_TargetDead();
    }

    /// <summary>
    /// 몬스터용 FSM 생성
    /// </summary>
    private void Create_FSM()
    {
        _fsm = new MonsterFSM();
        FSM.eFSM_Type fsm_Type = FSM.eFSM_Type.MONSTER;

        _fsm.InIt(this, fsm_Type);
        StartCoroutine(Cor_FSM_Update());
    }

    IEnumerator Cor_FSM_Update()
    {
        while(_fsm != null)
        {
            _fsm.Update();
            yield return null;
        }

        Create_FSM();
    }

    /// <summary>
    /// 몬스터용 패턴 생성
    /// </summary>
    private void Create_Pattern()
    {
        Pattern pattern = new Pattern();
        pattern.SetPattern(2f, 2.5f, "tAttack1");

        _dicPattern.Add("Attack1", pattern);

        pattern = new Pattern();
        pattern.SetPattern(2f, 2.5f, "tAttack2");
        _dicPattern.Add("Attack2", pattern);

        pattern = new Pattern();
        pattern.SetPattern(2f, 2.5f, "tAttack3");
        _dicPattern.Add("Attack3", pattern);

        StartCoroutine(Cor_Pattern_Update());
    }

    IEnumerator Cor_Pattern_Update()
    {
        while (true)
        {
            foreach (KeyValuePair<string, Pattern> patterns in _dicPattern) 
            {
                // 패턴이 쿨타임이 돌고있을때
                if(patterns.Value.CurrentTime > 0)
                {
                    patterns.Value.CurrentTime -= Time.deltaTime;

                    if(patterns.Value.CurrentTime < 0)
                    {
                        patterns.Value.CurrentTime = 0f;
                    }
                }
            }

            yield return null;
        }
    }

    /// <summary>
    /// 상태 변경
    /// </summary>
    /// <param name="state"></param>
    public void ChangeState(State state)
    {
        _fsm.SetState(state);
    }

    /// <summary>
    /// 현재 공격할 타겟이 있는지 체크
    /// </summary>
    /// <returns></returns>
    public bool Check_Target()
    {
        if(_target == null)
        {
            return false;
        }

        else
        {
            return true;
        }
    }

    public bool Check_ResetRange()
    {
        if(Vector3.Distance(transform.position, OriginalPos) > _resetRange)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    /// <summary>
    /// 이동할 수 있는 랜덤위치 반환
    /// </summary>
    /// <returns></returns>
    public Vector3 GetRandomPos()
    {
        RaycastHit _hit;

        while (true)
        {
            float randomX = Random.Range(-_patrolRange, _patrolRange);
            float maxZ = Mathf.Sqrt(Mathf.Pow(_patrolRange, 2) - Mathf.Pow(randomX, 2));
            float randomZ = Random.Range(-maxZ, maxZ);

            Vector3 RandomPos = new Vector3(randomX, 10, randomZ);

            if(Physics.Raycast(RandomPos, Vector3.down, out _hit))
            {
                if(_hit.collider.tag == "Ground")
                {
                    return _hit.point;
                }
            }
        }
    }

    /// <summary>
    /// 공격하는 애니메이션중 타겟과 거리체크후 데미지 적용
    /// </summary>
    public override void OnTargetAttack()
    {
        if (Check_Target())
        {
            if (Vector3.Distance(transform.position, Target.transform.position) <= AttackRange)
            {
                ObjectPoolManager.Instance.ActiveObject("HitEffect", _trans_HitEffectPos.position, Vector3.zero);
                Target.OnDamaged(this);
            }
        }
    }

    /// <summary>
    /// 공격하는 애니메이션이 시작했을때 알리기 위한 매서드
    /// </summary>
    public void AttackStart()
    {
        IsAttack = true;
    }

    /// <summary>
    /// 공격하는 애니메이션이 끝났을때 상태를 바꿔주기 위한 매서드
    /// </summary>
    public void AttackEnd()
    {
        if (Check_Target())
        {
            ChangeState(new StateBattleIdle_Monster());
        }

        else
        {
            ChangeState(new StateIdle_Monster());
        }

        Animator.SetTrigger("tAttackEnd");
        IsAttack = false;
        CurrentPattern = null;
    }

    /// <summary>
    /// 사용가능한 패턴을 랜덤으로 설정
    /// </summary>
    public void SetRandomPattern()
    {
        List<Pattern> canUsePattern = new List<Pattern>();

        foreach (KeyValuePair<string, Pattern> patterns in _dicPattern)
        {
            // 사용 가능한 패턴들을 canUsePattern 리스트에 추가
            if(patterns.Value.CurrentTime == 0)
            {
                canUsePattern.Add(patterns.Value);
            }
        }

        // 사용 가능한 패턴이 없을때
        if(canUsePattern.Count == 0)
        {
            CurrentPattern = null;
        }

        else
        {
            int random = Random.Range(0, canUsePattern.Count);
            CurrentPattern = canUsePattern[random];
        }
    }

    /// <summary>
    /// 현재 발동중인 패턴 return
    /// </summary>
    /// <returns></returns>
    public Pattern GetCurrentPattern()
    {
        if(CurrentPattern != null)
        {
            return CurrentPattern;
        }

        else
        {
            SetRandomPattern();
            return CurrentPattern;
        }
    }

    /// <summary>
    /// 공격을 받았을때 데미지를 받는 매서드
    /// </summary>
    /// <param name="enemy">나를 공격한 상대</param>
    public override void OnDamaged(BaseCharacter enemy)
    {
        // 나를 공격한 대상이 없으면 return
        if (enemy == null) return;
        // 이미 죽었다면 return;
        if (IsDead) return;

        CurrentHP--;

        // 죽었을때
        if (CurrentHP <= 0)
        {
            _isDead = true;
            ChangeState(new StateDead_Monster());
        }

        else
        {
            _target = enemy;

            // 공격중이 아닐때
            if(!IsAttack)
            {
                ChangeState(new StateDamaged_Monster());
            }
        }
    }

    public void Check_TargetDead()
    {
        if (Target)
        {
            if (Target.IsDead)
            {
                _target = null;
            }
        }
    }

    public void OnDissolve()
    {
        Invoke("Dissolve", 2f);
    }

    void Dissolve()
    {
        Destroy(gameObject);
    }
}
