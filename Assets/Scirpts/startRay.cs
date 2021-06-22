using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class startRay : MonoBehaviour
{
    private bool raycheckbool = false;
    private float timeElapsed;
    public Image reticle;

    public GameObject start;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Raycast();
    }

    void Raycast()
    {
        RaycastHit hit;
        Vector3 forward = transform.TransformDirection(Vector3.forward * 1000);
        if (Physics.Raycast(transform.position, forward, out hit))
        {
            if (raycheckbool == true)
            {
                return;
            }
            else if (timeElapsed < 2)
            {

                timeElapsed += Time.deltaTime;
                reticle.fillAmount = timeElapsed / 2f;
            }
            else if (timeElapsed >= 2)
            {
                timeElapsed = 0;
                reticle.fillAmount = 0;
                raycheckbool = true;
                Movement(hit);
                return;
            } // switch »£√‚
            else
            {
                timeElapsed -= Time.deltaTime + 3f;
                if (timeElapsed < 0)
                {
                    timeElapsed = 0;
                }
                reticle.fillAmount = timeElapsed * 0;
            }
        }
    }

    void Movement(RaycastHit hit)
    {
        string hitName = hit.transform.tag;

        switch (hitName)
        {
            case "GameStart":
                SceneManager.LoadScene("GameScene");
                break;
        }
    }
}
