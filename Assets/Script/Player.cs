using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float _speed = 5;
    Rigidbody _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayerMove()
    {
        float verticalSpd = _speed * Input.GetAxis("Vertical");
        float horizontalSpd = _speed * Input.GetAxis("Horizontal");
    }
}
