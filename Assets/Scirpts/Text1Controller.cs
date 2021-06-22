using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Text1Controller : MonoBehaviour
{
    private Image text1Image;
    public GameObject go;

    void Update()
    {
        text1Image = go.gameObject.GetComponent<Image>();
    }

    public IEnumerator ImageOn()
    {
        
        Color a = text1Image.color;
        a.a = 10.0f;
        text1Image.color = a;
        yield return new WaitForSeconds(0.1f);
    }

    public IEnumerator ImageOff()
    {
        Color a = text1Image.color;
        a.a = 0;
        text1Image.color = a;
        yield return new WaitForSeconds(0.1f);
    }



}
