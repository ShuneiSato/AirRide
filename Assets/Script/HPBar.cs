using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    Image _hpImage;
    // Start is called before the first frame update
    void Start()
    {
        _hpImage = GetComponent<Image>();
    }

    public void UpdateHpBar(RideStatus status)
    {
        var hp = status._currentHp;
        var maxHp = status._currentHealth;
        _hpImage.fillAmount = hp / (float)maxHp;
    }
}
