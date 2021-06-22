using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System;

public class RayCasting : MonoBehaviour
{
    public Text A;
    public Text B;
    public BoxCollider firstQuiz;
    public AudioSource BackGround;
    public AudioSource EndMusic;
    public AudioClip audioDrawer;
    public AudioClip audioDoorOpen;
    public AudioClip audioWalk;
    public AudioClip audioPaper;
    public AudioClip audioPaperThrow;
    public AudioClip audioKeyON;
    public AudioClip audioStartNoKey;
    public AudioClip audioQuiz1Change;
    public AudioClip audioButton;
    public AudioClip audioLightChange;
    public AudioClip audioLightSwitch;
    public AudioClip audioDestroy;
    public AudioClip audioBeam;
    public AudioClip audioLaugh;
    public AudioClip audioPianoCrash;
    public AudioClip audioCrash;
    public AudioClip audioPen;
    AudioSource audioSource;
    Text1Controller Text1Cotroller;
    FadeController FadeController;
    public Light firstQuizLight;
    public Light[] buttonRGBLight;
    public Light[] mainRGBLight;
    public Light[] mainLight;
    public Image imgStartRoomKey;
    public Image imgFirstQuizKey;
    public Image[] imgRGBLight;
    public Image reticle;
    private TextMesh firstWallText;
    private TextMesh secondWallText;
    private TextMesh thirdWallText;
    private TextMesh endText;
    public TextMesh clearTimeText;
    public TextMesh[] PwdButtonText;
    private Animator aniFirstDoor;
    private Animator aniStartDrawer;
    private Animator aniQuiz1Drawer;
    private Animator aniQuiz2Drawer;
    private Animator aniEndDoor;
    private Animator aniEndBeam;
    public Transform[] pos;
    public GameObject endText1;
    public GameObject End;
    public GameObject startArrow;
    public GameObject Quiz2Arrow;
    public GameObject Quiz2Arrow2;
    public GameObject Quiz3Arrow;
    public GameObject Quiz3Arrow2;
    public GameObject Quiz3Arrow3;
    public GameObject endQuad;
    public GameObject firstKey;
    public GameObject secondKey;
    public GameObject page;
    public GameObject CamOffset;
    public GameObject scareimg;
    public GameObject scareimg2;
    public GameObject scareimg3;
    public GameObject[] tel;
    public GameObject[] txtImg;
    public GameObject[] lockCheck;
    public GameObject[] pwd;
    public GameObject[] quiz2Object;
    public Vector3 POSV;
    public bool timerOn = true;
    public float totalTime = 0f;
    private int minute = 0;
    private int second = 0;
    private bool isInStartRoom = true;
    private bool isInQuiz1 = false;
    private bool isInQuiz2 = false;
    private bool isInQuiz3 = false;
    private bool isFirstDoorKey = false;
    private bool isFirstQuizKey = false;
    private bool isposiCheck2 = false;
    private bool isfirstPassword = false;
    private bool isSecondPassword = false;
    private bool isThirdPassword = false;
    private bool isSecondQuizKey = false;
    private bool isFinalQuizKey = false;
    private bool isEndLocation = false;
    private bool isCubeOn = false;
    private bool isLightCheck = false;
    private bool isFirstLocation = false;
    private new string light = "";
    private float timeElapsed;
    bool isPlaying = false;
    private bool raycheckbool = false;
    enum enumColor {Blue, Purple, Yellow, Green};
    enumColor myColor = enumColor.Blue;
    private List<int> Quiz1Num = new List<int>() { 1, 2, 3, 4, 5, 6, 7 };
    private List<int> Quiz1PwdNum = new List<int>();
    private List<int> Quiz1ClearNum = new List<int>() { 4, 1, 7, 2, 6, 3, 5 };

     void Start()
    {        
        firstQuizLight = GameObject.FindWithTag("Light1").GetComponent<Light>();
        firstWallText = GameObject.Find("firstWallText").GetComponent<TextMesh>();
        secondWallText = GameObject.Find("secondWallText").GetComponent<TextMesh>();
        endText = GameObject.Find("End_Text").GetComponent<TextMesh>();
        thirdWallText = GameObject.Find("final_text").GetComponent<TextMesh>();
        Text1Cotroller = FindObjectOfType<Text1Controller>();
        FadeController = FindObjectOfType<FadeController>();
        aniFirstDoor = GameObject.FindWithTag("firstDoor").GetComponent<Animator>();
        aniStartDrawer = GameObject.FindWithTag("StartDrawer").GetComponent<Animator>();
        aniQuiz1Drawer = GameObject.FindWithTag("FirstQuiz_Drawer").GetComponent<Animator>();
        aniQuiz2Drawer = GameObject.FindWithTag("SecondQuiz_Drawer").GetComponent<Animator>();
        aniEndDoor = GameObject.FindWithTag("EndDoor").GetComponent<Animator>();
        aniEndBeam = GameObject.FindWithTag("EndLight").GetComponent<Animator>();
    }
    void Awake()
    {
        this.audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Raycast();
        if(timerOn)
        {
            totalTime += Time.deltaTime;
        }
        A.text = TimerCalc();
        B.text = TimerRes();
    }

    private string TimerCalc()
    {
        second = (int)totalTime % 60;

        minute = (int)totalTime / 60;

        return minute + " : " + second;
    }
    private string TimerRes()
    {
        second = (int)totalTime % 60;

        minute = (int)totalTime / 60;

        return "ClearTime " + minute + "분 " + second + "초";
    }

    void PlaySound(String action)
    {
        switch (action)
        {
            case "DRAWER":
                audioSource.clip = audioDrawer;
                break;
            case "DOOROPEN":
                audioSource.clip = audioDoorOpen;
                break;
            case "WALK":
                audioSource.clip = audioWalk;
                break;
            case "PAPER":
                audioSource.clip = audioPaper;
                break;
            case "PAPERTHROW":
                audioSource.clip = audioPaperThrow;
                break;
            case "KEYON":
                audioSource.clip = audioKeyON;
                break;
            case "NOKEY":
                audioSource.clip = audioStartNoKey;
                break;
            case "QUIZ1":
                audioSource.clip = audioQuiz1Change;
                break;
            case "BUTTON":
                audioSource.clip = audioButton;
                break;
            case "LIGHTCHANGE":
                audioSource.clip = audioLightChange;
                break;
            case "SWITCH":
                audioSource.clip = audioLightSwitch;
                break;
            case "DESTROY":
                audioSource.clip = audioDestroy;
                break;
            case "BEAM":
                audioSource.clip = audioBeam;
                break;
            case "IMG1":
                audioSource.clip = audioPianoCrash;
                break;
            case "IMG2":
                audioSource.clip = audioLaugh;
                break;
            case "IMG3":
                audioSource.clip = audioCrash;
                break;
            case "PEN":
                audioSource.clip = audioPen;
                break;
        }
        audioSource.Play();
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
                if (isfirstPassword == true)
                {
                    FirstPwd(hit);
                }
                else if (isSecondPassword == true)
                {
                    SecondPwd(hit);
                }
                else if (isThirdPassword == true)
                {
                    ThirdPwd(hit);
                }
                return;
            } // switch 호출
        }
        else
        {
            timeElapsed -= Time.deltaTime + 3f;
            if (timeElapsed < 0)
            {
                timeElapsed = 0;
            }
            reticle.fillAmount = timeElapsed * 0;
        }
        Debug.DrawRay(transform.position, forward, Color.red);
    }
    void Movement(RaycastHit hit)
    {
        string hitName = hit.transform.tag;
        if(isInStartRoom == true)
        {
            switch (hitName)
            {
                case "text1":
                    startArrow.transform.localScale = new Vector3(0f, 0f, 0f);
                    PlaySound("PAPER");
                    page.transform.localScale = new Vector3(0.19f, 0.19f, 0.19f);
                    break;

                case "pageOut":
                    PlaySound("PAPERTHROW");
                    page.transform.localScale = new Vector3(0f, 0.19f, 0.19f);
                    break;

                case "firstDoor":
                    if (isFirstDoorKey == true)
                    {
                        imgStartRoomKey.fillAmount = 0;
                        OpenDoor();
                        PlaySound("DOOROPEN");
                        isInStartRoom = false;
                        isInQuiz1 = true;
                    }
                    else
                    {
                        startArrow.transform.localScale = new Vector3(0.08f, 0.08f, 0.05f);
                        PlaySound("NOKEY");
                        Debug.Log("키 없음");
                    }
                    break;

                case "StartDrawer":
                    POSV = pos[3].position;
                    MovePoint(POSV);
                    aniStartDrawer.Play("openDrawer2", 0, 0.0f);
                    PlaySound("DRAWER");
                    break;

                case "firstKey":
                    isFirstDoorKey = true;
                    PlaySound("KEYON");
                    Destroy(firstKey, 0.0f);
                    imgStartRoomKey.fillAmount = 1;
                    break;
            }
        }
        else if (isInQuiz1 == true)
        {
            isfirstPassword = true;
            switch (hitName)
            {
                case "img1":
                    PlaySound("PEN");
                    txtImg[0].transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
                    break;

                case "img2":
                    PlaySound("PEN");
                    txtImg[1].transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
                    break;

                case "img3":
                    PlaySound("IMG2");
                    scareimg2.transform.localScale = new Vector3(1f, 1f, 1f);
                    InStartFade(3);
                    OutStartFade(3);
                    txtImg[2].transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
                    break;

                case "img4":
                    PlaySound("IMG3");
                    scareimg3.transform.localScale = new Vector3(1f, 1f, 1f);
                    InStartFade(4);
                    OutStartFade(4);
                    txtImg[3].transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
                    break;

                case "img5":
                    PlaySound("IMG1");
                    scareimg.transform.localScale = new Vector3(1f, 1f, 1f);
                    InStartFade(2);
                    OutStartFade(2);
                    txtImg[4].transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
                    break;

                case "img6":
                    PlaySound("PEN");
                    txtImg[5].transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
                    break;

                case "img7":
                    PlaySound("PEN");
                    txtImg[6].transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
                    break;

                case "firstRoomOut":
                    POSV = pos[2].position;
                    MovePoint(POSV);
                    PlaySound("WALK");
                    firstQuiz.size = new Vector3(1f, 1f, 1f);
                    mainLight[1].intensity = 1.3f;
                    Destroy(tel[0], 0.0f);
                    Destroy(hit.transform.gameObject, 0.0f);
                    break;

                //퀴즈1
                case "firstQuiz":
                    if (isFirstLocation == true)
                    {
                        break;
                    }
                    else if (isFirstLocation == false)
                    {
                        Destroy(tel[1], 0.0f);
                        isfirstPassword = true;
                        firstWallText.fontSize = 50;
                        Quiz1Random(); // 퀴즈1 랜덤
                        POSV = pos[4].position;
                        MovePoint(POSV);
                        pwd[0].transform.localScale = new Vector3(1f, 1f, 1f);
                        isFirstLocation = true;
                        break;
                    }
                    break;
                    

                case "FirstQuiz_Drawer":
                    if (isFirstQuizKey == true)
                    {
                        PlaySound("DRAWER");
                        POSV = pos[5].position;
                        aniQuiz1Drawer.Play("openDrawer2");
                        MovePoint(POSV);
                        tel[2].transform.localPosition = new Vector3(0.09f, -0.09f, 2.31f);
                        firstWallText.text = "뒤를돌아봐";
                        raycheckbool = false;
                        isfirstPassword = false;
                        isSecondPassword = true;
                        isInQuiz1 = false;
                        isInQuiz2 = true;
                        break;
                    }
                    else
                    {
                        break;
                    }
            }
        }
        else if (isInQuiz2 == true)
        {
            switch (hitName)
            {
                //퀴즈2
                case "secondKey":
                    PlaySound("KEYON");
                    isSecondQuizKey = true;
                    Destroy(secondKey, 0.0f);
                    imgFirstQuizKey.fillAmount = 1;
                break;

                case "secondLight":
                    if (isSecondQuizKey == true)
                    {
                        PlaySound("LIGHTCHANGE");
                        secondWallText.fontSize = 90;
                        ColorRandom();
                        Quiz2Arrow.transform.localScale = new Vector3(0f, 0f, 0f);
                        Quiz2Arrow2.transform.localScale = new Vector3(0f, 0f, 0f);
                        firstQuizLight.range = 1;
                        imgFirstQuizKey.fillAmount = 0;
                    }
                break;

                case "secondQuiz":
                if (isposiCheck2 == false)
                {
                    Destroy(tel[2], 0.0f);
                    isSecondPassword = true;
                    isposiCheck2 = true;
                    mainLight[2].range = 9.1f;
                    pwd[1].transform.localScale = new Vector3(1f, 1f, 1f);
                    POSV = pos[6].position;
                    secondWallText.fontSize = 50;
                    secondWallText.text = "전선 스위치 연결";
                    Quiz2Arrow.transform.localScale = new Vector3(0.08f, 0.08f, 0.05f);
                    Quiz2Arrow2.transform.localScale = new Vector3(0.08f, 0.08f, 0.05f);
                    MovePoint(POSV);
                    break;
                }
                else
                {
                    break;
                }

                case "Quiz2":
                    PlaySound("DESTROY");
                    Destroy(hit.transform.gameObject, 0.0f);
                break;
                }

        }
        else if (isInQuiz3 == true)
        {
            isThirdPassword = true;
            switch (hitName)
            {
                //퀴즈3
                case "SecondQuiz_Drawer":
                    if (isFinalQuizKey == true)
                    {
                        PlaySound("DRAWER");
                        isSecondPassword = false;
                        isThirdPassword = true;
                        isInQuiz2 = false;
                        isInQuiz3 = true;
                        isEndLocation = false;
                        POSV = pos[7].position;
                        MovePoint(POSV);
                        aniQuiz2Drawer.Play("openDrawer2");
                        secondWallText.text = "전구 교체";
                        Quiz3Arrow.transform.localScale = new Vector3(0.08f, 0.08f, 0.05f);
                        raycheckbool = false;
                        break;
                    }
                    else
                        break;

                case "red":
                    if (isCubeOn == false)
                    {
                        Quiz3Arrow.transform.localScale = new Vector3(0f, 0f, 0f);
                        Quiz3Arrow2.transform.localScale = new Vector3(0.08f, 0.08f, 0.05f);
                        PlaySound("KEYON");
                        isCubeOn = true;
                        Destroy(hit.transform.gameObject, 0.0f);
                        imgRGBLight[0].fillAmount = 1;
                        light = "red";
                        break;
                    }
                    break;

                case "blue":
                    if (isCubeOn == false)
                    {
                        Quiz3Arrow.transform.localScale = new Vector3(0f, 0f, 0f);
                        Quiz3Arrow2.transform.localScale = new Vector3(0.08f, 0.08f, 0.05f);
                        PlaySound("KEYON");
                        isCubeOn = true;
                        Destroy(hit.transform.gameObject, 0.0f);
                        imgRGBLight[1].fillAmount = 1;
                        light = "blue";
                        break;
                    }
                    break;

                case "green":
                    if (isCubeOn == false)
                    {
                        Quiz3Arrow.transform.localScale = new Vector3(0f, 0f, 0f);
                        Quiz3Arrow2.transform.localScale = new Vector3(0.08f, 0.08f, 0.05f);
                        PlaySound("KEYON");
                        isCubeOn = true;
                        Destroy(hit.transform.gameObject, 0.0f);
                        imgRGBLight[2].fillAmount = 1;
                        light = "green";
                        break;
                    }
                    break;


                case "lightcube":
                    buttonRGBLight[0].range = 0;
                    buttonRGBLight[1].range = 0;
                    buttonRGBLight[2].range = 0;
                    if (light == "red")
                    {
                        Quiz3Arrow2.transform.localScale = new Vector3(0f, 0f, 0f);
                        Quiz3Arrow3.transform.localScale = new Vector3(0.08f, 0.08f, 0.05f);
                        PlaySound("LIGHTCHANGE");
                        isLightCheck = true;
                        buttonRGBLight[0].range = 25;
                        imgRGBLight[0].fillAmount = 0;
                        isCubeOn = false;
                        break;
                    }
                    else if (light == "blue")
                    {
                        Quiz3Arrow2.transform.localScale = new Vector3(0f, 0f, 0f);
                        Quiz3Arrow3.transform.localScale = new Vector3(0.08f, 0.08f, 0.05f);
                        PlaySound("LIGHTCHANGE");
                        isLightCheck = true;
                        buttonRGBLight[1].range = 25;
                        imgRGBLight[1].fillAmount = 0;
                        isCubeOn = false;
                        break;
                    }
                    else if (light == "green")
                    {
                        Quiz3Arrow2.transform.localScale = new Vector3(0f, 0f, 0f);
                        Quiz3Arrow3.transform.localScale = new Vector3(0.08f, 0.08f, 0.05f);
                        PlaySound("LIGHTCHANGE");
                        isLightCheck = true;
                        buttonRGBLight[2].range = 25;
                        imgRGBLight[2].fillAmount = 0;
                        isCubeOn = false;
                        break;
                    }
                    break;

                case "onButton":
                    Quiz3Arrow3.transform.localScale = new Vector3(0f, 0f, 0f);
                    Quiz3Arrow.transform.localScale = new Vector3(0.08f, 0.08f, 0.05f);
                    PlaySound("SWITCH");
                    mainRGBLight[0].range = 0;
                    mainRGBLight[2].range = 0;
                    mainRGBLight[1].range = 0;
                    mainLight[0].range = 0;
                    mainLight[1].range = 0;
                    mainLight[2].range = 5;
                    mainLight[4].range = 0;
                    pwd[2].transform.localScale = new Vector3(1f, 1f, 1f);
                    endQuad.transform.localScale = new Vector3(2.35f, 1f, 1f);
                    if (isLightCheck == true)
                    {
                        if (light == "red")
                        {
                            mainRGBLight[0].range = 6;

                        }
                        else if (light == "blue")
                        {
                            mainRGBLight[1].range = 6;
                            thirdWallText.text = "100+200=^^";
                        }
                        else if (light == "green")
                        {
                            mainRGBLight[2].range = 6;
                        }
                    }
                    break;

                case "endQuad":
                    if (isEndLocation == false)
                    {
                        isEndLocation = true;
                        POSV = pos[8].position;
                        MovePoint(POSV);
                        break;
                    }
                    else
                        break;

                case "endRoomIn":
                    Destroy(hit.transform.gameObject, 0.0f);
                    POSV = pos[9].position;
                    MovePoint(POSV);
                    break;

                case "EndBeam":
                    if (isEndLocation == true)
                    {
                        PlaySound("BEAM");
                        BackGround.Stop();
                        EndMusic.Play();
                        mainLight[3].intensity = 0;
                        // End.transform.localScale = new Vector3(2.97f, 2.97f, 0.38f);
                        endText1.transform.localScale = new Vector3(1f, 1f, 1f);
                        aniEndBeam.Play("end_light");
                        timerOn = false;
                        clearTimeText.text = B.text;
                        break;
                    }
                    else
                        break;
            }
        }
        else
        {
            return;
        }
        raycheckbool = false;
    }
    // 비밀번호 기능
    void FirstPwd(RaycastHit hit)
    {
        string hitName = hit.transform.tag;
        string buttonName;
        switch (hitName)
        {
            case "Button00":
                buttonName = "Button00";
                ButtonCheck(buttonName, 0, 1, 2);
                break;

            case "Button01":
                buttonName = "Button01";
                ButtonCheck(buttonName, 0, 1, 2);
                break;

            case "Button02":
                buttonName = "Button02";
                ButtonCheck(buttonName, 0, 1, 2);
                break;

            case "Button03":
                buttonName = "Button03";
                ButtonCheck(buttonName, 0, 1, 2);
                break;

            case "Button04":
                buttonName = "Button04";
                ButtonCheck(buttonName, 0, 1, 2);
                break;

            case "Button05":
                buttonName = "Button05";
                ButtonCheck(buttonName, 0, 1, 2);
                break;

            case "Button06":
                buttonName = "Button06";
                ButtonCheck(buttonName, 0, 1, 2);
                break;

            case "Button07":
                buttonName = "Button07";
                ButtonCheck(buttonName, 0, 1, 2);
                break;

            case "Button08":
                buttonName = "Button08";
                ButtonCheck(buttonName, 0, 1, 2);
                break;

            case "Button09":
                buttonName = "Button09";
                ButtonCheck(buttonName, 0, 1, 2);
                break;

            case "ButtonReset":
                PwdButtonText[0].text = "X";
                PwdButtonText[1].text = "X";
                PwdButtonText[2].text = "X";
                break;

            case "ButtonEnter":
                int a = Quiz1PwdNum[0] - 1;
                int b = Quiz1PwdNum[1] - 1;
                int c = Quiz1PwdNum[2] - 1;
                if (PwdButtonText[0].text == Quiz1ClearNum[a].ToString() && PwdButtonText[1].text == Quiz1ClearNum[b].ToString() && PwdButtonText[2].text == Quiz1ClearNum[c].ToString())
                {
                    pwd[0].transform.localScale = new Vector3(0f, 1f, 1f);
                    lockCheck[0].transform.localScale = new Vector3(0f, 0f, 1f);
                    firstWallText.fontSize = 50;
                    firstWallText.text = "두번째칸";
                    secondWallText.text = "?";
                    PlaySound("QUIZ1");
                    isFirstQuizKey = true;
                    isfirstPassword = false;

                }
                else
                {
                    PwdButtonText[0].text = "X";
                    PwdButtonText[1].text = "X";
                    PwdButtonText[2].text = "X";
                }
                break;
        }

    }
    void SecondPwd(RaycastHit hit)
    {
        string hitName = hit.transform.tag;
        string buttonName;
        switch (hitName)
        {
            case "Button10":
                buttonName = "Button00";
                ButtonCheck(buttonName, 3, 4, 5);
                break;

            case "Button11":
                buttonName = "Button01";
                ButtonCheck(buttonName, 3, 4, 5);
                break;

            case "Button12":
                buttonName = "Button02";
                ButtonCheck(buttonName, 3, 4, 5);
                break;

            case "Button13":
                buttonName = "Button03";
                ButtonCheck(buttonName, 3, 4, 5);
                break;

            case "Button14":
                buttonName = "Button04";
                ButtonCheck(buttonName, 3, 4, 5);
                break;

            case "Button15":
                buttonName = "Button05";
                ButtonCheck(buttonName, 3, 4, 5);
                break;

            case "Button16":
                buttonName = "Button06";
                ButtonCheck(buttonName, 3, 4, 5);
                break;

            case "Button17":
                buttonName = "Button07";
                ButtonCheck(buttonName, 3, 4, 5);
                break;

            case "Button18":
                buttonName = "Button08";
                ButtonCheck(buttonName, 3, 4, 5);
                break;

            case "Button19":
                buttonName = "Button09";
                ButtonCheck(buttonName, 3, 4, 5);
                break;

            case "ButtonReset":
                PwdButtonText[3].text = "X";
                PwdButtonText[4].text = "X";
                PwdButtonText[5].text = "X";
                break;

            case "ButtonEnter":
                string second = secondWallText.text;
                switch (second)
                {
                    case "Green":
                        if (PwdButtonText[3].text == "7" && PwdButtonText[4].text == "1" && PwdButtonText[5].text == "8")
                        {
                            ClearQuiz2();
                            PlaySound("QUIZ1");
                            isInQuiz2 = false;
                            isInQuiz3 = true;
                            break;
                        }
                        break;

                    case "Blue":
                        if (PwdButtonText[3].text == "3" && PwdButtonText[4].text == "2" && PwdButtonText[5].text == "8")
                        {
                            ClearQuiz2();
                            PlaySound("QUIZ1");
                            isInQuiz2 = false;
                            isInQuiz3 = true;
                            break;
                        }
                        break;

                    case "Purple":
                        if (PwdButtonText[3].text == "7" && PwdButtonText[4].text == "9" && PwdButtonText[5].text == "2")
                        {
                            ClearQuiz2();
                            PlaySound("QUIZ1");
                            isInQuiz2 = false;
                            isInQuiz3 = true;
                            break;
                        }
                        break;

                    case "Yellow":
                        if (PwdButtonText[3].text == "6" && PwdButtonText[4].text == "5" && PwdButtonText[5].text == "4")
                        {
                            ClearQuiz2();
                            PlaySound("QUIZ1");
                            isInQuiz2 = false;
                            isInQuiz3 = true;
                            break;
                        }
                        break;
                }
                ClearText();
                break;
        }
    }
    void ThirdPwd(RaycastHit hit)
    {
        string hitName = hit.transform.tag;
        string buttonName;
        switch (hitName)
        {
            case "Button20":
                buttonName = "Button00";
                ButtonCheck(buttonName, 6, 7, 8);
                break;

            case "Button21":
                buttonName = "Button01";
                ButtonCheck(buttonName, 6, 7, 8);
                break;

            case "Button22":
                buttonName = "Button02";
                ButtonCheck(buttonName, 6, 7, 8);
                break;

            case "Button23":
                buttonName = "Button03";
                ButtonCheck(buttonName, 6, 7, 8);
                break;

            case "Button24":
                buttonName = "Button04";
                ButtonCheck(buttonName, 6, 7, 8);
                break;

            case "Button25":
                buttonName = "Button05";
                ButtonCheck(buttonName, 6, 7, 8);
                break;

            case "Button26":
                buttonName = "Button06";
                ButtonCheck(buttonName, 6, 7, 8);
                break;

            case "Button27":
                buttonName = "Button07";
                ButtonCheck(buttonName, 6, 7, 8);
                break;

            case "Button28":
                buttonName = "Button08";
                ButtonCheck(buttonName, 6, 7, 8);
                break;

            case "Button29":
                buttonName = "Button09";
                ButtonCheck(buttonName, 6, 7, 8);
                break;

            case "ButtonReset":
                PwdButtonText[6].text = "X";
                PwdButtonText[7].text = "X";
                PwdButtonText[8].text = "X";
                break;

            case "ButtonEnter":
                if (PwdButtonText[6].text == "3" && PwdButtonText[7].text == "0" && PwdButtonText[8].text == "0")
                {
                    pwd[2].transform.localScale = new Vector3(0f, 1f, 1f);
                    lockCheck[2].transform.localScale = new Vector3(0f, 0f, 1f);
                    PlaySound("DOOROPEN");
                    endText.text = "정답" + "\n빔 프로젝터";
                    aniEndDoor.Play("end_door");
                    break;

                }
                else
                {
                    PwdButtonText[6].text = "X";
                    PwdButtonText[7].text = "X";
                    PwdButtonText[8].text = "X";
                }
                break;
        }
    }
    // 비밀번호 버튼 확인 기능
    void ButtonCheck(string buttonName, int n0, int n1, int n2)
    {
        PlaySound("BUTTON");
        Debug.Log(buttonName.Substring(7));
        if (PwdButtonText[n2].text == "X")
        {
            if (PwdButtonText[n1].text == "X")
            {
                if (PwdButtonText[n0].text == "X")
                {
                    Debug.Log("ButtonCheck1");
                    PwdButtonText[n0].text = buttonName.Substring(7);
                    return;
                }
                else
                    PwdButtonText[n1].text = buttonName.Substring(7);
                return;
            }
            else
                PwdButtonText[n2].text = buttonName.Substring(7);
            return;
        }

    }
    // 장소 이동 기능
    void MovePoint(Vector3 ves3)
    {
        InStartFade(1);
        MoveStart(ves3);
        PlaySound("WALK");
        OutStartFade(1);
    }
    // Quiz2에서 랜덤값 추출
    void ColorRandom()
    {
        for (int i = 0; i < 3; i++)
        {
            int rand = UnityEngine.Random.Range(0, 4);
            myColor = (enumColor)rand;
            secondWallText.text = myColor.ToString();
        }
    }
    void Quiz1Random()
    {
        for (int k = 0; k < 3; k++)
        {
            int num = UnityEngine.Random.Range(0, Quiz1Num.Count);
            Quiz1PwdNum.Add(Quiz1Num[num]);
            Quiz1Num.RemoveAt(num);
        }
        firstWallText.text = Quiz1PwdNum[0] + "->" + Quiz1PwdNum[1] + "->" + Quiz1PwdNum[2] + "\n그림 순서";
    }
    void ClearQuiz2()
    {
        pwd[1].transform.localScale = new Vector3(0f, 1f, 1f);
        lockCheck[1].transform.localScale = new Vector3(0f, 0f, 1f);
        secondWallText.text = "정답";
        isFinalQuizKey = true;
    }
    void ClearText()
    {
        PwdButtonText[3].text = "X";
        PwdButtonText[4].text = "X";
        PwdButtonText[5].text = "X";
    }
    // 이동할때 페이드 부분 구현
    public void OutStartFade(int OutFadeNum)
    {
        if (isPlaying == true)
        {
            return;
        }

        isPlaying = true;
        switch (OutFadeNum)
        {
            case 1:
                FadeController.StartCoroutine(FadeController.FadeOut());
                isPlaying = false;
                Debug.Log("OutFade");
                break;

            case 2:
                FadeController.StartCoroutine(FadeController.FadeOut1());
                isPlaying = false;
                break;

            case 3:
                FadeController.StartCoroutine(FadeController.FadeOut2());
                isPlaying = false;
                break;
            case 4:
                FadeController.StartCoroutine(FadeController.FadeOut3());
                isPlaying = false;
                break;
        }
    }
    public void InStartFade(int InFadeNum)
    {
        if (isPlaying == true)
        {
            return;
        }
        isPlaying = true;
        switch (InFadeNum)
        {
            case 1:
                FadeController.StartCoroutine(FadeController.FadeIn());
                isPlaying = false;
                Debug.Log("StartFade");
                break;
            case 2:
                FadeController.StartCoroutine(FadeController.FadeIn1());
                isPlaying = false;
                break;
            case 3:
                FadeController.StartCoroutine(FadeController.FadeIn2());
                isPlaying = false;
                break;
            case 4:
                FadeController.StartCoroutine(FadeController.FadeIn3());
                isPlaying = false;
                break;
        }
    }
    public void OpenDoor()
    {
        if (isPlaying == true)
        {
            return;
        }
        isPlaying = true;
        aniFirstDoor.Play("openDoor", 0, 0.0f);
        isPlaying = false;
        Debug.Log("openDoor");
    }
    public void MoveStart(Vector3 vcs3)
    {
        if (isPlaying == true)
        {
            return;
        }
        isPlaying = true;
        StartCoroutine(Move(vcs3));
        Debug.Log("Move");
    }
    IEnumerator Move(Vector3 vc3)
    {
        CamOffset.transform.position = vc3;
        isPlaying = false;
        yield return null;
    }
}
