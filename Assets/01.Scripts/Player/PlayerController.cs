using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public ExecuteSkill Skill { get; set; }

    [SerializeField]
    private Transform _itemTransform;

    public Transform ItemTransform => _itemTransform;

    private void Start()
    {
        Skill = GetComponent<ExecuteSkill>();

    }
}
