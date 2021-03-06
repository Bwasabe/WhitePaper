using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : EnemyState
{
    private Rigidbody _rb;

    //TODO: private
    private float _moveTimer;

    private float _moveMaxTimer;

    [SerializeField]
    private Vector2 _randomValue;

    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private float _angleSmooth = 5f;

    private Vector3 _dir;

    private EnemyChase _enemyChase;
    private void Start()
    {
        _enemy.SetState(ENEMY_STATE.PATROL, this);
        _rb = GetComponent<Rigidbody>();
        _enemyChase = GetComponent<EnemyChase>();
    }

    public override void Init()
    {
        _dir = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized * _speed;
        _moveMaxTimer = Random.Range(_randomValue.x, _randomValue.y);
    }

    public override void EnemyUpdate()
    {
        _moveTimer += Time.deltaTime;

        if (_moveTimer >= _moveMaxTimer)
        {
            _moveTimer = 0f;
            _rb.velocity = Vector3.zero;
            _enemy.ChangeState(ENEMY_STATE.IDLE);
            return;
        }
        _dir.y = _rb.velocity.y;
        _rb.velocity = _dir;
        if (_dir != Vector3.zero){
            Vector3 rotateDir = _dir;
            rotateDir.y = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(rotateDir * Time.deltaTime), _angleSmooth * Time.deltaTime);
        }

        _enemyChase.CheckChase();
    }

}
