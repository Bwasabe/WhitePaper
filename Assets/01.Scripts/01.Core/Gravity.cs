using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    [SerializeField]
    private float _gravityScale = 1f;

    [SerializeField]
    private LayerMask _groundLayer;

    private Rigidbody _rb = null;

    // private void Start() {
    //     _rb = GetComponent<Rigidbody>();
    // }

    // private void FixedUpdate() {
    //     SetGravity();
    // }

    // private void SetGravity(){
    //     _rb.AddForce(Vector3.down * _gravityScale, ForceMode.Acceleration);
    // }

    private void Start() {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        SetGravity();
    }

    private void SetGravity(){
        Ray ray = new Ray(transform.position, Vector3.down);

        RaycastHit raycastHit;
        if(Physics.Raycast(ray,out raycastHit, 2f, _groundLayer)){
            Debug.Log(raycastHit.distance);
        }
    }
}
