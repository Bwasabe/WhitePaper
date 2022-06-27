using static Yields;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonAttack : BaseAttack
{
    [SerializeField]
    private AnimationClip[] _spearAttacks;

    private Animator _animator;


    private readonly int ANIMNAMEHASH = Animator.StringToHash("SpearAttack");

    private readonly int ATTACKCOUNT = Animator.StringToHash("AttackCount");
    private int _attackCount;

    private PlayerMove _playerMove;
    private void Start() {
        _animator = GameManager.Instance.PlayerCtrl.Animator;
        _playerMove = GameManager.Instance.PlayerMove;
    }

    public override void Attack()
    {
        if(_playerMove.PlayerState.HasFlag(PlayerMove.PLAYERSTATE.ATTACK))return;
        _playerMove.PlayerState |= PlayerMove.PLAYERSTATE.ATTACK;

        _animator.Play(ANIMNAMEHASH);
        _animator.SetFloat(ATTACKCOUNT, _attackCount);

        StartCoroutine(IsAttack());

        _attackCount = (_attackCount + 1) % _spearAttacks.Length;
    }

    private IEnumerator IsAttack(){
        yield return WaitForSeconds(_spearAttacks[_attackCount].length);
        _playerMove.PlayerState &= ~PlayerMove.PLAYERSTATE.ATTACK;
    }
    
}
