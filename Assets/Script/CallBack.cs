using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallBack : MonoBehaviour
{
    GameObject _sceneTransObj;
    SceneTransition _sceneTrans;
    // Start is called before the first frame update
    void Start()
    {
        _sceneTransObj = GameObject.Find("SceneTransition");
        _sceneTrans = _sceneTransObj.GetComponent<SceneTransition>();
    }

    public void BackTitle()
    {
        _sceneTrans.LoadSceneTitle();
    }
}
