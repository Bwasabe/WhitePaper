using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcBossEnter : MonoBehaviour
{
    [SerializeField]
    private GameObject _fightWall;


    private Enemy _enemy;

    private void Start() {
        _enemy = transform.parent.GetComponent<Enemy>();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            gameObject.SetActive(false);
            _fightWall.SetActive(true);
            _enemy.ChangeState(ENEMY_STATE.CHASE);
        }
    }
}
