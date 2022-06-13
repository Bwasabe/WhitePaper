using static Define;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstEnter : MonoBehaviour
{
    [SerializeField]
    private GameObject _cam;
    public void OnClickSendMessage(){
        MainCam.gameObject.SetActive(false);
        _cam.SetActive(true);
        gameObject.SendMessage("GameMovementFirstEnter");
    }
    
    private void GameMovementFirstEnter(){
        Debug.Log("ㄱ");
        ObjectManager.Instance.ShowBar(true);
    }
}
