using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    [SerializeField]
    private float _gravityScale = 1f;

    private Rigidbody _rb = null;

    private void Start() {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        SetGravity();
    }

    private void SetGravity(){
        _rb.AddForce(Vector3.down * _gravityScale, ForceMode.Acceleration);
    }
}
