using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ride : MonoBehaviour
{
    [SerializeField] float _speed = 5;
    [SerializeField] float _rotateSpeed = 20;
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
        int range = 10;
        Debug.DrawRay(ray.origin, ray.direction, Color.red);
        if(Physics.Raycast(ray,out rayHit,range))
        {
            Debug.Log(rayHit.collider.gameObject.name + rayHit.normal);

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
}
