using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemaChineFollow : MonoBehaviour
{
    private CinemachineVirtualCamera _virtualCamera;
    private GameObject _player;
    // Start is called before the first frame update
    void Start()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        if (_player == null)
        {
            _player = GameObject.Find("Player");
            if (_player != null)
            {
                _virtualCamera.Follow = _player.transform;
                _virtualCamera.LookAt = _player.transform;
            }
        }
    }
}
