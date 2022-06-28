using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcBossChase : EnemyChase
{
    public override void Init()
    {
        _target = GameManager.Instance.PlayerCtrl.transform;
    }

    public override void EnemyUpdate()
    {
        if(_target != null)
            ChaseTarget();
    }
}
