using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAttack : MonoBehaviour
{
    protected BaseItem _baseItem;

    [SerializeField]
    private LayerMask _hitLayers;

    private void Awake() {
        _baseItem = GetComponent<BaseItem>();
    }

    public abstract void Attack();

    protected virtual void OnTriggerEnter(Collider other) {
        Debug.Log("맞음");
        if((1 << other.gameObject.layer & _hitLayers) > 0){
            other.GetComponent<IDamageable>().Damage(_baseItem.ItemStatus.Str);
        }
    }
}
