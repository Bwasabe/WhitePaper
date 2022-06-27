using static Yields;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle : EnemyState
{

    //TODO: private
    public float _idleDuration = 0f;

    [SerializeField]
    private Vector2 _randomValue;

    private void Start() {
        _enemy.SetState(ENEMY_STATE.IDLE,this);
        _enemy.ChangeState(ENEMY_STATE.IDLE);
    }

    public override void Init()
    {
        StartCoroutine(IdleToPatrol());
        Debug.Log("Idle Init");
    }

    private IEnumerator IdleToPatrol(){
        _idleDuration = Random.Range(_randomValue.x, _randomValue.y);
        yield return WaitForSeconds(_idleDuration);
        _enemy.ChangeState(ENEMY_STATE.PATROL);
    }

    public override void EnemyUpdate()
    {
        
    }
}