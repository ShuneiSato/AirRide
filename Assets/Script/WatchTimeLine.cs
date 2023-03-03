using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class WatchTimeLine : MonoBehaviour
{
    public PlayableDirector director;

    [SerializeField] CanvasGroup _canvasGroup;
    [SerializeField] Canvas _canvas;
    // フェードさせる時間を設定
    [SerializeField]
    [Tooltip("フェードさせる時間(秒)")]
    private float fadeTime = 1f;
    // 経過時間を取得
    private float timer;

    private bool _isFinished;
    // Start is called before the first frame update
    void Start()
    {
        _isFinished = false;
        _canvasGroup.alpha = 0;
        _canvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isFinished == true)
        {
            // 経過時間を加算
            timer += Time.deltaTime;

            _canvas.enabled = true;
            // 経過時間をfadeTimeで割った値をalphaに入れる
            _canvasGroup.alpha = timer / fadeTime;
        }
    }
    void OnEnable()
    {
        director.stopped += OnPlayableDirectorStopped;
    }

    void OnPlayableDirectorStopped(PlayableDirector aDirector)
    {
        if (director == aDirector)
        {
            Debug.Log("PlayableDirector named " + aDirector.name + " is now stopped.");
            _isFinished = true;
        }
    }

    void OnDisable()
    {
        director.stopped -= OnPlayableDirectorStopped;
    }
}
