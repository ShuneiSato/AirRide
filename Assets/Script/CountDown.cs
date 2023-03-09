using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class CountDown : MonoBehaviour
{
    public int countdownMinutes = 3;
    private float countdownSeconds;
    private TextMeshProUGUI timeText;
    private bool _sceneLoaded;

    GameObject _sceneTransObj;
    GameObject _fadeObj;
    [SerializeField] SceneTransition _sceneTrans;
    [SerializeField] Fade _fade;
    [SerializeField] Image _finishImage;
    [SerializeField] AudioClip _finishSE;

    private void Start()
    {
        timeText = GetComponent<TextMeshProUGUI>();
        _sceneTransObj = GameObject.Find("SceneTransition");
        _fadeObj = GameObject.Find("Fade");
        if (_fadeObj != null)
            _fade = _fadeObj.GetComponent<Fade>();
        if (_sceneTransObj != null)
            _sceneTrans = _sceneTransObj.GetComponent<SceneTransition>();

        countdownSeconds = countdownMinutes * 60;
        _sceneLoaded = false;
        _finishImage.enabled = false;
    }

    void Update()
    {
        countdownSeconds -= Time.deltaTime;
        var span = new TimeSpan(0, 0, (int)countdownSeconds);
        timeText.text = span.ToString(@"mm\:ss");

        if (countdownSeconds <= 0)
        {
            // 0•b‚É‚È‚Á‚½‚Æ‚«‚Ìˆ—
            timeText.enabled = false;
            _finishImage.enabled = true;
            GameManager.instance.PlaySE(_finishSE);
            if (SceneManager.GetActiveScene().name == "PlayScene[Tryal]" && _sceneLoaded == false)
            {
                _sceneLoaded = true;
                _fade.FadeOutStart();
                _sceneTrans.LoadSceneResult();
            }
            if (SceneManager.GetActiveScene().name == "PlayScene[Battle]" && _sceneLoaded == false)
            {
                _sceneLoaded = true;
                _fade.FadeOutStart();
                _sceneTrans.LoadSceneResult();
            }
        }
    }
}
