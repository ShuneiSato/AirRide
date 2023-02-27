using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeMeter : MonoBehaviour
{
    Image _chargeImage;
    // Start is called before the first frame update
    void Start()
    {
        _chargeImage = GetComponent<Image>();
    }

    public void UpdateCharge(float charge)
    {
        _chargeImage.fillAmount = charge / 100f;
    }
}
