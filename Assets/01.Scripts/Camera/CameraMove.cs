using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    private float _sencetive = 3f;

    [SerializeField]
    private Transform _player;

    [SerializeField]
    private Vector3 _maxRotate;

    [SerializeField]
    private Vector3 _minRotate;

    private float _rotateX = 0f;
    private float _rotateY = 0f;

    private PlayerMove _playerMove;

    private void Start()
    {
        _playerMove = GameManager.Instance.PlayerMove;
    }
    private void LateUpdate()
    {
        Move();
    }

    private void Move()
    {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        _rotateX += x * _sencetive;
        _rotateY += y * _sencetive;

        _rotateY = Mathf.Clamp(_rotateY, _minRotate.y, _maxRotate.y);
        //TODO: 나중에는 얼굴만 돌리다가 90도?단위 정도로 돌아갔을때만 몸 돌리는거로 바꿀 것
        if (!_playerMove.PlayerState.HasFlag(PlayerMove.PLAYERSTATE.ATTACK))
        {
            _player.rotation = Quaternion.Euler(0, _rotateX, 0f);
            transform.rotation = Quaternion.Euler(-_rotateY, _rotateX, 0f);
        }
        
        // _player.rotation = Quaternion.Euler(
        //     Mathf.Clamp(-_rotateY, _minRotate.x, _maxRotate.x),
        //     Mathf.Clamp( _rotateX, _minRotate.y, _maxRotate.y),
        //     0f
        // );
    }

    private void Setbody()
    {

    }

    // private void OnGUI()
    // {
    //     var labelStyle = new GUIStyle();
    //     labelStyle.fontSize = 50;
    //     labelStyle.normal.textColor = Color.red;

    //     GUI.Label(new Rect(-40, 500, 100, 100), $"플레이어 얼굴 각도 : {_player.eulerAngles}", labelStyle);

    // }

}
