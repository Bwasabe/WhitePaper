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

    [SerializeField]
    private Transform _itemTransform;

    public Transform ItemTransform => _itemTransform;

    public ExecuteSkill _skill { get; set; }


    private Transform _camTransform = null;


    private CharacterController _characterController;
    private CollisionFlags _currentHit;


    private Vector3 _playerVelocity;
    private Vector3 _dashDir;

    public bool IsNotGravity { get; set; } = false;

    private Animator _animator;

    private int VELOCITYHHASH = Animator.StringToHash("VelocityH");
    //private int VELOCITYVHASH = Animator.StringToHash("VelocityV");

    private int JUMPHASH = Animator.StringToHash("Jump");
    private int RETURNTOSTATEHASH = Animator.StringToHash("ReturnToState");

    private void Start()
    {
        _skill = GetComponent<ExecuteSkill>();
        _animator = GetComponent<Animator>();
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

        _currentHit = _characterController.Move(dir * Time.deltaTime * GameManager.TimeScale);
        if (dir == Vector3.zero)
            PlayerState &= ~PLAYERSTATE.MOVE;
        else
            PlayerState |= PLAYERSTATE.MOVE;

        _animator.SetFloat(VELOCITYHHASH, dir.x + dir.z);
    }


    private bool IsGround()
    {
        Vector3 pos1 = transform.position + _characterController.center;
        float value = _characterController.height * 0.5f;
        pos1.y += value;
        Vector3 pos2 = transform.position + _characterController.center;
        pos2.y -= value;
        

        return Physics.CheckCapsule(pos1, pos2, _characterController.radius, _groundLayer);
    }

    // private void OnGUI() {
    //     var labelStyle = new GUIStyle();
    //     labelStyle.fontSize = 50;
    //     labelStyle.normal.textColor = Color.red;

    //     GUILayout.Label($"땅에 닿았는가 : {IsGround()}", labelStyle);
    //     GUILayout.Label($"땅에 닿았는가 : {_characterController.isGrounded}", labelStyle);
    // }


    private void Jump()
    {
        if (IsGround() && _playerVelocity.y <= 0f)
        {
            _playerVelocity.y = 0f;
            PlayerState &= ~PLAYERSTATE.JUMP;
        }
        else
        {
            if (!IsNotGravity)
                _playerVelocity.y += Physics.gravity.y * Time.deltaTime * _gravityScale * GameManager.TimeScale;
        }
        if (Input.GetKeyDown(KeyCode.Space) && IsGround())
        {
            _playerVelocity.y += Mathf.Sqrt(_jumpForce * -2.0f * Physics.gravity.y);
            PlayerState |= PLAYERSTATE.JUMP;
            _animator.SetTrigger(JUMPHASH);
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

