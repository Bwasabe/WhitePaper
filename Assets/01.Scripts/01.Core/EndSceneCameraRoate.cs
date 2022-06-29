using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSceneCameraRoate : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed = 0.01f;

    private void LateUpdate() {
        Move();
    }
    
    private void Move(){
        transform.Rotate(Vector3.up*_rotateSpeed);
    }
}
