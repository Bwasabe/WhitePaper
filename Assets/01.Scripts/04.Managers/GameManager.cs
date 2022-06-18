using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{

    public static float TimeScale { get; set; } = 1f;

    public PlayerMove Player
    {
        get
        {
            if (_player == null)
            {
                _player = FindObjectOfType<PlayerMove>();
            }
            return _player;
        }
    }

    private PlayerMove _player;

}
