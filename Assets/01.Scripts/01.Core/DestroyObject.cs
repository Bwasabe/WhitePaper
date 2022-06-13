using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    [SerializeField]
    private float _destroyDuration = 5f;
    private void Start() {
        Destroy(gameObject, _destroyDuration);
    }
}
