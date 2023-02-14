using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float _speed = 5;
    [SerializeField] Camera _cam;
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

        float verticalSpd = _speed * Input.GetAxisRaw("Vertical");
        float horizontalSpd = _speed * Input.GetAxisRaw("Horizontal");
        Vector3 camFoward = new Vector3(_cam.transform.forward.x, 0 , _cam.transform.forward.z).normalized;
        Vector3 move = camFoward * verticalSpd + _cam.transform.right * horizontalSpd;
        transform.position += move * Time.deltaTime;
    }

    void Flight()
    {
        _rb.AddForce(transform.up * 5f, ForceMode.Impulse);
    }
}
