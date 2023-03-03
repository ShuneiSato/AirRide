using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    private NavMeshAgent _agent;
    private NavMeshAgent _childAgent;
    private RideStatus _status;
    private SphereCollider _col;
    private GameObject closeTarget;
    private GameObject[] targets;
    private float searchTime = 0;

    bool _isRide;
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _col = GetComponent<SphereCollider>();
        _agent.destination = this.transform.position;
        _agent.avoidancePriority = Random.Range(0, 100);
        _isRide = false;
    }

    private void Update()
    {
        if(_isRide == false)
        searchTime += Time.fixedDeltaTime;
        if (searchTime >= 1.0f && _isRide == false)
        {
            targets = GameObject.FindGameObjectsWithTag("Machine");
            // 初期値の設定
            float closeDist = 100;
            foreach (GameObject t in targets)
            {
                Debug.Log(Vector3.Distance(transform.position, t.transform.position));

                float tDist = Vector3.Distance(transform.position, t.transform.position);

                if (closeDist > tDist)
                {
                    closeDist = tDist;
                    closeTarget = t;
                }
            }
            searchTime = 0;
        }
        if (_isRide == false)
        {
            _agent.destination = closeTarget.transform.position;
        }
        else if (_isRide)
        {
            _col.enabled = false;
            this.transform.localPosition = new Vector3(0, 0.7f, 0);
            this.transform.localRotation = Quaternion.Euler(0, 0, 0);
            _childAgent.speed = _status._currentTopSpd / 3.2f;
            _childAgent.acceleration = _status._currentAcc / 25f;
            RandomWander();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Machine")
        {
            var insertParent = collision.transform.GetChild(0).gameObject;
            this.transform.parent = insertParent.transform;
            _childAgent = collision.gameObject.GetComponentInChildren<NavMeshAgent>();
            _status = collision.gameObject.GetComponent<RideStatus>();
            _agent.enabled = false;
            _childAgent.enabled = true;
            _isRide = true;
        }
    }
    void RandomWander()
    {
        if (_isRide)
        {
            //指定した目的地に障害物があるかどうか、そもそも到達可能なのかを確認して問題なければセットする。
            if (!_childAgent.pathPending)
            {
                if (_childAgent.remainingDistance <= _childAgent.stoppingDistance)
                {
                    if (!_childAgent.hasPath || _childAgent.velocity.sqrMagnitude == 0f)
                    {
                        _childAgent.destination = GetRandomLocation();
                        Debug.Log(GetRandomLocation());
                    }
                }
            }
        }
    }

    Vector3 GetRandomLocation()
    {
        NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();

        int t = Random.Range(0, navMeshData.indices.Length - 3);

        Vector3 point = Vector3.Lerp(navMeshData.vertices[navMeshData.indices[t]], navMeshData.vertices[navMeshData.indices[t + 1]], Random.value);
        Vector3.Lerp(point, navMeshData.vertices[navMeshData.indices[t + 2]], Random.value);

        return point;
    }
}
