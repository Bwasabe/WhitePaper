using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class NewPlayerMove : MonoBehaviour
{
    public enum PLAYERSTATE
    {
        IDLE = 0,
        JUMP = 1,
        MOVE = 2,
        DASH = 3,
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

    private Animator _animator = null;

    private int IDLE = Animator.StringToHash("BasicMotions@Idle01");

    private int MOVE = Animator.StringToHash("BasicMotions@Run01 - Forwards");

    private int JUMP = Animator.StringToHash("BasicMotions@Jump01");

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
        _camTransform = Camera.main.transform;

    }


    private void Update()
    {
        Move();
        Jump();
        ReadyDash();
        AnimationCtrl();
    }

    private void AnimationCtrl()
    {
        switch (PlayerState)
        {
            case PLAYERSTATE.IDLE:
                _animator.Play(IDLE);
                break;
            case PLAYERSTATE.JUMP:
                _animator.Play(JUMP);
                break;
            case PLAYERSTATE.MOVE:
                _animator.Play(MOVE);
                break;
        }
    }

    private void Move()
    {
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        Vector3 forward = _camTransform.forward;
        forward.y = 0f;

        Vector3 right = new Vector3(forward.z, 0f, -forward.x);

        Vector3 dir = (right * h + forward * v).normalized * _speed;

        _currentHit = _characterController.Move(dir * Time.deltaTime);
        if (PlayerState != PLAYERSTATE.JUMP &&  PlayerState != PLAYERSTATE.DASH)
        {
            if (dir != Vector3.zero)
                PlayerState = PLAYERSTATE.MOVE;
            else
                PlayerState = PLAYERSTATE.IDLE;
        }

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
        if (IsGround() && _playerVelocity.y <= 0f )
        {
            _playerVelocity.y = 0f;
            if(PlayerState == PLAYERSTATE.JUMP)
                PlayerState = PLAYERSTATE.IDLE;
        }
        else
        {
            _playerVelocity.y += Physics.gravity.y * Time.deltaTime * _gravityScale;
        }
        if (Input.GetKeyDown(KeyCode.Space) && IsGround())
        {
            _playerVelocity.y += Mathf.Sqrt(_jumpForce * -2.0f * Physics.gravity.y);
            PlayerState = PLAYERSTATE.JUMP;
        }
        _animator.SetBool("IsGround", IsGround());
        _animator.SetFloat("VelocityY", _playerVelocity.y);
        _currentHit = _characterController.Move(_playerVelocity * Time.deltaTime);
    }

    private void ReadyDash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _dashDir = _camTransform.forward * _dashSpeed;
            PlayerState = PLAYERSTATE.DASH;
            StartCoroutine(Dash());
        }

        _characterController.Move(_dashDir * Time.deltaTime);
    }


    private IEnumerator Dash()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerState = PLAYERSTATE.IDLE;
        _dashDir = Vector3.zero;
    }

    private void OnGUI()
    {
        var labelStyle = new GUIStyle();
        labelStyle.fontSize = 50;
        labelStyle.normal.textColor = Color.red;
        GUILayout.Label($"플레이어 상태 : {PlayerState.ToString()}", labelStyle);
        GUILayout.Label($"땅에 닿았는가 : {IsGround()}", labelStyle);


    }

}

