using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : BaseCharacter
{
    public Animator Animator { get { return _anim; } set { _anim = value; } }
    public NavMeshAgent Agent { get { return _agent; } set { _agent = value; } }
    public float AttackRange { get { return _attackRange; } set { _attackRange = value; } }
    public float Speed { get { return _moveSpeed; } set { _moveSpeed = value; } }
    public Vector3 OriginalPos { get; set; }

    /// <summary>
    /// 공격했을때 이펙트가 나올 위치
    /// </summary>
    [SerializeField] Transform _trans_HitEffectPos;

    BaseCharacter _target;
    public BaseCharacter Target { get { return _target; } }

    public Pattern CurrentPattern { get; set; }

    public int MaxHP { get { return _characterHP; } }
    public int CurrentHP { get; set; }

    public Vector3 TargetPos { get; set; }

    private void Start()
    {
        GamePlayStatics.LocalPlayer = gameObject;
        CurrentHP = _characterHP;
        Create_FSM();
    }

    private void Update()
    {
        PlayerInput();
        Check_TargetDead();
    }

    private void Create_FSM()
    {
        _fsm = new PlayerFSM();
        FSM.eFSM_Type fsm_Type = FSM.eFSM_Type.PLAYER;

        _fsm.InIt(this, fsm_Type);
        StartCoroutine(Cor_FSM_Update());
    }

    IEnumerator Cor_FSM_Update()
    {
        while (_fsm != null)
        {
            _fsm.Update();
            yield return null;
        }

        Create_FSM();
    }

    void PlayerInput()
    {
        // 죽어있을때 못움직이게 return
        if (IsDead) return;

        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                // 해당 위치가 이동가능한곳일때
                if (hit.collider.tag == "Ground")
                {
                    TargetPos = hit.point;
                    ObjectPoolManager.Instance.ActiveObject("MoveCurser", hit.point + Vector3.up * 0.5f, Vector3.zero);
                    ChangeState(new StateWalk_Player());
                }
            
                else if (hit.collider.tag == "Monster")
                {
                    _target = hit.collider.GetComponent<Monster>();
                    ChangeState(new StateChase_Player());
                }
            }
        }

        else if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                // 해당 위치가 이동가능한곳일때
                if (hit.collider.tag == "Ground")
                {
                    TargetPos = hit.point;
                    ChangeState(new StateWalk_Player());
                }
            }
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
        if (_target == null)
        {
            return false;
        }

        else
        {
            return true;
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
            ChangeState(new StateDead_Player());
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
    /// 공격하는 애니메이션이 끝났을때 상태를 바꿔주기 위해 호출
    /// </summary>
    public void AttackEnd()
    {
        if (Check_Target())
        {
            ChangeState(new StateBattleIdle_Player());
        }

        else
        {
            ChangeState(new StateIdle_Player());
        }

        Animator.SetTrigger("tAttackEnd");
        CurrentPattern = null;
    }

    /// <summary>
    /// 공격하는 애니메이션이 시작했을때 알리기 위한 매서드
    /// </summary>
    public void AttackStart()
    {
        
    }

    /// <summary>
    /// 타겟이 죽었는지 체크
    /// </summary>
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
