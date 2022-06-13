using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CamFirstEnter : MonoBehaviour
{
    [SerializeField]
    private Transform[] _camPoses;
    [SerializeField]
    private Transform _cam;

    private Sequence _camSequence = null;

    private void Start()
    {
        _camSequence = DOTween.Sequence();
    }

    private void SetSequence()
    {
        for (int i = 0; i < _camPoses.Length; ++i)
        {
            _camSequence.Append(
                _cam.DOLocalMoveZ(5f, 3f).SetDelay(0.5f)
            ).AppendCallback(
                ()=>{
                    _cam.SetParent(_camPoses[i]);
                    _cam.position = Vector3.zero;
                }
            );
        }

    }


    private void GameMovementFirstEnter()
    {
        SetSequence();
        Debug.Log("ㅁㄴㅇㄹ");
        //_camSequence.Play();
        
    }
}
