using static Yields;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoSingleton<EnemyManager>
{
    public int CurrentEnemyCount{ get; set; }

    public List<Enemy> EnemyList { get; set; } = new List<Enemy>();

    [SerializeField]
    private Text _enemyText;

    [SerializeField]
    private Text _infoText;


    private IEnumerator Start() {
        yield return WaitUntil(() => QuestManager.Instance.CurrentQuest >= 1);
        _infoText.text = "적들을 모두 처치하세요!";
        CheckEnemy();
    }

    public void CheckEnemy(){
        _enemyText.text = $"현재 잡은 마리수{Environment.NewLine}{CurrentEnemyCount} / {EnemyList.Count}";

        if(CurrentEnemyCount == EnemyList.Count){
            _infoText.text = $"와! 모든 적들을 처치했어요!{Environment.NewLine}잠시 뒤 화면이 전환됩니다";

            StartCoroutine(ChangeScene());
        }
    }

    private IEnumerator ChangeScene(){
        for (int i = 3; i >= 1; --i)
        {
            _enemyText.text = $"{i}초 뒤";
            yield return WaitForSeconds(1f);
        }
        KeyInterectionManager.Instance.SetCursorLock(false);
        SceneManager.LoadScene("YouWin");
    }
}
