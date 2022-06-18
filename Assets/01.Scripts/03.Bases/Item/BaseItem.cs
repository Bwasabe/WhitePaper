using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private Item _itemStatus;

    public Item ItemStatus => _itemStatus;

    [SerializeField]
    private Vector3 _inventoryScale;

    [SerializeField]
    private bool _isDroped;

    public Vector3 InventoryScale => _inventoryScale;

}


