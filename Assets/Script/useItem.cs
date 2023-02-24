using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class useItem : MonoBehaviour
{
    [SerializeField] GameObject _itemPrefub;
    [SerializeField] InputItemData data;
    [SerializeField] ItemDataBase _dataBase;
    Sprite sprite;

    // Start is called before the first frame update
    void Start()
    {
        int Rondomid = Random.Range(0, 18);
        int RandomX = Random.Range(-145, 110);
        int RandomY = Random.Range(2, 10);
        int RandomZ = Random.Range(-135, 130);

        GameObject newItem = Instantiate(_itemPrefub, new Vector3(RandomX, RandomY, RandomZ), Quaternion.identity);
        data = newItem.GetComponent<InputItemData>();
        data._itemData = _dataBase.items[Rondomid];

        var sprite = newItem.GetComponent<SpriteRenderer>();
        sprite.sprite = data._itemData.image;
        Debug.Log(data._itemData);
    }
}
