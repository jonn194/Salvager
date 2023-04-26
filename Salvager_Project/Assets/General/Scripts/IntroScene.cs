using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroScene : MonoBehaviour
{
    public float timer;

    public Image coverImage;
    void Start()
    {
        StartCoroutine(CoverTimer());
    }

    IEnumerator CoverTimer()
    {
        yield return new WaitForSeconds(timer);
        coverImage.gameObject.SetActive(true);
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
