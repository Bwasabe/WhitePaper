using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using DG.Tweening;
using UnityEngine.UI;

public class PlayerDamaged : CharacterDamaged
{
    [SerializeField]
    private Volume _volume;

    private Vignette _vigenette;

    [SerializeField]
    private Text _hpText;

    [SerializeField]
    private GameObject _deadObject;
    private void Start()
    {
        _volume.sharedProfile.TryGet<Vignette>(out _vigenette);

    }
    public override void Damage(int damage)
    {
        base.Damage(damage);

        if (_hp > 0)
        {
            DOTween.To(
                () => _vigenette.intensity.value,
                value => _vigenette.intensity.value = value,
                0.4f,0.1f
            ).SetLoops(2, LoopType.Yoyo);
        }

        _hpText.text = $"남은 체력 : {_hp}";
    }

    protected override void Dead()
    {
        _deadObject.SetActive(true);
    }
}
