using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyDamaged : CharacterDamaged
{
    [SerializeField]
    private Renderer _renderer;

    [SerializeField]
    private Color _hitColor;
    [SerializeField]
    private GameObject _hitSound;

    private Enemy _enemy;

    private void Start() {
        _enemy = GetComponent<Enemy>();
    }
    public override void Damage(int damage)
    {
        base.Damage(damage);
        GameObject g =Instantiate(_hitSound, transform);
        g.SetActive(true);
        if(_hp >= 0){
            _renderer.material.DOColor(_hitColor, 0.3f).SetLoops(2, LoopType.Yoyo);
        }
    }

    protected override void Dead()
    {
        _enemy.ChangeState(ENEMY_STATE.DEAD);
    }
}
