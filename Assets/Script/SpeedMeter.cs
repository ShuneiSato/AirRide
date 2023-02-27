using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpeedMeter : MonoBehaviour
{
    TextMeshProUGUI _speedText;
    // Start is called before the first frame update
    void Start()
    {
        _speedText = GetComponent<TextMeshProUGUI>();
    }

    public void SpeedUpdate(float speed)
    {
        _speedText.SetText(" { 0:2 } ", speed);
    }
}
