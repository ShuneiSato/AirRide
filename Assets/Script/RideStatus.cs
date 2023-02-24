using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RideStatus : MonoBehaviour
{
    // ���������X�e�[�^�X
    [SerializeField, Range(1, 36)] int _acceleration = 18; // ����
    int maxAcceleration = 36;
    [SerializeField, Range(1, 36)] int _topSpeed = 18; // �ō���
    int maxTopSpeed = 36;
    [SerializeField, Range(1, 36)] int _turning = 18; // ����
    int maxTurning = 36;
    [SerializeField, Range(1, 36)] int _charge = 18; // �`���[�W��
    int maxCharge = 36;
    [SerializeField, Range(1, 36)] int _flight = 18; // ��s
    int maxFlight = 36;
    [SerializeField, Range(1, 36)] int _attack = 18; // �U����
    int maxAttack = 36;
    [SerializeField, Range(1, 36)] int _defense = 18; // �h���
    int maxDefense = 36;
    [SerializeField, Range(1, 36)] int _weight = 18; // �d��
    int maxWeight = 36;
    [SerializeField, Range(1, 32)] int _health = 16; // �̗�
    int maxHealth = 32;

    // ���C�h�}�V����b�X�e�[�^�X
    [SerializeField] int _rideAcceleration = 5;
    [SerializeField] int _rideTopSpeed = 10;
    [SerializeField] int _rideTurning = 100;
    [SerializeField] float _rideCharge = 1;
    [SerializeField] float _rideFlight = 1;
    [SerializeField] int _rideAattack = 1;
    [SerializeField] int _rideDefense = 1;
    [SerializeField] int _rideWeight = 1;
    [SerializeField] int _rideHealth = 1;

    // ���v�X�e�[�^�X
    public float _currentAcc;
    public float _currentTopSpd ;
    public float _currentTurn;
    public float _currentCharge;
    public float _currentFlight;
    public float _currentAtk;
    public float _currentDef;
    public float _currentWeight;
    public float _currentHealth;

    private void Update()
    {
        float highSpd = maxTopSpeed + _rideTopSpeed;
        float nowSpd = _topSpeed + _rideTopSpeed;
        float highFlight = maxFlight + _rideFlight;
        float nowFlight = _flight + _rideFlight;
        _currentAcc = Mathf.Pow(_acceleration / 1.22f, _rideAcceleration / 4f) / 2.5f;
        if(_currentAcc > 100f)
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
    }
}
