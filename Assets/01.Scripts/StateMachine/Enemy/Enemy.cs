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
    private EnemyState _currentState;

    public void SetState(EnemyState state){
        _currentState = state;
        _currentState.Init();
    }

    private void Update() {
        _currentState.Update();
    }
}
