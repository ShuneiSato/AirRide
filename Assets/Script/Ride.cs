using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ride : MonoBehaviour
{
    [SerializeField] float _speed = 5;
    [SerializeField] float _rotateSpeed = 200;
    [SerializeField] float _limitSpeed = 150;
    [SerializeField] float _currentSpeed;
    [SerializeField] Camera _cam;
    [SerializeField] GameObject _playerObj;
    Rigidbody _rb;
    BoxCollider _col;

    bool _isRide = false;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit rayHit;
        float range = 0.9f;
        Debug.DrawRay(ray.origin, ray.direction, Color.red);
        if(Physics.Raycast(ray,out rayHit,range))
        {
            Debug.Log(rayHit.collider.gameObject.name + rayHit.normal);

            _rb.useGravity = false;
            // 法線ベクトルの取得、角度をベクトルと垂直に
            // localPosition.yを少し上に、浮遊させる
            Vector3 rotation = this.transform.eulerAngles;
            rotation = Quaternion.FromToRotation(transform.up, rayHit.normal).eulerAngles;
            Vector3 p = rayHit.point;
            Vector3 plusFloat = p + new Vector3(0, 0.7f, 0);
            if (_isRide == true)
            {
                RideMove(plusFloat, rotation);
            }
        }
        else
        {
            _rb.useGravity = true;
            if (_isRide == true)
            {
                AirMove();
            }
        }

        if (_rb.velocity.magnitude > _limitSpeed)
        {
            _rb.velocity = new Vector3(_rb.velocity.x / 1.05f, _rb.velocity.y, _rb.velocity.z / 1.05f);
        }
        _currentSpeed = _rb.velocity.magnitude;
        Debug.Log(_currentSpeed);
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

    void RideMove(Vector3 yPosition, Vector3 nVector)
    {
        float verticalSpd = _speed * Input.GetAxisRaw("Vertical");
        float horizontalSpd = _rotateSpeed * Input.GetAxisRaw("Horizontal");
        _rb.AddRelativeForce(Vector3.forward * verticalSpd);
        transform.localPosition = yPosition;
        transform.eulerAngles += new Vector3(0, horizontalSpd * Time.deltaTime , 0) + nVector;
    }

    void AirMove()
    {
        float verticalSpd = _speed * Input.GetAxisRaw("Vertical");
        float horizontalSpd = _rotateSpeed * Input.GetAxisRaw("Horizontal");
        _rb.AddRelativeForce(Vector3.forward * verticalSpd);
        transform.eulerAngles += new Vector3(0, horizontalSpd * Time.deltaTime, 0);
    }
}
