using System.Collections;
using UnityEngine;

public class InitializedObject : MonoBehaviour
{
    private IEnumerator Start(){
        yield return null;

        gameObject.SetActive(false);
    }
}
