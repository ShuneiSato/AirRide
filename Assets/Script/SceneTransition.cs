using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition instance;

    void Awake()
    {
        CheckInstance();
    }
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    void CheckInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void LoadScenePlay()
    {
        StartCoroutine("LoadScenePlayCoroutine");
    }

    public void LoadSceneBattle()
    {
        StartCoroutine("LoadSceneBattleCoroutine");
    }

    IEnumerator LoadScenePlayCoroutine()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("PlayScene[Tryal]");

        if (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    IEnumerator LoadSceneBattleCoroutine()
    {
        Time.timeScale = 0.5f;

        yield return new WaitForSeconds(3f);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("PlayScene[Battle]");
    }
}
