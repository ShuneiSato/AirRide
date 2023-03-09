using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSE : MonoBehaviour
{
    [SerializeField] AudioClip _pushButton;
    public void PlayButton()
    {
        GameManager.instance.PlaySE(_pushButton);
    }
}
