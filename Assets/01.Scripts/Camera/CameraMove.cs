using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    private float _sencetive = 3f;

    private float _rotateX = 0f;
    private float _rotateY = 0f;

    private void LateUpdate() {
        Move();
    }

    private void Move(){
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        _rotateX += x * _sencetive;
        _rotateY += y * _sencetive;

        _rotateY = Mathf.Clamp(_rotateY, -89f, 89f);

        transform.parent.rotation = Quaternion.Euler(0f, _rotateX, 0f);
        transform.rotation = Quaternion.Euler(-_rotateY, _rotateX, 0f);
    }

}
