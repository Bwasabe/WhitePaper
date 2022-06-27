using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAttack : MonoBehaviour
{
    protected BaseItem _baseItem;

    [SerializeField]
    private LayerMask _hitLayers;

    private void Awake() {
        _baseItem = GetComponent<BaseItem>();
    }

    public virtual void Attack(){}

    protected virtual void OnTriggerEnter(Collider other) {
        if((1 << other.gameObject.layer & _hitLayers) > 0){
            IDamageable damageable = other.GetComponent<IDamageable>();
            if(damageable != null)
                damageable.Damage(_baseItem.ItemStatus.Str);
        }
    }

}
