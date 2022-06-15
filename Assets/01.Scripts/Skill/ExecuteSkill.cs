using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExecuteSkill : MonoBehaviour
{
    [SerializeField]
    private UnityEvent _ultimateAction;

    private void Update() {
        Ultimate();
    }

    private void Ultimate(){
        if(Input.GetKeyDown(KeyCode.Q)){
            _ultimateAction?.Invoke();
        }
    }
}
