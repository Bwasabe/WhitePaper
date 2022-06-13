using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class FirstEnterTextMove : MonoBehaviour
{
    [SerializeField]
    private RectTransform _textPos = null;

    [SerializeField]
    private TMP_Text _mapNameText = null;

    private Sequence _textSequence = null;

    private void Start() {
        _textSequence = DOTween.Sequence();
    }

    private void SetSequence(){
        _textSequence.Append(
            _mapNameText.rectTransform.DOAnchorPos(_textPos.anchoredPosition, 2f).SetDelay(0.5f).SetEase(Ease.OutQuad)
        );

        _textSequence.Append(
            _mapNameText.DOFade(0f, 1.5f).SetDelay(0.5f)
        );

    }

    private void GameMovementFirstEnter()
    {
        SetSequence();
    }

}
