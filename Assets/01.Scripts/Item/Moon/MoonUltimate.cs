using static Define;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MoonUltimate : BaseSkill
{
    [SerializeField]
    private Light _light;

    [SerializeField]
    private Material _attackSkybox;

    [SerializeField]
    private float _lightIntencity = 0.2f;
    [SerializeField]
    private float _oldLightIntencity = 5f;

    [SerializeField]
    private float _attackDistance = 8f;

    [SerializeField]
    private float _skillDuration = 6f;

    [SerializeField]
    private float _skillColtime = 10f;

    //몇초만에 도착했으면 좋겠는지
    [SerializeField]
    private float _dashTime = 0.25f;

    [SerializeField]
    private int _skillMaxCount = 3;


    private PlayerMove _playerMove;

    private CharacterController _characterController;

    private Transform _camTransform;

    private Animator _animator;
    private Tweener _readyToUltimateTween;

    private readonly int SPEARULTIMATE = Animator.StringToHash("SpearUltimate");

    private bool _isSkill = false;

    private int _skillCount;

    private Material _oldSkyBox;

    private bool _isCanUseSkill = true;

    private Outline _skillIconOutline;

    private bool _isAttackCompleted;

    private void Start()
    {
        _camTransform = MainCam.transform;
        _playerMove = GameManager.Instance.PlayerMove;
        _animator = GameManager.Instance.PlayerCtrl.Animator;
        _characterController = _playerMove.transform.GetComponent<CharacterController>();

        _skillIconOutline = UIManager.Instance.SkillIcon.GetComponent<Outline>();
        //계산을 위해 역수로 변환
        _dashTime = 1 / _dashTime;

    }
    public override void Skill()
    {
        //if(_readyToUltimateTween != null && _readyToUltimateTween.IsPlaying())return;
        if (!_isCanUseSkill) return;
        if (!_isSkill)
        {
            _oldSkyBox = RenderSettings.skybox;
            _readyToUltimateTween = _light.DOIntensity(_lightIntencity, 1f).OnComplete(() =>
            {
                RenderSettings.skybox = _attackSkybox;

                _isSkill = true;
                UIManager.Instance.DOSkillIcon(100f, _skillDuration, () =>
                {
                    _isCanUseSkill = false;
                    if(!_isAttackCompleted)
                        AttackComplete();
                });
            });

            return;
        }
        _skillCount++;
        _animator.Play(SPEARULTIMATE);
        _playerMove.IsNotGravity = true;
        _playerMove.RemoveGravity();
        StartCoroutine(DashAttack());
    }


    private IEnumerator DashAttack()
    {
        float timer = 0f;
        while (timer <= 0.25f)
        {
            timer += Time.deltaTime;
            _characterController.Move(_camTransform.forward * _attackDistance * Time.deltaTime * _dashTime * GameManager.TimeScale);
            yield return null;
        }
        _playerMove.IsNotGravity = false;
        if (_skillCount >= _skillMaxCount)
        {
            UIManager.Instance.DOSkillIcon(100f, 0f);
            _isAttackCompleted = true;
            AttackComplete();
        }
    }

    private void AttackComplete()
    {
        _skillCount = 0;
        _isSkill = false;
        RenderSettings.skybox = _oldSkyBox;
        _light.DOIntensity(_oldLightIntencity, 1f).OnComplete(() =>
        {
            UIManager.Instance.DOSkillIcon(0f, _skillColtime - 1f, () =>
            {
                //다시 아이콘이 차오르고
                UIManager.Instance.SkillRectMask.enabled = false;

                DOTween.To( // 아이콘이 좌우로 분열했다가
                    () => _skillIconOutline.effectDistance.x,
                    value => _skillIconOutline.effectDistance = new Vector2(value, 0f),
                        100f, 0.5f
                    ).SetEase(Ease.OutCirc).OnComplete(() =>
                    {
                        DOTween.To( // 다시 합쳐진 후
                            () => _skillIconOutline.effectDistance.x,
                            value => _skillIconOutline.effectDistance = new Vector2(value, 0f),
                            0f, 0.5f
                        ).SetEase(Ease.OutElastic).OnComplete(() =>
                        {
                            // 스킬 사용 가능으로 바꾸기
                            _isCanUseSkill = true;
                            _isAttackCompleted = false;
                            UIManager.Instance.SkillRectMask.enabled = true;
                        });
                    });
            });
        });
    }
}
