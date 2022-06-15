using static Define;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoonUltimate : BaseSkill
{
    [SerializeField]
    private Light _light;

    [SerializeField]
    private Material _attackSkybox;

    [SerializeField]
    private float _attackDistance = 8f;

    //몇초만에 도착했으면 좋겠는지
    [SerializeField]
    private float _dashTime = 0.25f;

    [SerializeField]
    private PlayerMove _playerMove;

    private CharacterController _characterController;

    private Transform _camTransform;

    private Coroutine _distanceCoroutine = null;

    private Tweener _readyToUltimateTween;


    private void Start()
    {
        _camTransform = MainCam.transform;
        _characterController = _playerMove.transform.GetComponent<CharacterController>();

        //계산을 위해 역수로 변환
        _dashTime = 1 / _dashTime;
    }
    public override void Skill()
    {
        //if(_readyToUltimateTween != null && _readyToUltimateTween.IsPlaying())return;
        if (!_playerMove.PlayerState.HasFlag(PlayerMove.PLAYERSTATE.READYTOSMASH))
        {
            _readyToUltimateTween = _light.DOIntensity(0f, 1f).OnComplete(() =>
            {
                RenderSettings.skybox = _attackSkybox;

                _playerMove.PlayerState |= PlayerMove.PLAYERSTATE.READYTOSMASH;
            });
            return;
        }

        StartCoroutine(DashAttack());
    }

    private IEnumerator DashAttack()
    {
        float timer = 0f;
        while (timer <= 0.25f)
        {
            timer += Time.deltaTime;
            _characterController.Move(_camTransform.forward * _attackDistance * Time.deltaTime * _dashTime);
            yield return null;

        }
    }
}
