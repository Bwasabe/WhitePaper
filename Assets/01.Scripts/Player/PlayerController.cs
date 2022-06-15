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



    private Transform _camTransform = null;

    private Coroutine _rayCoroutine = null;


    private int _isRayHit = 0;

    private int _isDistanceOutFive = 0;

    private PlayerMove _playerMove;

    private CharacterController _characterController;

    //private Sequence _attackSequence;

    private void Start()
    {
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
