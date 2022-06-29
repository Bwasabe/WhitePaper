using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShowOrHideKeyInterection : ShowOrHideBtn
{
    private static bool _isShow = false;
    private void Awake()
    {
        switch (_buttonType)
        {
            case BUTTONTYPE.SHOW:
                EventManager.StartListening(KeyCode.E.ToString(), OnClickShow);
                break;
            case BUTTONTYPE.HIDE:
                EventManager.StartListening(KeyCode.Escape.ToString(), OnClickHide);
                break;

        }
        KeyInterectionManager.Instance.SetCursorLock(true);
    }

    protected override void OnClickShow()
    {
        if (!_isShow)
        {
            _isShow = true;
            KeyInterectionManager.Instance.SetCursorLock(false);
            base.OnClickShow();
        }
    }

    protected override void OnClickHide()
    {
        if (_isShow)
        {
            _isShow = false;
            KeyInterectionManager.Instance.SetCursorLock(true);
            base.OnClickHide();

        }
    }
}