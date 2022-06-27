using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Status {
    public Status(Status status){
        this._agi = status.Agi;
        // this._attackSpeed = status.AttackSpeed;
        // this._hp = status.Hp;
        this._str = status.Str;

    }
    [SerializeField]
    private int _str;

    [SerializeField]
    private int _agi;

    // [SerializeField]
    // private int _hp;

    // [SerializeField]
    // private float _attackSpeed;



    public int Str => _str;

    public int Agi => _agi;

    // public int Hp => _hp;

    // public float AttackSpeed => _attackSpeed;
}