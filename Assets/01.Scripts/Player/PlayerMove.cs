using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMove : MonoBehaviour
{
    [System.Flags]
    public enum PLAYERSTATE
    {
        IDLE = 0,//0
        DASH = 1 << 0,//1
        JUMP = 1 << 1,//2
        MOVE = 1 << 2,//4
        READYTOSMASH = 1 << 3,//8
        SMASH = 1 << 4,//16
    }

    public PLAYERSTATE PlayerState { get; set; }


    [SerializeField]
    private float _speed = 8f;
    [SerializeField]
    private float _jumpForce = 15f;
    [SerializeField]
    private float _dashSpeed = 20f;
    [SerializeField]
    private float _gravityScale = 3f;

    [SerializeField]
    private LayerMask _groundLayer;


    private Transform _camTransform = null;


    private CharacterController _characterController;
    private CollisionFlags _currentHit;


    private Vector3 _playerVelocity;
    private Vector3 _dashDir;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _camTransform = Camera.main.transform;

    }


    private void Update()
    {
        
        Move();
        Jump();
        ReadyDash();
    }

    private void Move()
    {
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        Vector3 forward = _camTransform.forward;
        forward.y = 0f;

        Vector3 right = new Vector3(forward.z, 0f, -forward.x);

        Vector3 dir = (right * h + forward * v).normalized * _speed;

        _currentHit = _characterController.Move(dir * Time.deltaTime * GameManager.Instance.TimeScale);
        if (dir == Vector3.zero)
            PlayerState &= ~PLAYERSTATE.MOVE;
        else
            PlayerState |= PLAYERSTATE.MOVE;
    }

    private bool IsGround()
    {
        Vector3 pos1 = transform.position;
        float value = _characterController.height * 0.5f - _characterController.radius + 0.1f;
        pos1.y += value;
        Vector3 pos2 = transform.position;
        pos2.y -= value;


        return Physics.CheckCapsule(pos1, pos2, _characterController.radius, _groundLayer);
    }


    private void Jump()
    {
        if (IsGround() && _playerVelocity.y <= 0f)
        {
            _playerVelocity.y = 0f;
            PlayerState &= ~PLAYERSTATE.JUMP;
        }
        else
        {
            if(!PlayerState.HasFlag(PLAYERSTATE.DASH) || !PlayerState.HasFlag(PLAYERSTATE.READYTOSMASH))
                _playerVelocity.y += Physics.gravity.y * Time.deltaTime * _gravityScale;
        }
        if (Input.GetKeyDown(KeyCode.Space) && IsGround())
        {
            _playerVelocity.y += Mathf.Sqrt(_jumpForce * -2.0f * Physics.gravity.y);
            PlayerState |= PLAYERSTATE.JUMP;
        }
        _currentHit = _characterController.Move(_playerVelocity * Time.deltaTime * GameManager.Instance.TimeScale);
    }

    private void ReadyDash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _dashDir = _camTransform.forward * _dashSpeed;
            _playerVelocity.y = 0f;
            
            PlayerState |= PLAYERSTATE.DASH;
            StartCoroutine(Dash());
        }

        _characterController.Move(_dashDir * Time.deltaTime * GameManager.Instance.TimeScale);
    }


    private IEnumerator Dash()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerState &= ~PLAYERSTATE.DASH;
        _dashDir = Vector3.zero;
    }

}

