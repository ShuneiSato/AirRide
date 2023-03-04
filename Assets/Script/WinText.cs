using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinText : MonoBehaviour
{
    TextMeshProUGUI _winText;
    [SerializeField] TextMeshProUGUI _1stText;
    [SerializeField] TextMeshProUGUI _2ndText;
    [SerializeField] TextMeshProUGUI _3rdText;
    [SerializeField] TextMeshProUGUI _4thText;
    // Start is called before the first frame update
    void Start()
    {
        _winText = GetComponent<TextMeshProUGUI>();
        List<int> Ranking = new List<int>();
        Ranking.Add(BreakCount.playerCount);
        Ranking.Add(BreakCount.enemyCount);
        Ranking.Add(BreakCount.enemyCount2);
        Ranking.Add(BreakCount.enemyCount3);

        Ranking.Sort();

        _winText.enabled = false;

        _1stText.text = Ranking[0].ToString() + " 1st";
        _2ndText.text = Ranking[1].ToString() + " 2nd";
        _3rdText.text = Ranking[2].ToString() + " 3rd";
        _4thText.text = Ranking[3].ToString() + " 4th";

        if (Ranking[0] == BreakCount.playerCount)
        {
            _winText.text = "Player Win!";
            _winText.enabled = true;
        }
        if (Ranking[0] == BreakCount.enemyCount)
        {
            _winText.text = "Computer1 Win!";
            _winText.enabled = true;
        }
        if (Ranking[0] == BreakCount.enemyCount2)
        {
            _winText.text = "Computer2 Win!";
            _winText.enabled = true;
        }
        if (Ranking[0] == BreakCount.enemyCount3)
        {
            _winText.text = "Computer3 Win!";
            _winText.enabled = true;
        }
    }
}
