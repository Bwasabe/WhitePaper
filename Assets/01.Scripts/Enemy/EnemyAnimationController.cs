using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    private Enemy _enemy;

    private Animator _animator;

    private readonly int STATE = Animator.StringToHash("State");    
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        if(_enemy.CurrentState == ENEMY_STATE.INVINCIBLE || _enemy.CurrentState == ENEMY_STATE.DEAD)return;
            _animator.SetInteger(STATE, (int)_enemy.CurrentState);
    }
}
