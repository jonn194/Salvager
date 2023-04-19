using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScene : MonoBehaviour
{
    public float timer;
    void Start()
    {
        StartCoroutine(ChangeTimer());
    }

    IEnumerator ChangeTimer()
    {
        yield return new WaitForSeconds(timer);
        StartCoroutine(ChangeScene());
    }

    IEnumerator ChangeScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);

        while(!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
