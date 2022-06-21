using static Define;
using static Yields;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class FireUltimate : BaseSkill
{
    [SerializeField]
    private PlayerMove _player;

    [SerializeField]
    private ParticleSystemRenderer _magicCircleEffect;

    [SerializeField]
    private float _circleDuration = 1.3f;

    [SerializeField]
    private float _distance = 8f;

    [SerializeField]
    private float _height = 20f;

    [SerializeField]
    private GameObject _meteor;

    private Transform _camTransform = null;

    private void Start()
    {
        _camTransform = MainCam.transform;
    }

    public override void Skill()
    {
        Vector3 pos = _camTransform.forward;
        pos.y += 1.5f;

        ParticleSystemRenderer g = Instantiate(_magicCircleEffect, _player.transform);
        g.transform.rotation = Quaternion.identity;
        g.transform.localPosition = pos;
        g.transform.SetParent(null);
        g.gameObject.SetActive(true);

        this.StartCoroutine(ExecuteParticle(g));

    }
    private IEnumerator ExecuteParticle(ParticleSystemRenderer g)
    {
        // DOTween.To(
        //     () => g.minParticleSize,
        //     value => {
        //         g.maxParticleSize = value;
        //         g.minParticleSize = value;
        //     },
        //     1f, _circleDuration
        // );
        // /g.alignment = ParticleSystemRenderSpace.World;
        g.maxParticleSize = 1f;
        g.minParticleSize = 1f;
        yield return WaitForSeconds(_circleDuration);

        // DOTween.To(
        //     () => g.minParticleSize,
        //     value =>
        //     {
        //         g.minParticleSize = value;
        //         g.maxParticleSize = value;
        //     },
        //     0f, _circleDuration / 4
        // );

        // yield return WaitForSeconds(_circleDuration / 4);

        ExecuteMeteor();
    }

    private void ExecuteMeteor()
    {
        Vector3 camPos = _camTransform.forward;
        camPos.y += _height;
        camPos.z += _distance;

        GameObject g = Instantiate(_meteor, _player.transform);

        g.transform.rotation = Quaternion.identity;
        g.transform.localPosition = camPos;
        g.transform.SetParent(null);
        g.SetActive(true);
    }
}
