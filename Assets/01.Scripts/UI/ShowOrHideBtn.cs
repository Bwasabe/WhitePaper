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
        GameManager.Instance.TimeScale = 0f;
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
        DOTween.To(
            () => _uiCanvasGroup.alpha,
            value => _uiCanvasGroup.alpha = value,
            1f, _showDuration
        );
    }

    protected virtual void OnClickHide()
    {
        GameManager.Instance.TimeScale = 1f;

        _interectBtns.ForEach(x => x.interactable = true);
        _button.interactable = false;

        _uiCanvasGroup.interactable = false;
        _uiCanvasGroup.blocksRaycasts = false;

        DOTween.To(
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
}
