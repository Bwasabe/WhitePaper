using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBulletMove : MonoBehaviour
{
    [SerializeField]
    private float _speed = 20f;

    private Transform _transform = null;
    private void Start() {
        _transform = transform;
    }

    private void Update() {
        Move();
    }

    private void Move(){
        _transform.Translate(Vector3.forward * _speed * Time.deltaTime * GameManager.Instance.TimeScale);
    }
}
