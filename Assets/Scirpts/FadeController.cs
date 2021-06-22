using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FadeController : MonoBehaviour
{
    private Image fadeImage;
    private Image fadeImage1;
    private Image fadeImage2;
    private Image fadeImage3;
    public GameObject go;
    public GameObject go2;
    public GameObject go3;
    public GameObject go4;


    void Update()
    {
        fadeImage = go.gameObject.GetComponent<Image>();
        fadeImage1 = go2.gameObject.GetComponent<Image>();
        fadeImage2 = go3.gameObject.GetComponent<Image>();
        fadeImage3 = go4.gameObject.GetComponent<Image>();
    }

    public IEnumerator FadeIn()
    {
        for (int i = 0; i < 10; i++)
        {
            float f = i / 10.0f;
            Color c = fadeImage.color;
            c.a = f;
            fadeImage.color = c;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public IEnumerator FadeOut()
    {
        for (int i = 10; i >= 0; i--)
        {
            float f = i / 10.0f;
            Color c = fadeImage.color;
            c.a = f;
            fadeImage.color = c;
            yield return new WaitForSeconds(0.1f);
        }

    }
    public IEnumerator FadeIn1()
    {
        for (int i = 0; i < 10; i++)
        {
            float f = i / 2.0f;
            Color c = fadeImage1.color;
            c.a = f;
            fadeImage1.color = c;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public IEnumerator FadeOut1()
    {
        for (int i = 10; i >= 0; i--)
        {
            float f = i / 2.0f;
            Color c = fadeImage1.color;
            c.a = f;
            fadeImage1.color = c;
            yield return new WaitForSeconds(0.1f);
        }

    }
    public IEnumerator FadeIn2()
    {
        for (int i = 0; i < 10; i++)
        {
            float f = i / 2.0f;
            Color c = fadeImage2.color;
            c.a = f;
            fadeImage2.color = c;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public IEnumerator FadeOut2()
    {
        for (int i = 10; i >= 0; i--)
        {
            float f = i / 2.0f;
            Color c = fadeImage2.color;
            c.a = f;
            fadeImage2.color = c;
            yield return new WaitForSeconds(0.1f);
        }

    }
    public IEnumerator FadeIn3()
    {
        for (int i = 0; i < 10; i++)
        {
            float f = i / 2.0f;
            Color c = fadeImage3.color;
            c.a = f;
            fadeImage3.color = c;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public IEnumerator FadeOut3()
    {
        for (int i = 10; i >= 0; i--)
        {
            float f = i / 2.0f;
            Color c = fadeImage3.color;
            c.a = f;
            fadeImage3.color = c;
            yield return new WaitForSeconds(0.1f);
        }

    }

}
