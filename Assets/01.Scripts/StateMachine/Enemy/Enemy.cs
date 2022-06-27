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
    public ENEMY_STATE ENUMSTATE{ get; set; }
    private Dictionary<ENEMY_STATE, EnemyState> _currentState;

    public void SetState(ENEMY_STATE enumState,EnemyState state){
        _currentState[enumState] = state;
        _currentState[enumState].Init();
    }

    private void Update() {
        _currentState[ENUMSTATE].Update();
    }
}
