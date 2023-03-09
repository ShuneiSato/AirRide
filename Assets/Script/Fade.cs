using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("FadeIn");
    }

    public void FadeOutStart()
    {
        StartCoroutine("FadeOut");
    }

    IEnumerator FadeIn()
    {
        Image fade = GetComponent<Image>();

        const float fade_time = 2.0f;
        const int loop_count = 6;

        float wait_time = fade_time / loop_count;
        float alpha_interval = 255.0f / loop_count;

        for (float alpha = 255.0f; alpha >= 0.0f; alpha -= alpha_interval)
        {
            // ‘Ò‚¿ŽžŠÔ
            yield return new WaitForSeconds(wait_time);

            Color new_color = fade.color;
            new_color.a = alpha / 255.0f;
            fade.color = new_color;
        }
    }
    IEnumerator FadeOut()
    {
        Image fade = GetComponent<Image>();

        const float fade_time = 2.0f;
        const int loop_count = 6;

        float wait_time = fade_time / loop_count;
        float alpha_interval = 255.0f / loop_count;

        for (float alpha = 0.0f; alpha <= 255.0f; alpha += alpha_interval)
        {
            // ‘Ò‚¿ŽžŠÔ
            yield return new WaitForSeconds(wait_time);

            Color new_color = fade.color;
            new_color.a = alpha / 255.0f;
            fade.color = new_color;
        }
    }
}
