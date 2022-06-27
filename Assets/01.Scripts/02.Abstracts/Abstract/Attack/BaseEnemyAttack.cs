using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyAttack : MonoBehaviour
{

    [SerializeField]
    private LayerMask _hitLayers;

    [SerializeField]
    private int _damage = 10;

    public virtual void Attack(){}

    protected virtual void OnTriggerEnter(Collider other) {
        if((1 << other.gameObject.layer & _hitLayers) > 0){
            IDamageable damageable = other.GetComponent<IDamageable>();
            if(damageable != null)
                damageable.Damage(_damage);
        }
    }
}
