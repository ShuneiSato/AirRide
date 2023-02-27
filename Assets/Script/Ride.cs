using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ride : MonoBehaviour
{
    [SerializeField] float _currentSpeed;
    [SerializeField] float _charge;
    [SerializeField] Vector3 _localGravity;
    [SerializeField] Vector3 _flightGravity;
    [SerializeField] GameObject _playerObj;
    [SerializeField] GameObject _meterObj;
    [SerializeField] GameObject _speedObj;
    [SerializeField] ChargeMeter _meter;
    [SerializeField] SpeedMeter _sMeter;
    Vector3 rotation;
    Rigidbody _rb;
    BoxCollider _col;
    RideStatus _status;

    public bool _isRide = false;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<BoxCollider>();
        _status = GetComponent<RideStatus>();
        _playerObj = GameObject.Find("Player");
        _meterObj = GameObject.Find("ChargeMeter_Fill");
        _speedObj = GameObject.Find("SpeedMeter");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit rayHit;
        float range = 1.1f;
        int layerMask = LayerMask.GetMask(new string[] { "HitRayCast" });
        Debug.DrawRay(ray.origin, ray.direction, Color.red);

        // RayCastを下に射出、Hit時に挙動をオリジナル化
        if(Physics.Raycast(ray,out rayHit,range,layerMask))
        {
            Debug.Log(rayHit.collider.gameObject.name + rayHit.normal);

            _rb.useGravity = false;
            // 法線ベクトルの取得、角度をベクトルと垂直に
            // localPosition.yを少し上に、浮遊させる
            rotation = Quaternion.FromToRotation(transform.up, rayHit.normal).eulerAngles;
            Vector3 p = rayHit.point;
            if (Input.GetKey(KeyCode.Mouse0))
            {
                _rb.AddForce(_localGravity * Time.deltaTime, ForceMode.Impulse);
                StopMove();
            }
            else
            {
                Vector3 plusFloat = p + new Vector3(0, 0.9f, 0);
                if (_isRide == true)
                {
                    RideMove(plusFloat, rotation);
                }
            }
        }
        else
        {
            _rb.useGravity = true;
            if (_isRide == true)
            {
                _flightGravity.y = _status._currentFlight;
                AirMove();
                _rb.AddForce(_flightGravity * Time.fixedDeltaTime, ForceMode.Impulse);
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    _rb.AddForce(_localGravity * Time.fixedDeltaTime, ForceMode.Impulse);
                    StopMove();
                }
            }
        }
        // トップスピードに到達したときvelocityを少し減らし、自然に最高速付近へ
        if (_rb.velocity.magnitude > _status._currentTopSpd)
        {
            _rb.velocity = new Vector3(_rb.velocity.x / 1.05f, _rb.velocity.y, _rb.velocity.z / 1.05f);
        }
        _currentSpeed = _rb.velocity.magnitude;
        // UIへの情報伝達
        if (_meter != null)
        {
            _meter.UpdateCharge(_charge);
        }
        if (_sMeter != null)
        {
            _sMeter.SpeedUpdate(_rb.velocity.magnitude * 3.8f);
        }

        // マウスを離した際、chargeを移動量へ変換
        if (Input.GetMouseButtonUp(0))
        {
            if (_isRide == true)
            {
                _rb.AddRelativeForce(Vector3.forward * (_status._currentAcc * _charge * 0.01f), ForceMode.Impulse);
                _charge = 0;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == _playerObj)
        {
            _isRide = true;
            _meter = _meterObj.GetComponent<ChargeMeter>();
            _sMeter = _speedObj.GetComponent<SpeedMeter>();
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject == _playerObj)
        {
            _isRide = false;
        }
    }

    // 通常時(地面付近)の挙動
    void RideMove(Vector3 yPosition, Vector3 nVector)
    {
        float verticalSpd = _status._currentAcc * Input.GetAxisRaw("Vertical");
        float horizontalSpd = _status._currentTurn * Input.GetAxisRaw("Horizontal");
        _rb.AddRelativeForce(Vector3.forward * verticalSpd);
        transform.localPosition = yPosition;
        transform.eulerAngles += new Vector3(0, horizontalSpd * Time.deltaTime , 0) + nVector;
    }
    // 空中時の挙動
    void AirMove()
    {
        float verticalSpd = _status._currentAcc * Input.GetAxisRaw("Vertical");
        float horizontalSpd = _status._currentTurn * Input.GetAxisRaw("Horizontal");
        _rb.AddRelativeForce(Vector3.forward * verticalSpd / 1.44f);
        transform.eulerAngles += new Vector3(0, horizontalSpd * Time.deltaTime, 0);
    }
    // チャージ時(マウス押し)の挙動
    void StopMove()
    {
            float horizontalSpd = _status._currentTurn * Input.GetAxisRaw("Horizontal");
            transform.eulerAngles += new Vector3(0, horizontalSpd * Time.deltaTime, 0);
            _charge += _status._currentCharge * Time.fixedDeltaTime;
            if (_charge >= 100)
            {
                _charge = 100;
            }
    }
}
