using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{

    private Collider _col;

    public void RegisterCol(Collider col){
        _col = col;
    }
    protected void EventColliderOn(){
        _col.enabled = true;
    }

    protected void EventColliderOff(){
        _col.enabled = false;
        
    }

    
}
