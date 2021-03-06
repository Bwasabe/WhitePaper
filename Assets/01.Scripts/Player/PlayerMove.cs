using static Define;
using static Yields;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [System.Flags]
    public enum PLAYERSTATE
    {
        IDLE = 0,//0
        DASH = 1 << 0,//1
        JUMP = 1 << 1,//2
        MOVE = 1 << 2,//4
        ATTACK = 1 << 3,
    }

    public PLAYERSTATE PlayerState { get; set; }


    [SerializeField]
    private float _jumpForce = 15f;
    [SerializeField]
    private float _dashSpeed = 20f;
    [SerializeField]
    private float _gravityScale = 3f;

    [SerializeField]
    private int _jumpMaxCount = 1;
    [SerializeField]
    private int _jumpCount;


    [SerializeField]
    private LayerMask _groundLayer;


    private Transform _camTransform = null;


    private CharacterController _characterController;
    private CollisionFlags _currentHit;


    private Vector3 _playerVelocity;
    private Vector3 _dashDir;

    public bool IsNotGravity { get; set; } = false;

    private Status _playerStatus;

    private Animator _animator;

    private readonly int WALK = Animator.StringToHash("Walk");
    private readonly int PLAYERSTATEHASH = Animator.StringToHash("PlayerState");
    private void Start()
    {
        _playerStatus = GameManager.Instance.PlayerCtrl.PlayerStatus;
        _characterController = GetComponent<CharacterController>();
        _camTransform = MainCam.transform;
        _animator = GameManager.Instance.PlayerCtrl.Animator;
    }

    private void Update()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        Vector3 forward = _camTransform.forward;
        forward.y = 0f;

        Vector3 right = new Vector3(forward.z, 0f, -forward.x);

        Vector3 dir = (right * h + forward * v).normalized * _playerStatus.Agi;

        _currentHit = _characterController.Move(dir * Time.deltaTime * GameManager.TimeScale);
        if (dir == Vector3.zero)
        {
            if (!(PlayerState.HasFlag(PLAYERSTATE.JUMP) || PlayerState.HasFlag(PLAYERSTATE.ATTACK)))
            {
                _animator.SetInteger(PLAYERSTATEHASH, (int)PLAYERSTATE.IDLE);
            }
            PlayerState &= ~PLAYERSTATE.MOVE;
        }
        else
        {
            if (!(PlayerState.HasFlag(PLAYERSTATE.JUMP) || PlayerState.HasFlag(PLAYERSTATE.ATTACK)))
            {
                _animator.SetInteger(PLAYERSTATEHASH, (int)PLAYERSTATE.MOVE);
            }
            PlayerState |= PLAYERSTATE.MOVE;
        }


    }

    Vector3 pos1;
    Vector3 pos2;
    private bool IsGround()
    {
        pos1 = transform.position + _characterController.center;
        float value = _characterController.height * 0.5f;
        pos1.y += value;
        pos2 = transform.position;

        return Physics.CheckCapsule(pos1, pos2, _characterController.radius, _groundLayer);
    }

    private void OnGUI()
    {
        var labelStyle = new GUIStyle();
        labelStyle.fontSize = 50;
        labelStyle.normal.textColor = Color.red;

        GUILayout.Label($"?????? ???????????? : {_characterController.isGrounded}", labelStyle);
        GUILayout.Label($"?????? ???????????? : {IsGround()}", labelStyle);
        GUILayout.Label($"???????????? ?????? : {_playerVelocity.y}", labelStyle);

        GUILayout.Label($"Pos1 : {transform.forward}", labelStyle);
        GUILayout.Label($"Pos2 : {pos2}", labelStyle);
        GUILayout.Label($"???????????? : {PlayerState}", labelStyle);
    }


    private void Jump()
    {
        if (IsGround() && _playerVelocity.y <= 0f)
        {
            _jumpCount = 0;
            _playerVelocity.y = 0f;
            PlayerState &= ~PLAYERSTATE.JUMP;
        }
        else
        {
            if (!IsNotGravity)
            {
                Debug.Log("?????? ?????????");
                _playerVelocity.y += Physics.gravity.y * Time.deltaTime * _gravityScale * GameManager.TimeScale;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!Physics.CheckCapsule(pos1, new Vector3(pos2.x, pos2.y - 1f, pos2.z), _characterController.radius, _groundLayer))
            {
                Debug.Log("?????? ??????");
                _jumpCount = 1;
            }
            else
            {
                Debug.Log("?????? ??????");
            }
            if (_jumpCount < _jumpMaxCount)
            {
                _jumpCount++;
                _playerVelocity.y = Mathf.Sqrt(_jumpForce * -2.0f * Physics.gravity.y) * GameManager.TimeScale;
                PlayerState |= PLAYERSTATE.JUMP;
            }

            _animator.SetInteger(PLAYERSTATEHASH,(int)PLAYERSTATE.JUMP);
        }



        _currentHit = _characterController.Move(_playerVelocity * Time.deltaTime * GameManager.TimeScale);
    }

    public void RemoveGravity()
    {
        _playerVelocity.y = 0f;
    }

    private void ReadyDash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            IsNotGravity = true;
            _dashDir = _camTransform.forward * _dashSpeed;
            _playerVelocity.y = 0f;

            PlayerState |= PLAYERSTATE.DASH;
            StartCoroutine(Dash());
        }

        _characterController.Move(_dashDir * Time.deltaTime * GameManager.TimeScale);
    }

    private IEnumerator Dash()
    {
        yield return WaitForSeconds(0.5f);
        IsNotGravity = false;
        PlayerState &= ~PLAYERSTATE.DASH;
        _dashDir = Vector3.zero;
    }

}

