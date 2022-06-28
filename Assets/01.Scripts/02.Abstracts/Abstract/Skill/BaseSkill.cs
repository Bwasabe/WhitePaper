using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BaseSkill : MonoBehaviour
{
    [SerializeField]
    protected ITEMTYPE _skillType = ITEMTYPE.NONE;
    [SerializeField]
    private Sprite _skillSprite;

    public Sprite SkillSprite => _skillSprite;

    [SerializeField]
    protected PlayerExecuteSkill _executeSkill;

    public abstract void Skill();
}
