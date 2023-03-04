using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RideStatus : MonoBehaviour
{
    // 増減されるステータス
    [SerializeField, Range(1, 36)] public int _acceleration = 18; // 加速
    int maxAcceleration = 36;
    [SerializeField, Range(1, 36)] public int _topSpeed = 18; // 最高速
    int maxTopSpeed = 36;
    [SerializeField, Range(1, 36)] public int _turning = 18; // 旋回
    int maxTurning = 36;
    [SerializeField, Range(1, 36)] public int _charge = 18; // チャージ力
    int maxCharge = 36;
    [SerializeField, Range(1, 36)] public int _flight = 18; // 飛行
    int maxFlight = 36;
    [SerializeField, Range(1, 36)] public int _attack = 18; // 攻撃力
    int maxAttack = 36;
    [SerializeField, Range(1, 36)] public int _defense = 18; // 防御力
    int maxDefense = 36;
    [SerializeField, Range(1, 36)] public int _weight = 18; // 重さ
    int maxWeight = 36;
    [SerializeField, Range(1, 32)] int _health = 16; // 体力
    int maxHealth = 32;

    // ライドマシン基礎ステータス
    [SerializeField] int _rideAcceleration = 5;
    [SerializeField] int _rideTopSpeed = 10;
    [SerializeField] int _rideTurning = 100;
    [SerializeField] float _rideCharge = 1;
    [SerializeField] float _rideFlight = 1;
    [SerializeField] int _rideAattack = 1;
    [SerializeField] int _rideDefense = 1;
    [SerializeField] int _rideWeight = 1;
    [SerializeField] int _rideHealth = 1;

    // 合計ステータス
    public float _currentAcc;
    public float _currentTopSpd ;
    public float _currentTurn;
    public float _currentCharge;
    public float _currentFlight;
    public float _currentAtk;
    public float _currentDef;
    public float _currentWeight;
    public int _currentHealth;

    public int _currentHp;

    [SerializeField] Ride _ride;
    [SerializeField] GameObject _playerObj;
    [SerializeField] HPBar _hPoint;
    [SerializeField] GameObject _hpBar;
    bool _rideSetUp = false;
    bool _isDead;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        _playerObj = GameObject.Find("Player");
        _hpBar = GameObject.Find("Fill");

        _isDead = false;
    }
    void OnEnable()
    {
        _playerObj = GameObject.Find("Player");
        _hpBar = GameObject.Find("Fill");

        _isDead = false;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {

    }

    private void Update()
    {
        if (_ride != null)
        {
            if (_ride._isRide == true || _ride._isCpuRide == true)
            {
                float highSpd = maxTopSpeed + _rideTopSpeed;
                float nowSpd = _topSpeed + _rideTopSpeed;
                float highFlight = maxFlight + _rideFlight;
                float nowFlight = _flight + _rideFlight;
                _currentAcc = Mathf.Pow(_acceleration / 1.22f, _rideAcceleration / 4f) / 2.5f;
                if (_currentAcc > 100f)
                {
                    _currentAcc = 100f;
                }
                if (_acceleration > maxAcceleration)
                    _acceleration = maxAcceleration;

                _currentTopSpd = (nowSpd / highSpd) * 55.5f;
                if (_topSpeed > maxTopSpeed)
                    _topSpeed = maxTopSpeed;

                _currentTurn = (_turning * 5) + _rideTurning;
                if (_turning > maxTurning)
                    _turning = maxTurning;

                _currentCharge = (_charge * 10) * _rideCharge / 2;
                if (_charge > maxCharge)
                    _charge = maxCharge;

                _currentFlight = (1 - (nowFlight / highFlight)) * -9.81f;
                if (_flight > maxFlight)
                    _flight = maxFlight;

                _currentAtk = _attack + _rideAattack;
                if (_attack > maxAttack)
                    _attack = maxAttack;

                _currentDef = (_defense + _rideDefense) * 2;
                if (_defense > maxDefense)
                    _defense = maxDefense;

                _currentWeight = _weight * 2 + _rideWeight;
                if (_weight > maxWeight)
                    _weight = maxWeight;

                _currentHealth = _health * 10 + _rideHealth;
                if (_health > maxHealth)
                    _health = maxHealth;

                // HPBarに現在HPと最大HPを渡す
                if (_ride._isRide == true)
                    _hPoint.UpdateHpBar(this);

                if (_currentHp <= 0 && _isDead == false)
                {
                    _isDead = true;
                    _ride.Death();
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == _playerObj)
        {
            _ride = GetComponent<Ride>();
            _hPoint = _hpBar.GetComponent<HPBar>();
            if (_rideSetUp == false)
            {
                _currentHealth = _health * 10 + _rideHealth;
                _currentHp = _currentHealth;
                _rideSetUp = true;
            }
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            _ride = GetComponent<Ride>();
            if (_rideSetUp == false)
            {
                _currentHealth = _health * 10 + _rideHealth;
                _currentHp = _currentHealth;
                _rideSetUp = true;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Item")
        {
            var item = other.gameObject.GetComponent<InputItemData>();
            var itemData = item._itemData;
            var itemType = itemData.type;
            var itemValue = itemData.efficacyValue;

            if (itemType == Item.Type.Acceleration)
            {
                _acceleration += itemValue;
            }
            if (itemType == Item.Type.TopSpeed)
            {
                _topSpeed += itemValue;
            }
            if (itemType == Item.Type.Turning)
            {
                _turning += itemValue;
            }
            if (itemType == Item.Type.Charge)
            {
                _charge += itemValue;
            }
            if (itemType == Item.Type.Flight)
            {
                _flight += itemValue;
            }
            if (itemType == Item.Type.Attack)
            {
                _attack += itemValue;
            }
            if (itemType == Item.Type.Defense)
            {
                _defense += itemValue;
            }
            if (itemType == Item.Type.Weight)
            {
                _weight += itemValue;
            }
            if (itemType == Item.Type.health)
            {
                _health += itemValue;
                _currentHp += itemValue * 10;
            }

            Destroy(other.gameObject);
        }
    }
}
