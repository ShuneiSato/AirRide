using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition instance;

    public List<GameObject> _playersData = new List<GameObject>();

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

    public void LoadSceneResult()
    {
        StartCoroutine("LoadSceneResultCoroutine");
    }

    public void LoadSceneTitle()
    {
        StartCoroutine("LoadSceneTitleCoroutine");
    }

    IEnumerator LoadScenePlayCoroutine()
    {
        yield return new WaitForSeconds(3f);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("PlayScene[Tryal]");

        if (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    IEnumerator LoadSceneBattleCoroutine()
    {
        GameObject rideObj = (GameObject)Resources.Load("RideMachines[Aqua]");
        _playersData.Add(GameObject.Find("Player").transform.root.gameObject);
        GameObject[] enemyData = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemyData.Length; i++)
        {
            _playersData.Add(enemyData[i].transform.root.gameObject);
        }
        for (int i = 0; i < _playersData.Count; i++)
        {
            if (_playersData[i].transform.root.gameObject.tag != "Machine")
            {
                GameObject instance = Instantiate(rideObj
                    ,new Vector3(_playersData[i].transform.position.x, _playersData[i].transform.position.y, _playersData[i].transform.position.z)
                    ,Quaternion.identity);
                var insertParent = instance.transform.GetChild(0).gameObject;
                _playersData[i].transform.parent = insertParent.transform;
            }
            DontDestroyOnLoad(_playersData[i].transform.root.gameObject);
        }
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(3f);
        Time.timeScale = 1f;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("PlayScene[Battle]");
    }

    IEnumerator LoadSceneResultCoroutine()
    {
        yield return new WaitForSeconds(3f);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Result");

        if (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    IEnumerator LoadSceneTitleCoroutine()
    {
        yield return new WaitForSeconds(3f);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Title");

        if (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
