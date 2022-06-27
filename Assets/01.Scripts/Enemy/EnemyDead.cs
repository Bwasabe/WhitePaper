using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyDead : EnemyState
{
    private Animator _animator;

    [SerializeField]
    private List<GameObject> _enableFalseObjects;

    [SerializeField]
    private Renderer _renderer;

    private Rigidbody _rb;

    private readonly int DEAD = Animator.StringToHash("Dead");
    private void Start() {
        _enemy.SetState(ENEMY_STATE.DEAD, this);

        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }
    public override void EnemyUpdate()
    {
        
    }

    public override void Init()
    {
        _animator.SetTrigger(DEAD);
        EnemyManager.Instance.CurrentEnemyCount++;
        EnemyManager.Instance.CheckEnemy();
        _enemy.enabled = false;
        _rb.useGravity = false;
        _rb.velocity = Vector3.zero;
        _enableFalseObjects.ForEach(x => x.SetActive(false));
        _renderer.material.DOFade(0f, 1f).OnComplete(() => gameObject.SetActive(false));
    }
}
