using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    private bool _isCursorHide = false;

    public float TimeScale{ get; set; }

    private void Start()
    {
        SetCursorLock(true);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetCursorLock(_isCursorHide);
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
