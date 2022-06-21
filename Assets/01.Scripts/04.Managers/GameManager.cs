using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{

    public static float TimeScale { get; set; } = 1f;

    public PlayerMove PlayerMove
    {
        get
        {
            if (_playerMove == null)
            {
                _playerMove = FindObjectOfType<PlayerMove>();
            }
            return _playerMove;
        }
    }

    private PlayerMove _playerMove;

    public PlayerController PlayerCtrl
    {
        get
        {
            if (_playerCtrl == null)
            {
                _playerCtrl = FindObjectOfType<PlayerController>();
            }
            return _playerCtrl;
        }
    }

    private PlayerController _playerCtrl;

}
