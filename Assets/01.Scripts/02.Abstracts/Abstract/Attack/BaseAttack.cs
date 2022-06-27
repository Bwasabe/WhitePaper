using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAttack : MonoBehaviour
{
    protected BaseItem _baseItem;

    private void Awake() {
        _baseItem = GetComponent<BaseItem>();
    }

    public abstract void Attack();
}
