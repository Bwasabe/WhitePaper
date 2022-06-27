using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public PlayerExecuteSkill Skill { get; private set; }
    public PlayerAttack Attack { get; private set; }
    public Animator Animator { get; private set; }
    public AnimationController AnimationController { get; private set; }


    [SerializeField]
    private Transform _itemTransform;

    public Transform ItemTransform => _itemTransform;

    [SerializeField]
    private Status _playerStatus;

    public Status PlayerStatus { 
        get{
            return _playerStatus;
        }
        set{
            _playerStatus = value;
        }
    }

    private void Awake()
    {
        Animator = transform.GetChild(0).GetComponent<Animator>();
        AnimationController = Animator.GetComponent<AnimationController>();
        Skill = GetComponent<PlayerExecuteSkill>();
        Attack = GetComponent<PlayerAttack>();
    }
}
