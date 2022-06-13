using static Define;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private LayerMask _hitLayer;

    [SerializeField]
    private GameObject _bullet = null;

    [SerializeField]
    private Transform _shootPos = null;
    [SerializeField]
    private Light _light;


    [SerializeField]
    private Material _attackSkybox;

    private Transform _camTransform = null;

    private Coroutine _rayCoroutine = null;
    private Coroutine _distanceCoroutine = null;


    private int _isRayHit = 0;

    private int _isDistanceOutFive = 0;

    private PlayerMove _playerMove;

    private CharacterController _characterController;

    //private Sequence _attackSequence;

    private void Start()
    {
        _playerMove = GetComponent<PlayerMove>();
        _characterController = GetComponent<CharacterController>();
        _camTransform = MainCam.transform;
    }

    private void Update()
    {
        Attack();
        AttackLong();
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!_playerMove.PlayerState.HasFlag(PlayerMove.PLAYERSTATE.READYTOSMASH))
            {
                _light.DOIntensity(0f, 1f).OnComplete(() =>
                {
                    RenderSettings.skybox = _attackSkybox;

                    _playerMove.PlayerState |= PlayerMove.PLAYERSTATE.READYTOSMASH;
                });
                return;
            }

            if (!_playerMove.PlayerState.HasFlag(PlayerMove.PLAYERSTATE.READYTOSMASH)) return;
            Ray ray = new Ray(_camTransform.position, _camTransform.forward);

            RaycastHit raycastHit;

            if (Physics.Raycast(ray, out raycastHit, Mathf.Infinity, _hitLayer, QueryTriggerInteraction.Ignore))
            {
                // 맞음
                _isRayHit = 1;

                // 스카이박스 넣어주기

                float distance = Vector2.Distance(
                new Vector2(transform.position.x, transform.position.z),
                new Vector2(raycastHit.transform.position.x, raycastHit.transform.position.z)
                );

                if (distance <= 5f)
                {
                    _isDistanceOutFive = 1;
                    if (_distanceCoroutine != null)
                    {
                        StopCoroutine(_distanceCoroutine);
                    }
                    _distanceCoroutine = StartCoroutine(ReturnToFalse());
                    return;
                }
                else
                {
                    _isDistanceOutFive = 2;
                    if (_distanceCoroutine != null)
                    {
                        StopCoroutine(_distanceCoroutine);
                    }
                    _distanceCoroutine = StartCoroutine(ReturnToFalse());
                }

                StartCoroutine(Smash(distance));
            }
            else
            {
                // 안맞음
                _isRayHit = 2;
            }

            if (_rayCoroutine != null)
            {
                StopCoroutine(_rayCoroutine);
            }
            _rayCoroutine = StartCoroutine(ReturnToZero());
        }

    }

    private IEnumerator Smash(float distance)
    {
        float timer = 0f;
        while (timer <= 0.25f)
        {
            timer += Time.deltaTime;
            _characterController.Move(_camTransform.forward * distance * 1.2f * Time.deltaTime * 4f);
            yield return null;

        }
    }

    private IEnumerator ReturnToFalse()
    {
        yield return new WaitForSeconds(1f);
        _isDistanceOutFive = 0;
    }

    private IEnumerator ReturnToZero()
    {
        yield return new WaitForSeconds(1f);
        _isRayHit = 0;
    }

    private void AttackLong()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            GameObject g = Instantiate(_bullet, _shootPos.position, Quaternion.identity);
            g.transform.rotation = _camTransform.rotation;
            g.SetActive(true);
        }
    }


    private void OnGUI()
    {
        var labelStyle = new GUIStyle();
        labelStyle.fontSize = 50;
        labelStyle.normal.textColor = Color.red;
        GUILayout.Label($"플레이어 상태 : {_playerMove.PlayerState.ToString()}", labelStyle);
        if (_isRayHit != 0)
        {
            if (_isRayHit == 1)
            {
                GUILayout.Label("레이 맞음", labelStyle);
            }
            else
            {
                GUILayout.Label("레이 안 맞음", labelStyle);
            }
        }

        if (_isDistanceOutFive != 0)
        {
            if (_isDistanceOutFive == 1)
            {
                GUILayout.Label("거리가 너무 짧음", labelStyle);
            }
            else
            {
                GUILayout.Label("거리가 너무 김", labelStyle);
            }
        }

    }
}
