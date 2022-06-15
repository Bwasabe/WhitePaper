using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SKILLTYPE{
    NONE,
    MOON,
    FIRE,
    LENGTH,
}

public abstract class BaseSkill : MonoBehaviour
{
    [SerializeField]
    private SKILLTYPE _skillType = SKILLTYPE.NONE;

    public abstract void Skill();
}
