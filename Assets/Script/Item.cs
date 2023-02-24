using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[Serializable]
[CreateAssetMenu(fileName = "Item", menuName = "CreateItem")]
public class Item : ScriptableObject
{
    public int id; // アイテムID
    public enum Type // アイテムの基礎情報
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

    public Type type; // アイテムの種類
    public String infomation; // アイテム説明
    public float efficacyValue; // アイテムの効果量
    public Sprite image; // アイテム画像
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
