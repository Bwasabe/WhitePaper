using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SKILLTYPE
{
    NONE,
    MOON,
    FIRE,
    LENGTH,
}

public abstract class BaseSkill : MonoBehaviour
{
    [SerializeField]
    protected SKILLTYPE _skillType = SKILLTYPE.NONE;

    [SerializeField]
    protected ExecuteSkill _executeSkill;


    protected virtual void Awake()
    {
        _executeSkill.RegisterAction(Skill);
    }


    public abstract void Skill();
}
