using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ENEMY_STATE
{
    IDLE,
    PATROL,
    CHASE,
    INVINCIBLE,
    DEAD,
}

public class Enemy : MonoBehaviour
{
    public ENEMY_STATE CurrentState{ get; set; }
    //private ENEMY_STATE _currentState = ENEMY_STATE.IDLE;
    private Dictionary<ENEMY_STATE, EnemyState> _stateMachine = new Dictionary<ENEMY_STATE, EnemyState>();

    [SerializeField]
    private Status _enemyStatus;

    public void SetState(ENEMY_STATE enumState, EnemyState state)
    {
        _stateMachine[enumState] = state;
    }

    public void ChangeState(ENEMY_STATE enumState)
    {
        _stateMachine[enumState].Init();
        CurrentState = enumState;
    }

    private void Update()
    {
        _stateMachine[CurrentState].EnemyUpdate();
    }
}
