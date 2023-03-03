using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    [SerializeField] GameObject _parentObj;
    Ride _ride;
    public BoxCollider _col;
    // Start is called before the first frame update
    void Start()
    {
        _parentObj = transform.root.gameObject;
        _ride = _parentObj.GetComponent<Ride>();
        _col = GetComponent<BoxCollider>();

        _col.enabled = false;
    }
    public void EndAttack()
    {
        _col.enabled = false;
    }
    public void DestroyObj()
    {
        Destroy(_parentObj.gameObject);
    }
}
