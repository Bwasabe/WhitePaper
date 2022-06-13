using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public enum PLAYERANIMATIONSTATE{
        IDLE,
        MOVE,
        JUMP,
        ATTACK,

    }

    private Animator _animator = null;

    public PLAYERANIMATIONSTATE _playerAnimationState = PLAYERANIMATIONSTATE.IDLE;

    private readonly int STATEHASH = Animator.StringToHash("PlayerState"); 
    private void Start() {
        _animator = GetComponent<Animator>();
    }

    private void Update() {
        _animator.SetInteger(STATEHASH, (int)_playerAnimationState);
    }


}
