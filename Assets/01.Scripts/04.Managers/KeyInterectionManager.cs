using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInterectionManager : MonoBehaviour
{
    private bool _isCursorHide = false;

    private EventParam _cursorParam;

    private event Action<EventParam> _escapeAction;
    private void Start()
    {
        _cursorParam = new EventParam { objs = new object[]{ true } };
        Debug.Log(_cursorParam.objs[0]);
        _escapeAction = _cursorParam =>
        {
            SetCursorLock((bool)_cursorParam.objs[0]);
        };

        ParamEventManager.StartListening(KeyCode.Escape.ToString(), _escapeAction);
        //ParamEventManager.StopListening(KeyCode.Escape.ToString(), _escapeAction);
        ParamEventManager.TriggerEvent(KeyCode.Escape.ToString(), _cursorParam);
        //SetCursorLock(true);

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ParamEventManager.TriggerEvent(KeyCode.Escape.ToString(), _cursorParam);
            _isCursorHide = !_isCursorHide;
            _cursorParam.objs[0] = _isCursorHide;
        }
    }

    private void SetCursorLock(bool value)
    {
        if (value == true)
        {
            _isCursorHide = false;
            Debug.Log("마우스 잠김");
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            _isCursorHide = true;
            Debug.Log("마우스 풀림");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
