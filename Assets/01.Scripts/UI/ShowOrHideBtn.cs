using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Button))]
public class ShowOrHideBtn : MonoBehaviour
{
    protected enum BUTTONTYPE
    {
        NONE,
        SHOW,
        HIDE,
    }

    [SerializeField]
    private List<GameObject> _activeObjs = new List<GameObject>();

    [SerializeField]
    private GameObject _hideFirstObject;

    [SerializeField]
    protected BUTTONTYPE _buttonType = BUTTONTYPE.NONE;

    [SerializeField]
    private CanvasGroup _uiCanvasGroup;

    [SerializeField]
    private List<Button> _interectBtns = new List<Button>();

    [SerializeField]
    private float _showDuration = 1f;

    [SerializeField]
    private float _hideDuration = 1f;

    private Button _button;

    private static Tweener _hideTweener;

    private static Tweener _showTweener;

    private void Start()
    {
        _button = GetComponent<Button>();

        switch (_buttonType)
        {
            case BUTTONTYPE.SHOW:
                _button.onClick.AddListener(OnClickShow);
                break;
            case BUTTONTYPE.HIDE:
                _button.onClick.AddListener(OnClickHide);
                break;
            default:
                Debug.LogError("버튼 타입이 잘못되었습니다");
                break;
        }
    }

    protected virtual void OnClickShow()
    {
        
        if (_hideTweener.IsActive())
        {
            _hideTweener.Kill();

            SetHideValue();

            _activeObjs.ForEach(obj =>
            {
                if (obj.activeSelf)
                {
                    obj.SetActive(false);
                }
            });
            _uiCanvasGroup.alpha = 0f;
        }
        SetShowValue();
        _hideFirstObject.SetActive(true);
        _showTweener = DOTween.To(
            () => _uiCanvasGroup.alpha,
            value => _uiCanvasGroup.alpha = value,
            1f, _showDuration
        ).OnComplete(() => Time.timeScale = 0f);
    }

    private void SetShowValue()
    {
        _interectBtns.ForEach(x => x.interactable = true);
        _button.interactable = false;

        _uiCanvasGroup.interactable = true;
        _uiCanvasGroup.blocksRaycasts = true;
        _activeObjs.ForEach(obj =>
        {
            if (!obj.activeSelf)
            {
                obj.SetActive(true);
            }
        });
    }


    protected virtual void OnClickHide()
    {
        if (_showTweener.IsActive())
        {
            _showTweener.Kill();
            SetShowValue();

            _uiCanvasGroup.alpha = 1f;
        }
        Time.timeScale = 1f;

        SetHideValue();
        _hideFirstObject.SetActive(false);
        _hideTweener = DOTween.To(
            () => _uiCanvasGroup.alpha,
            value => _uiCanvasGroup.alpha = value,
            0f, _hideDuration
        ).OnComplete(() =>
        {
            _activeObjs.ForEach(obj =>
            {
                if (obj.activeSelf)
                {
                    obj.SetActive(false);
                }
            });
        });
    }

    private void SetHideValue()
    {
        _interectBtns.ForEach(x => x.interactable = true);
        _button.interactable = false;

        _uiCanvasGroup.interactable = false;
        _uiCanvasGroup.blocksRaycasts = false;
    }

}
