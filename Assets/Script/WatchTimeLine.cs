using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class WatchTimeLine : MonoBehaviour
{
    public PlayableDirector director;

    [SerializeField] CanvasGroup _canvasGroup;
    [SerializeField] Canvas _canvas;
    // �t�F�[�h�����鎞�Ԃ�ݒ�
    [SerializeField]
    [Tooltip("�t�F�[�h�����鎞��(�b)")]
    private float fadeTime = 1f;
    // �o�ߎ��Ԃ��擾
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
            // �o�ߎ��Ԃ����Z
            timer += Time.deltaTime;

            _canvas.enabled = true;
            // �o�ߎ��Ԃ�fadeTime�Ŋ������l��alpha�ɓ����
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
