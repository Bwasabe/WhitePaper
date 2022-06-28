using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField]
    private Image _skillIcon;

    public Image SkillIcon => _skillIcon;

    private RectMask2D _skillRectMask;

    public RectMask2D SkillRectMask => _skillRectMask;

    private void Start()
    {
        _skillRectMask = _skillIcon.transform.parent.GetComponent<RectMask2D>();
    }

    public void DOSkillIcon(float endValue, float duration, Action callBack = null)
    {
        DOTween.To(
            () => _skillRectMask.padding.w,
            value => _skillRectMask.padding = new Vector4(0f, 0f, 0f, value),
            endValue, duration
        ).OnComplete(() =>
        {
            callBack?.Invoke();
        });
    }
}
