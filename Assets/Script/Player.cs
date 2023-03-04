using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] float _speed = 5;
    [SerializeField] Camera _cam;
    [SerializeField] GameObject _player;
    Rigidbody _rb;
    SphereCollider _col;
    Scene _nowScene;

    public bool _isRide = false;
    // Start is called before the first frame update
    void Start()
    {
        _cam = Camera.main;
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_isRide == false)
        {
            PlayerMove();
        }
        if(_isRide == true)
        {
            _col.enabled = false;
            this.transform.localPosition = new Vector3(0, 0.7f, 0);
            this.transform.localRotation = Quaternion.Euler(0, 0, 0);

            if (Input.GetKeyDown("space"))
            {
                if (SceneManager.GetActiveScene().name == "PlayScene[Tryal]")
                    StartCoroutine("secession");
            }
        }
        
        if (Input.GetKeyDown("space"))
        {
            Flight();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Machine")
        {
            var insertParent = collision.transform.GetChild(0).gameObject;
            this.transform.parent = insertParent.transform;
            _isRide = true;
        }
    }

    public void PlayerMove()
    {

        float verticalSpd = _speed * Input.GetAxisRaw("Vertical");
        float horizontalSpd = _speed * Input.GetAxisRaw("Horizontal");
        Vector3 camFoward = new Vector3(_cam.transform.forward.x, 0 , _cam.transform.forward.z).normalized;
        Vector3 move = camFoward * verticalSpd + _cam.transform.right * horizontalSpd;
        transform.position += move * Time.deltaTime;
    }

    void Flight()
    {
        _rb.AddForce(transform.up * 8f, ForceMode.Impulse);
    }

    IEnumerator secession()
    {
        Debug.Log("’Ê‚Á‚½");
        _player.transform.parent = null;
        Flight();
        _isRide = false;

        yield return new WaitForSeconds(0.7f);
        _col.enabled = true;
    }
}
