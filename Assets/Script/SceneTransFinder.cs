using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransFinder : MonoBehaviour
{
    [SerializeField] Button _testBtn;
    GameObject _sceneTransObj;
    SceneTransition _sceneTrans;
    // Start is called before the first frame update
    void Start()
    {
        _sceneTransObj = GameObject.Find("SceneTransition");
        _sceneTrans = _sceneTransObj.GetComponent<SceneTransition>();
        if (SceneManager.GetActiveScene().name == "Title")
            _testBtn.onClick.AddListener(SceneMoveTryal);
    }

    void SceneMoveTryal()
    {
        _sceneTrans.LoadScenePlay();
    }
}
