using static Define;
using static Yields;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;

public class UIManager : MonoSingleton<UIManager>
{
    private const string UISCENE = "UIScene";
    private const string CAMNAME = "UICam";
    static AsyncOperation _async = new AsyncOperation();
    private static Camera cam;


    [RuntimeInitializeOnLoadMethod]
    private static void LoadingUIScene()
    {
        if (SceneManager.GetActiveScene().name == UISCENE) return;
        _async = SceneManager.LoadSceneAsync(UISCENE, LoadSceneMode.Additive);
        StartAfterCor(Instance);
    }

    public static void StartAfterCor(MonoBehaviour script)
    {
        script.StartCoroutine(AfterLoadUIScene());
    }

    public static IEnumerator AfterLoadUIScene()
    {
        yield return WaitUntil(() => _async.isDone);
        GameObject[] objs = SceneManager.GetSceneByName(UISCENE).GetRootGameObjects();
        for (int i = 0; i < objs.Length; i++)
        {
            SceneManager.MoveGameObjectToScene(objs[i], SceneManager.GetActiveScene());
        }

        cam = GameObject.Find("UICam").GetComponent<Camera>();
        var cameraData = MainCam.GetUniversalAdditionalCameraData();
        cameraData.cameraStack.Add(cam);
        SceneManager.UnloadSceneAsync(UISCENE);
    }




}
