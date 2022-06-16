using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBulletMove : MonoBehaviour
{
    [SerializeField]
    private float _speed = 20f;

    [SerializeField]
    protected Vector3 _dir = Vector3.forward;

    private Transform _transform = null;
    private void Start() {
        _transform = transform;
    }

    private void Update() {
        Move();
    }

    private void Move(){
        _transform.Translate(_dir * _speed * Time.deltaTime * GameManager.TimeScale);
    }
}
