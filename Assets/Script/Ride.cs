using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ride : MonoBehaviour
{
    [SerializeField] float _speed = 5;
    [SerializeField] Camera _cam;
    [SerializeField] GameObject _playerObj;
    Rigidbody _rb;

    public bool _isRide = false;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_isRide == true)
        {
            RideMove();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == _playerObj)
        {
            _isRide = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject == _playerObj)
        {
            _isRide = false;
        }
    }

    void RideMove()
    {
        float verticalSpd = _speed * Input.GetAxisRaw("Vertical");
        float horizontalSpd = _speed * Input.GetAxisRaw("Horizontal");
        Vector3 camFoward = new Vector3(_cam.transform.forward.x, 0, _cam.transform.forward.z).normalized;
        Vector3 move = camFoward * verticalSpd + _cam.transform.right * horizontalSpd;
        transform.position += move * Time.deltaTime;
    }
}
