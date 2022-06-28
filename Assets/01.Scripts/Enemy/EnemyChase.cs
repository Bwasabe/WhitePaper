using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : EnemyState
{

    [SerializeField]
    private float _radius = 3f;

    [SerializeField]
    private LayerMask _hitLayer;

    [SerializeField]
    private float _runSpeed = 5f;
    [SerializeField]
    private float _viewRange = 10f;
    [SerializeField]
    private float _attackRange = 3f;
    [SerializeField]
    private float _angleSmooth = 5f;

    protected Transform _target;

    private Rigidbody _rb;

    private void Start() {
        _enemy.SetState(ENEMY_STATE.CHASE, this);
        _rb = GetComponent<Rigidbody>();
    }

    public override void Init()
    {
        
    }

    public override void EnemyUpdate()
    {
        ChaseTarget();
        CheckDistance();
        CheckAttack();
    }

    protected void ChaseTarget(){
        Vector3 targetPos = _target.position;
        targetPos.y = transform.position.y;
        Vector3 dir = targetPos - transform.position;

        dir.Normalize();

        _rb.velocity = dir * _runSpeed;

        if(dir != Vector3.zero)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir * Time.deltaTime), _angleSmooth * Time.deltaTime);
    }

    protected void CheckDistance(){
        Vector3 dir = _target.position - transform.position;

        float magDistance = dir.sqrMagnitude;

        if(magDistance >= Mathf.Pow(_viewRange, 2f)){
            _enemy.ChangeState(ENEMY_STATE.IDLE);
        }
    }

    protected void CheckAttack(){
        Vector3 dir = _target.position - transform.position;

        float magDistance = dir.sqrMagnitude;

        if(magDistance <= Mathf.Pow(_attackRange, 2f)){
            Debug.Log("어택");
            _enemy.ChangeState(ENEMY_STATE.ATTACK);
        }
    }



    public void CheckChase()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, _radius, _hitLayer);

        foreach (Collider col in cols)
        {
            if (col != null)
            {
                Debug.Log("바뀜");
                _target = col.transform;
                _enemy.ChangeState(ENEMY_STATE.CHASE);
                return;
            }
        }
    }
}
