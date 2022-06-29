using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    private const string NEWGAMESCENE = "NewGameScene";

    
    public void OnClickStartBtn(){
        SceneManager.LoadScene(NEWGAMESCENE);
    }

    public void QuitBtn(){
        Application.Quit();
    }
}
