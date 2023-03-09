using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Ride : MonoBehaviour
{
    [SerializeField] float _currentSpeed;
    [SerializeField] float _charge;
    [SerializeField] Vector3 _localGravity;
    [SerializeField] Vector3 _flightGravity;
    [SerializeField] GameObject _playerObj;
    [SerializeField] GameObject _meterObj;
    [SerializeField] GameObject _speedObj;
    [SerializeField] GameObject _breakCountObj;
    [SerializeField] GameObject _AttackObj;
    [SerializeField] ChargeMeter _meter;
    [SerializeField] SpeedMeter _sMeter;
    [SerializeField] AudioClip _rideSE;
    [SerializeField] AudioClip _attackSE;
    Vector3 rotation;
    Rigidbody _rb;
    NavMeshAgent _agent;
    Animator _anim;
    RideStatus _status;
    BattleSpawn _battleSpawn;
    Player _player;
    BreakCount _breakCount;
    AnimationEvent _animationEvent;

    public bool _isRide = false;
    public bool _isCpuRide = false;
    bool _getHit = false;
    // Start is called before the first frame update
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _agent = GetComponent<NavMeshAgent>();
        _anim = GetComponentInChildren<Animator>();
        _status = GetComponent<RideStatus>();
        _animationEvent = GetComponentInChildren<AnimationEvent>();
        _playerObj = GameObject.Find("Player");
        _meterObj = GameObject.Find("ChargeMeter_Fill");
        _speedObj = GameObject.Find("SpeedMeter");
        SceneManager.sceneLoaded += OnSceneLoaded;

        _battleSpawn = GetComponent<BattleSpawn>();
        _breakCountObj = GameObject.Find("BreakCount");
        _breakCount = _breakCountObj.GetComponent<BreakCount>();

        _agent.enabled = false;
    }
    void OnEnable()
    {
        _playerObj = GameObject.Find("Player");
        _meterObj = GameObject.Find("ChargeMeter_Fill");
        _speedObj = GameObject.Find("SpeedMeter");

        _battleSpawn = GetComponent<BattleSpawn>();
        _breakCountObj = GameObject.Find("BreakCount");
        _breakCount = _breakCountObj.GetComponent<BreakCount>();

        _agent.enabled = false;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        Debug.Log(scene.name);
        StartCoroutine("Wait");
            
        if (SceneManager.GetActiveScene().name == "PlayScene[Battle]")
            SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());

        this.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit rayHit;
        float range = 1.2f;
        int layerMask = LayerMask.GetMask(new string[] { "HitRayCast" });
        //Debug.DrawRay(ray.origin, ray.direction, Color.red);

        // RayCast�����Ɏˏo�AHit���ɋ������I���W�i����
        if(Physics.Raycast(ray,out rayHit,range,layerMask))
        {
            //�@�����F�pRay
            Ray nomalRay = new Ray(rayHit.point, rayHit.normal);
            Debug.DrawRay(nomalRay.origin, nomalRay.direction, Color.red);

            _rb.useGravity = false;
            // �@���x�N�g���̎擾�A�x�N�g���Ɛ�����
            // localPosition.y��������ɁA���V������
            rotation = Vector3.Cross(rayHit.normal,Vector3.left);
            Debug.DrawRay(ray.origin, rotation, Color.red);
            Vector3 p = rayHit.point;
            if (Input.GetKey(KeyCode.Mouse0))
            {
                if (_isRide == true)
                {
                    _rb.AddForce(_localGravity * Time.deltaTime, ForceMode.Impulse);
                    StopMove();
                }
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
                if (Input.GetMouseButton(0))
                {
                    _rb.AddForce(_localGravity * Time.fixedDeltaTime, ForceMode.Impulse);
                    StopMove();
                }
            }
        }
        // �g�b�v�X�s�[�h�ɓ��B�����Ƃ�velocity���������炵�A���R�ɍō����t�߂�
        if (_rb.velocity.magnitude > _status._currentTopSpd)
        {
            _rb.velocity = new Vector3(_rb.velocity.x / 1.05f, _rb.velocity.y, _rb.velocity.z / 1.05f);
        }
        _currentSpeed = _rb.velocity.magnitude;
        _anim.SetFloat("Speed", _currentSpeed);
        // UI�ւ̏��`�B
        if (_meter != null)
        {
            _meter.UpdateCharge(_charge);
        }
        if (_sMeter != null)
        {
            _sMeter.SpeedUpdate(_rb.velocity.magnitude * 3.8f);
        }

        // �}�E�X�𗣂����ہAcharge���ړ��ʂ֕ϊ�
        if (Input.GetMouseButtonUp(0))
        {
            if (_isRide == true)
            {
                _rb.AddRelativeForce(Vector3.forward * (_status._currentAcc * _charge * 0.01f), ForceMode.Impulse);
                _charge = 0;
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (_isRide == true)
            {
                _animationEvent._col.enabled = true;
                _anim.SetTrigger("Attack");
            }
        }
        if (Input.GetKeyDown("space"))
        {
            if(_isRide == true)
            {
                if (SceneManager.GetActiveScene().name == "PlayScene[Tryal]")
                    _isRide = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == _playerObj)
        {
            GameManager.instance.PlaySE(_rideSE);
            _isRide = true;
            _meter = _meterObj.GetComponent<ChargeMeter>();
            _sMeter = _speedObj.GetComponent<SpeedMeter>();
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            _isCpuRide = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject == _playerObj)
        {
            _isRide = false;
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            _isCpuRide = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("AttackPoint"))
        {
            Debug.Log("�U�����󂯂�");
            if (_getHit == false)
            {
                StartCoroutine("hit");
                GameManager.instance.PlaySE(_attackSE);
                _animationEvent.EndAttack();
                GetDamage(other.gameObject.GetComponentInParent<RideStatus>());
                var child = other.transform.GetChild(1).gameObject;
                if (child != null)
                    _AttackObj = child;
            }
        }
    }

    // �ʏ펞(�n�ʕt��)�̋���
    void RideMove(Vector3 yPosition, Vector3 nVector)
    {
        float verticalSpd = _status._currentAcc * Input.GetAxisRaw("Vertical");
        float horizontalSpd = _status._currentTurn * Input.GetAxisRaw("Horizontal");
        _rb.AddRelativeForce(rotation * verticalSpd);
        transform.localPosition = yPosition;
        transform.eulerAngles += new Vector3(0, horizontalSpd * Time.deltaTime , 0);
    }
    // �󒆎��̋���
    void AirMove()
    {
        float verticalSpd = _status._currentAcc * Input.GetAxisRaw("Vertical");
        float horizontalSpd = _status._currentTurn * Input.GetAxisRaw("Horizontal");
        _rb.AddRelativeForce(Vector3.forward * verticalSpd / 1.44f);
        transform.eulerAngles += new Vector3(0, horizontalSpd * Time.deltaTime, 0);
    }
    // �`���[�W��(�}�E�X����)�̋���
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

    public void GetDamage(RideStatus status)
    {
        _anim.SetTrigger("Damage");
        int damage = (int)((status._currentAtk * 2) - (this._status._currentDef / 4));
        if (damage <= 0)
            damage = 1;
        this._status._currentHp -= damage;
    }
    public void Death()
    {
        _anim.SetTrigger("Death");
        if (SceneManager.GetActiveScene().name == "PlayScene[Tryal]")
        {
            _player._isRide = false;
            _playerObj.transform.parent = null;
            StartCoroutine("DeathMotion");
        }
        if (SceneManager.GetActiveScene().name == "PlayScene[Battle]")
        {
            _battleSpawn.ResetMachine(this.gameObject);
            StartCoroutine("ResetMotion");
        }
    }

    IEnumerator hit()
    {
        _getHit = true;
        yield return new WaitForSeconds(3f);
        if (_getHit)
            _getHit = false;
    }
    IEnumerator DeathMotion()
    {
        if (_AttackObj != null)
            _breakCount.Defeat(_AttackObj);
        yield return new WaitForSeconds(1.9f);

        Destroy(this.gameObject);
    }
    IEnumerator ResetMotion()
    {
        if (_AttackObj != null)
            _breakCount.Defeat(_AttackObj);
        yield return new WaitForSeconds(2.2f);

        _status._currentHp = _status._currentHealth;
        _anim.Play("RideStop", 0, 0);
    }
    IEnumerator Wait()
    {
        this.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);
    }
}
