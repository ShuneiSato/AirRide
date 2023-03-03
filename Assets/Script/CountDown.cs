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

    GameObject _sceneTransObj;
    [SerializeField] SceneTransition _sceneTrans;
    [SerializeField] Image _finishImage;

    private void Start()
    {
        timeText = GetComponent<TextMeshProUGUI>();
        _sceneTransObj = GameObject.Find("SceneTransition");
        if (_sceneTransObj != null)
            _sceneTrans = _sceneTransObj.GetComponent<SceneTransition>();

        countdownSeconds = countdownMinutes * 60;
        _finishImage.enabled = false;
    }

    void Update()
    {
        countdownSeconds -= Time.deltaTime;
        var span = new TimeSpan(0, 0, (int)countdownSeconds);
        timeText.text = span.ToString(@"mm\:ss");

        if (countdownSeconds <= 0)
        {
            // 0�b�ɂȂ����Ƃ��̏���
            timeText.enabled = false;
            _finishImage.enabled = true;
            _sceneTrans.LoadSceneBattle();
        }
    }
}