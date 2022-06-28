using static Yields;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCanvas : MonoBehaviour
{
    private Canvas _canvas;
    private IEnumerator Start() {
        _canvas = GetComponent<Canvas>();

        yield return WaitUntil(() => UISceneLoader.Instance.UICam != null);

        _canvas.worldCamera = UISceneLoader.Instance.UICam;
    }
}
