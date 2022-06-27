using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum ITEMTYPE
{
    NONE,
    MOON,
    FIRE,
    LENGTH,
}

public class BaseItem : MonoBehaviour
{
    [SerializeField]
    private string _itemName;
    public string Name => _itemName;

    [SerializeField]
    private Color _nameColor = Color.white;
    public Color NameColor => _nameColor;

    [SerializeField]
    private string _skillName;
    public string SkillName => _skillName;

    [SerializeField]
    private Color _skillNameColor = Color.white;
    public Color SkillNameColor => _skillNameColor;


    [SerializeField]
    [TextArea]
    private string _skillExplain;
    public string SkillExplain => _skillExplain;


    [SerializeField]
    private Status _itemStatus;

    public Status ItemStatus => _itemStatus;

    [SerializeField]
    private Vector3 _inventoryScale;

    public Vector3 InventoryScale => _inventoryScale;

    [SerializeField]
    private Vector3 _inventoryPos;
    public Vector3 InventoryPos => _inventoryPos;

    [SerializeField]
    private Vector3 _handPos;
    public Vector3 HandPos => _handPos;

    [SerializeField]
    private Vector3 _handRotate;
    public Vector3 HandRotate => _handRotate;

    public bool IsDroped { get; set; } = false;

}



