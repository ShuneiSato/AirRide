using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> _rideMachines = new List<GameObject>();
    [SerializeField] List<GameObject> _spwanObj = new List<GameObject>();
    [SerializeField] List<Vector3> _spawnPosition = new List<Vector3>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i <= _spwanObj.Count; i++)
        {
            _spawnPosition.Add(new Vector3(_spwanObj[i].transform.position.x, _spwanObj[i].transform.position.y, _spwanObj[i].transform.position.z));
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] machineObjects = GameObject.FindGameObjectsWithTag("Machine");

        if (machineObjects.Length < 6)
        {
            int RandomId = Random.Range(0, _rideMachines.Count);
            int RandomPos = Random.Range(0, _spwanObj.Count);
            Instantiate(_rideMachines[RandomId], _spawnPosition[RandomPos], Quaternion.identity);
        }
    }
}
