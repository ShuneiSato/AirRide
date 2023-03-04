using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BreakCount : MonoBehaviour
{
    public static BreakCount instance;

    public static int playerCount;
    public static int enemyCount;
    public static int enemyCount2;
    public static int enemyCount3;

    void Awake()
    {
        CheckInstance();
    }
    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        if (SceneManager.GetActiveScene().name == "PlayScene[Battle]")
        {
            playerCount = 0;
            enemyCount = 0;
            enemyCount2 = 0;
            enemyCount3 = 0;
        }
    }
    void CheckInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Defeat(GameObject defeatObj)
    {
        if (defeatObj.name == "Player")
        {
            playerCount += 1;
        }
        else if (defeatObj.name == "Enemy")
        {
            enemyCount += 1;
        }
        else if (defeatObj.name == "Enemy2")
        {
            enemyCount2 += 1;
        }
        else if (defeatObj.name == "Enemy3")
        {
            enemyCount3 += 1;
        }
    }
}
