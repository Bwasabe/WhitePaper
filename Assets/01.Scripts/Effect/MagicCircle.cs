using static Yields;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class MagicCircle : MonoBehaviour
{
    [SerializeField]
    private float _animationSpeed = 0.1f;

    [SerializeField]
    private Vector2 _startOffset = new Vector2(0f,0.05f);

    [SerializeField]
    private float _offsetX = 0.25f;
    [SerializeField]
    private float _offsetY = 0.2f;


    [SerializeField]
    private int _row = 4;

    [SerializeField]
    private int _column = 4;

    private MeshRenderer _meshRenderer;

    private Vector2 _currentOffset;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _currentOffset = _startOffset;
        StartCoroutine(ChangeImage());
    }
    private IEnumerator ChangeImage()
    {
        while (true)
        {
            yield return WaitForSeconds(_animationSpeed);
            ChangeOffset();
        }
    }

    private void ChangeOffset()
    {
        // 오른쪽으로 한칸 이동
        _currentOffset.x += _offsetX;
        // 만약 오른쪽 행에 도달하면 한줄 내려줌
        if (_currentOffset.x >= _offsetX * _row)
        {
            _currentOffset.x = _startOffset.x;

            _currentOffset.y += _offsetY;
            if (_currentOffset.y >= _offsetY * _column)
            {
                // 만약 오른쪽 끝에 열에 도착하면 다시 왼
                _currentOffset.y = _startOffset.y;

            }
        }
        _meshRenderer.material.SetTextureOffset("_MainTex", _currentOffset);
    }
}
