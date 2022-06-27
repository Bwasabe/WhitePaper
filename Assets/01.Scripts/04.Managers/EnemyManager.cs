using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoSingleton<EnemyManager>
{
    public int CurrentEnemyCount{ get; set; }

    [SerializeField]
    private List<Enemy> _enemyLists = new List<Enemy>();

    [SerializeField]
    private Text _enemyText;

    [SerializeField]
    private Text _infoText;

    private void Start() {
        CheckEnemy();
    }

    public void CheckEnemy(){
        _enemyText.text = $"{CurrentEnemyCount} / {_enemyLists.Count}";

        if(CurrentEnemyCount == _enemyLists.Count){
            _enemyText.gameObject.SetActive(false);

            _infoText.text = $"와 적들을 모두 죽였어요!";
        }
    }
}
