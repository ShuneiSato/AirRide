using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BattleSpawn : MonoBehaviour
{
    [SerializeField] List<Vector3> _spawnPoint = new List<Vector3>();
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] spawn = GameObject.FindGameObjectsWithTag("SpawnPoint");
        GameObject[] machines = GameObject.FindGameObjectsWithTag("Machine");
        for (int i = 0; i < spawn.Length; i++)
        {
            _spawnPoint.Add(spawn[i].transform.position);
        }
        for (int i = 0; i < machines.Length; i++)
        {
            int RandomPos = Random.Range(0, _spawnPoint.Count);
            var child = machines[i].transform.GetChild(0).gameObject;
            var grandChild = child.transform.GetChild(1).gameObject;
            var agent = machines[i].GetComponent<NavMeshAgent>();
            agent.enabled = false;
            machines[i].gameObject.transform.position = new Vector3(_spawnPoint[RandomPos].x, _spawnPoint[RandomPos].y, _spawnPoint[RandomPos].z);
            if (grandChild.gameObject.tag == "Enemy")
                agent.enabled = true;
        }
    }

    public void ResetMachine(GameObject ObjName)
    {
        StartCoroutine(Respawn(ObjName));
    }

    IEnumerator Respawn(GameObject respawnObj)
    {
        yield return new WaitForSeconds(2.2f);
        int RandomPos = Random.Range(0, _spawnPoint.Count);
        respawnObj.transform.position = _spawnPoint[RandomPos];
    }
}
