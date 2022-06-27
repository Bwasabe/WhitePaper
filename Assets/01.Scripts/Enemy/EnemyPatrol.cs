using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : EnemyState
{
    private CharacterController _characterController;

    //TODO: private
    public float _moveTimer;

    public float _moveMaxTimer;

    [SerializeField]
    private Vector2 _randomValue;

    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private float _angleSmooth = 5f;

    public Vector3 _dir;
    private void Start()
    {
        _enemy.SetState(ENEMY_STATE.PATROL, this);
        _characterController = GetComponent<CharacterController>();
    }

    //TODO: 랜덤한 방향을 바라보고 움직인 후 Idle로 바꿈
    public override void Init()
    {
        _dir = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
        _moveMaxTimer = Random.Range(_randomValue.x, _randomValue.y);
    }

    public override void EnemyUpdate()
    {
        _moveTimer += Time.deltaTime;

        if (_moveTimer >= _moveMaxTimer)
        {
            _moveTimer = 0f;
            _enemy.ChangeState(ENEMY_STATE.IDLE);
        }
        _characterController.Move(_dir * _speed * Time.deltaTime);
        if (_dir != Vector3.zero)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_dir * Time.deltaTime), _angleSmooth * Time.deltaTime);
    }

}
