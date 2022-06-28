using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoSingleton<EnemyManager>
{
    public int CurrentEnemyCount{ get; set; }

    public List<Enemy> EnemyList { get; set; } = new List<Enemy>();

    [SerializeField]
    private Text _enemyText;

    [SerializeField]
    private Text _infoText;

    private void Start() {
        CheckEnemy();
    }

    public void CheckEnemy(){
        _enemyText.text = $"{CurrentEnemyCount} / {EnemyList.Count}";

        if(CurrentEnemyCount == EnemyList.Count){
            _enemyText.gameObject.SetActive(false);

            _infoText.text = $"와 적들을 모두 죽였어요!";
        }
    }
}
