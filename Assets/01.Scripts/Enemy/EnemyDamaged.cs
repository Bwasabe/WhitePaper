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
    public override void Damage(int damage)
    {
        base.Damage(damage);

        if(_hp >= 0){
            _renderer.material.DOColor(_hitColor, 0.1f).SetLoops(2, LoopType.Yoyo);
        }
    }
    protected override void Dead()
    {
        Debug.Break();
    }

}
