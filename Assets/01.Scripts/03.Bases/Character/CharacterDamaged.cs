using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterDamaged : MonoBehaviour, IDamageable
{
    [SerializeField]
    protected int _hp;

    protected bool _isDead;

    public virtual void Damage(int damage)
    {
        if(_isDead)return;
        _hp -= damage;

        if (_hp <= 0)
        {
            _isDead = true;
            Dead();
        }
    }

    protected abstract void Dead();
}
