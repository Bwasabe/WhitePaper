using static Yields;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle : EnemyState
{
    [SerializeField]
    private float _idleDuration = 0f;

    private void Start() {
        _enemy.SetState(ENEMY_STATE.IDLE,this);
    }

    public override void Init()
    {
        StartCoroutine(IdleToPatrol());
    }

    private IEnumerator IdleToPatrol(){
        _idleDuration = Random.Range(0.5f, 1.5f);
        yield return WaitForSeconds(_idleDuration);
        _enemy.ENUMSTATE = ENEMY_STATE.PATROL;
    }

    public override void Update()
    {
        
    }
}
