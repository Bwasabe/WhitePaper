using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterDamaged : MonoBehaviour, IDamageable
{
    [SerializeField]
    protected int _hp;

    public virtual void Damage(int damage)
    {
        _hp -= damage;

        if (_hp <= 0)
        {
            Dead();
        }
    }

    protected abstract void Dead();
}
