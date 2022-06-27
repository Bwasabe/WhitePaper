using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Action _attackAction;

    private void Update()
    {
        Attack();
    }

    public void ClearAction()
    {
        _attackAction = () => { };
    }

    public void RegisterAction(Action action)
    {
        _attackAction += action;
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _attackAction?.Invoke();
        }
    }

    
}
