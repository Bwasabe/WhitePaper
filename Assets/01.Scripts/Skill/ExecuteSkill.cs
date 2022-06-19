using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecuteSkill : MonoBehaviour
{
    private Action _ultimateAction;

    private void Update() {
        Ultimate();
    }

    public void ClearAction(){
        _ultimateAction = () => { };
    }

    public void RegisterAction(Action action){
        _ultimateAction += action;
        Debug.Log("잉");
    }

    private void Ultimate(){
        if(Input.GetKeyDown(KeyCode.Q)){
            _ultimateAction?.Invoke();
        }
    }
}
