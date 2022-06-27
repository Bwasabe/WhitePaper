using static Yields;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : EnemyState
{

    [SerializeField]
    private AnimationClip _attackClip;

    private void Start() {
        _enemy.SetState(ENEMY_STATE.ATTACK, this);
    }

    public override void Init()
    {
        StartCoroutine(Attack());   
    }

    public override void EnemyUpdate()
    {
        
    }

    private IEnumerator Attack(){
        yield return WaitForSeconds(_attackClip.length);
        _enemy.ChangeState(ENEMY_STATE.CHASE);
    }
}
