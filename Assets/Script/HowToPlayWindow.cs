using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowToPlayWindow : MonoBehaviour
{
    [SerializeField] GameObject _image;
    
    public void WindowOpen()
    {
        _image.SetActive(true);
    }

    public void WindowClose()
    {
        _image.SetActive(false);
    }
}
