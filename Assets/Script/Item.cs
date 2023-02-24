using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[Serializable]
[CreateAssetMenu(fileName = "Item", menuName = "CreateItem")]
public class Item : ScriptableObject
{
    public int id; // �A�C�e��ID
    public enum Type // �A�C�e���̊�b���
    {
        Acceleration,
        TopSpeed,
        Turning,
        Charge,
        Flight,
        Attack,
        Defense,
        Weight,
        health,

        AccelerationDown,
        TopSpeedDown,
        TurningDown,
        ChargeDown,
        FlightDown,
        AttackDown,
        DefenseDown,
        WeightDown,
        healthDown,

        Heal
    }

    public Type type; // �A�C�e���̎��
    public String infomation; // �A�C�e������
    public float efficacyValue; // �A�C�e���̌��ʗ�
    public Sprite image; // �A�C�e���摜
    /*
    public Item(Item item)
    {
        this.id = item.id;
        this.type = item.type;
        this.image = item.image;
        this.efficacyValue = item.efficacyValue;
    }
    */
}
