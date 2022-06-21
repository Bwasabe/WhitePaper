using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInterectionManager : MonoBehaviour
{
    private EventParam _cursorParam;

    private event Action<EventParam> _escapeAction;
    private void Start()
    {
        _cursorParam = new EventParam { objs = new object[]{ true } };
        Debug.Log(_cursorParam.objs[0]);
        _escapeAction = _cursorParam =>
        {
            SetCursorLock((bool)_cursorParam.objs[0]);
            //_cursorParam.objs[0] = !(bool)_cursorParam.objs[0];
        };

        ParamEventManager.StartListening(KeyCode.Escape.ToString(), _escapeAction);

        ParamEventManager.TriggerEvent(KeyCode.Escape.ToString(), _cursorParam);
        //SetCursorLock(true);

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _cursorParam.objs[0] = true;
            ParamEventManager.TriggerEvent(KeyCode.Escape.ToString(), _cursorParam);

            EventManager.TriggerEvent(KeyCode.Escape.ToString());
        }

        if(Input.GetKeyDown(KeyCode.E)){
            _cursorParam.objs[0] = false;
            ParamEventManager.TriggerEvent(KeyCode.Escape.ToString(), _cursorParam);

            EventManager.TriggerEvent(KeyCode.E.ToString());
        }
    }

    private void SetCursorLock(bool value)
    {
        if (value == true)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
