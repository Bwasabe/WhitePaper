using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public abstract class EnemyState : MonoBehaviour
{
    protected Enemy _enemy;

    private void Awake() {
        _enemy = GetComponent<Enemy>();
    }

    public abstract void Init();

    public abstract void Update();
}
