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
        PlayerMove();
        if (Input.GetKeyDown("space"))
        {
            Flight();
        }
    }

    void PlayerMove()
    {
        float verticalSpd = _speed * Input.GetAxis("Vertical");
        float horizontalSpd = _speed * Input.GetAxis("Horizontal");
        Vector3 moveSpeed = new Vector3(verticalSpd, horizontalSpd, 0);
        _rb.velocity = moveSpeed.normalized * _speed;
    }

    void Flight()
    {
        _rb.useGravity = false;
        _rb.AddForce(transform.up * 3f, ForceMode.Impulse);
        return;
    }
}
