using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item/ItemStatus", order = 0)]
public class Item : ScriptableObject {
    [SerializeField]
    private int _str;

    [SerializeField]
    private int _agi;

    [SerializeField]
    private int _hp;

    [SerializeField]
    private float _attackSpeed;



    public int Str => _str;

    public int Agi => _agi;

    public int Hp => _hp;

    public float AttackSpeed => _attackSpeed;
}