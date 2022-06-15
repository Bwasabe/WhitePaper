using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    protected override void OnClickShow()
    {
        if (!_isShow)
        {
            _isShow = true;
            base.OnClickShow();
        }
    }

    protected override void OnClickHide()
    {
        if (_isShow)
        {
            Debug.Log("눌림");
            _isShow = false;
            base.OnClickHide();
        }
    }
}
