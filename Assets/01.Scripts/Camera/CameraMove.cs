using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    private float _sencetive = 3f;

    [SerializeField]
    private Transform _player;

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

        _rotateY = Mathf.Clamp(_rotateY, -50f, 89f);
        //_rotateX = Mathf.Clamp(_rotateX, -52f, 52f);
        //TODO: 나중에는 얼굴만 돌리다가 90도?단위 정도로 돌아갔을때만 몸 돌리는거로 바꿀 것
        _player.rotation = Quaternion.Euler(0f, _rotateX, 0f);
        transform.rotation = Quaternion.Euler(-_rotateY, _rotateX, 0f);
    }

}
