using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class MainMenu : MonoBehaviour
{
    GameObject player;
    Options theOptions;
    public Text playerhealth;
    public Text playerlight;
    public int health;
    public int theLight;
    public float[] choices = new float[] { };
    int maxchoices = 4;

    public int currentSelection = 0;
    public GameObject SelectIcon;
    public Text ControlsText;
    public Text ControlsTextW;
    bool ControllerOptionToggle;
    bool AxisChanged = false;

    SpriteRenderer backgroundScroll;

    public Text sfxInt;
    public Text musicInt;

    public Text sfxInt2;
    public Text musicInt2;

    public enum Menu { Main = 0, Options, Achievements, Credits, Continue, AreYouSure, SelectDifficulty };
    Menu CurrMenu = Menu.Main;
    public GameObject MainMenuText;
    public GameObject[] MainMenu4Highlight = new GameObject[] { };

    public GameObject ContinueText;
    public GameObject[] ContinueTextHighlight = new GameObject[] { };

    public GameObject SelectDifficulty;
    public GameObject[] SelectDifficulty4Highlight = new GameObject[] { };

    public GameObject OptionsMenuText;
    public GameObject[] OptionsMenuText4Highlight = new GameObject[] { };

    public GameObject Achievements;
    public GameObject[] Achievements4Highlight = new GameObject[] { };

    public int achvState;

    public GameObject Credits;
    public GameObject[] Credits4Highlight = new GameObject[] { };

    public AudioClip changeSound;
    public AudioClip selectSound;
    public AudioSource soundSource;

    public Text continueForGraying;

    public Rect[] SelectRects;
    Vector2 Mouse;
    Vector2 MouseOld;
    bool MouseCanClick;
    bool disablemouse;
    float timer = 0;
    int offset = 0;
    // Use this for initialization
    void Start()
    {
        MouseCanClick = false;
        disablemouse = false;
        // Load();
        SelectRects = new Rect[7];
        for (int i = 0; i < SelectRects.Length; i++)
        {

            SelectRects[i] = new Rect(516, 125 + offset, 300, 150);
            offset += 67;
        }
        SelectRects[0] = new Rect(516, 125, 300, 150);  //Exit button
        SelectRects[1] = new Rect(516, 192, 300, 150); // Off the screen not needed for this menu
        SelectRects[2] = new Rect(516, 259, 300, 150); // Off the screen not needed for this menu
        SelectRects[3] = new Rect(516, 326, 300, 150); // Off the screen not needed for this menu
        SelectRects[4] = new Rect(516, 393, 300, 150); // Off the screen not needed for this menu
        SelectRects[5] = new Rect(516, 460, 300, 150); // Off the screen not needed for this menu
        SelectRects[6] = new Rect(516, 527, 300, 150); // Off the screen not needed for this menu
        Mouse = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);

        ControllerOptionToggle = false;
        health = 50;
        theLight = 0;

        achvState = 0;

#if UNITY_STANDALONE
        //  Save();
#endif
        //   playerhealth.text = health.ToString();
        //     playerlight.text = light.ToString();
        //soundSource = GetComponent<AudioSource>();
        theOptions = GameObject.Find("TheOptions").GetComponent<Options>();



        backgroundScroll = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //print(Mouse);

        MouseOld = Mouse;
        Mouse = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
        if (Mouse != MouseOld && disablemouse == false)
        {
            for (int i = 0; i < SelectRects.Length; i++)
            {
                if (SelectRects[i].Contains(Mouse))
                    currentSelection = i;
            }
        }


        for (int i = 0; i < SelectRects.Length; i++)
        {
            MouseCanClick = false;
            if (SelectRects[i].Contains(Mouse))
            {
                MouseCanClick = true;
                break;
            }
        }


        timer -= Time.deltaTime;

        if (Achievements.activeSelf == false)
        {
            SelectIcon.transform.position = new Vector3(-2.2f, choices[currentSelection], -5);
            backgroundScroll.enabled = true;
        }
        else
        {
            SelectIcon.transform.position = new Vector3(-1.2f, choices[currentSelection], -5);
            backgroundScroll.enabled = false;
        }

        if (Achievements.activeSelf == true)
        {
            if ((Input.GetAxis("CLSVertical") > .7f || Input.GetAxis("KBVertical") > 0 || Input.GetAxis("CDPadVertical") > .7f || Input.GetAxis("CLSHorizontal") > .7f || Input.GetAxis("KBHorizontal") > 0 || Input.GetAxis("CDPadHorizontal") > .7f) && AxisChanged == false)
            {
                if (currentSelection < maxchoices && currentSelection != 8 && currentSelection != 9)
                    currentSelection += 1;
                else
                    currentSelection = 0;
                AxisChanged = true;

                if (soundSource.isPlaying)
                    soundSource.Stop();
                soundSource.clip = changeSound;
                soundSource.Play();
            }
            if ((Input.GetAxis("CLSVertical") < -0.7f || Input.GetAxis("KBVertical") < 0 || Input.GetAxis("CDPadVertical") < -0.7f || Input.GetAxis("CLSHorizontal") < -0.7f || Input.GetAxis("KBHorizontal") < 0 || Input.GetAxis("CDPadHorizontal") < -0.7f) && AxisChanged == false)
            {
                if (currentSelection > 0)
                    currentSelection -= 1;
                else
                    currentSelection = maxchoices;
                AxisChanged = true;

                if (soundSource.isPlaying)
                    soundSource.Stop();
                soundSource.clip = changeSound;
                soundSource.Play();

            }
        }
        else
        {
            if ((Input.GetAxis("CLSVertical") > .7f || Input.GetAxis("KBVertical") > 0 || Input.GetAxis("CDPadVertical") > .7f) && AxisChanged == false && currentSelection < 8)
            {
                if (currentSelection > 0)
                    currentSelection -= 1;
                else
                    currentSelection = maxchoices;
                AxisChanged = true;

                if (soundSource.isPlaying)
                    soundSource.Stop();
                soundSource.clip = changeSound;
                soundSource.Play();
            }
            if ((Input.GetAxis("CLSVertical") < -0.7f || Input.GetAxis("KBVertical") < 0 || Input.GetAxis("CDPadVertical") < -0.7f) && AxisChanged == false && currentSelection < 8)
            {
                if (currentSelection < maxchoices && currentSelection != 8 && currentSelection != 9)
                    currentSelection += 1;
                else
                    currentSelection = 0;
                AxisChanged = true;

                if (soundSource.isPlaying)
                    soundSource.Stop();
                soundSource.clip = changeSound;
                soundSource.Play();
            }

        }

        if ((Input.GetAxis("CLSVertical") == 0 && Input.GetAxis("KBVertical") == 0 && Input.GetAxis("CDPadVertical") == 0 && Input.GetAxis("CLSHorizontal") == 0 && Input.GetAxis("KBHorizontal") == 0 && Input.GetAxis("CDPadHorizontal") == 0) && currentSelection < 8)
        {
            AxisChanged = false;
        }

        switch (CurrMenu)
        {
            case Menu.Main:
                {
                    SelectRects[0] = new Rect(516, 125, 300, 150);
                    SelectRects[1] = new Rect(516, 192, 300, 150);
                    SelectRects[2] = new Rect(516, 259, 300, 150);
                    SelectRects[3] = new Rect(516, 326, 300, 150);
                    SelectRects[4] = new Rect(516, 525, 300, 150);
                    SelectRects[5] = new Rect(516, 1867, 300, 150);
                    SelectRects[6] = new Rect(516, 1867, 300, 150);
                    MainMenuSelect();
                    break;
                }
            case Menu.Options:
                {
                    SelectRects[0] = new Rect(516, 125, 300, 150);
                    SelectRects[1] = new Rect(516, 192, 300, 150);
                    SelectRects[2] = new Rect(516, 259, 300, 150);
                    SelectRects[3] = new Rect(516, 326, 300, 150);
                    SelectRects[4] = new Rect(516, 525, 300, 150);
                    SelectRects[5] = new Rect(516, 1867, 300, 150);
                    SelectRects[6] = new Rect(516, 1867, 300, 150);
                    OptionsMenu();
                    break;
                }
            case Menu.Credits:
                {
                    SelectRects[0] = new Rect(516, 525, 300, 150);
                    SelectRects[1] = new Rect(516, 1867, 300, 150);
                    SelectRects[2] = new Rect(516, 1867, 300, 150);
                    SelectRects[3] = new Rect(516, 1867, 300, 150);
                    SelectRects[4] = new Rect(516, 1867, 300, 150);
                    SelectRects[5] = new Rect(516, 1867, 300, 150);
                    SelectRects[6] = new Rect(516, 1867, 300, 150);
                    CreditsMenu();
                    break;
                }
            case Menu.Achievements:
                {
                    SelectRects[0] = new Rect(516, 525, 300, 150);
                    SelectRects[1] = new Rect(516, 1867, 300, 150);
                    SelectRects[2] = new Rect(516, 1867, 300, 150);
                    SelectRects[3] = new Rect(516, 1867, 300, 150);
                    SelectRects[4] = new Rect(516, 1867, 300, 150);
                    SelectRects[5] = new Rect(516, 1867, 300, 150);
                    SelectRects[6] = new Rect(516, 1867, 300, 150);
                    AchievementsMenu();
                    break;
                }
            case Menu.Continue:
                {
                    SelectRects[0] = new Rect(516, 125 + 70, 300, 150);
                    SelectRects[1] = new Rect(516, 125 + 140, 300, 150);
                    SelectRects[2] = new Rect(516, 525, 300, 150);
                    SelectRects[3] = new Rect(516, 1867, 300, 150);
                    SelectRects[4] = new Rect(516, 1867, 300, 150);
                    SelectRects[5] = new Rect(516, 1867, 300, 150);
                    SelectRects[6] = new Rect(516, 1867, 300, 150);
                    ContinueMenu();
                    break;
                }
            case Menu.SelectDifficulty:
                {
                    SelectRects[0] = new Rect(516, 125 + 70, 300, 150);
                    SelectRects[1] = new Rect(516, 125 + 140, 300, 150);
                    SelectRects[2] = new Rect(516, 525, 300, 150);
                    SelectRects[3] = new Rect(516, 1867, 300, 150);
                    SelectRects[4] = new Rect(516, 1867, 300, 150);
                    SelectRects[5] = new Rect(516, 1867, 300, 150);
                    SelectRects[6] = new Rect(516, 1867, 300, 150);
                    SelectDifficultyMenu();
                    break;
                }
        }

    }


    void MainMenuSelect()
    {
        maxchoices = 4;
        choices[0] = 2.93f;
        choices[1] = 1.95f;
        choices[2] = 0.96f;
        choices[3] = 0;
        choices[4] = -2.72f;
        MainMenuText.SetActive(true);


        switch (currentSelection)
        {
            //This is used for Highlighting the currently selected menu option
            case 0:
                {
                    for (int i = 0; i < 5; i++)
                    {
                        MainMenu4Highlight[i].SetActive(false);
                    }
                    MainMenu4Highlight[0].SetActive(true);
                    break;
                }
            case 1:
                {
                    for (int i = 0; i < 5; i++)
                    {
                        MainMenu4Highlight[i].SetActive(false);
                    }
                    MainMenu4Highlight[1].SetActive(true);
                    break;
                }
            case 2:
                {
                    for (int i = 0; i < 5; i++)
                    {
                        MainMenu4Highlight[i].SetActive(false);
                    }
                    MainMenu4Highlight[2].SetActive(true);
                    break;
                }
            case 3:
                {
                    for (int i = 0; i < 5; i++)
                    {
                        MainMenu4Highlight[i].SetActive(false);
                    }
                    MainMenu4Highlight[3].SetActive(true);
                    break;
                }
            case 4:
                {
                    for (int i = 0; i < 5; i++)
                    {
                        MainMenu4Highlight[i].SetActive(false);
                    }
                    MainMenu4Highlight[4].SetActive(true);
                    break;
                }
        }



        if ((Input.GetButtonDown("CMenuAccept") || Input.GetButtonDown("KBMenuAccept")) || (Input.GetKeyDown(KeyCode.Mouse0) && MouseCanClick) && timer < 0)
        {
            timer = .5f;
            for (int i = 0; i < 5; i++)
            {
                MainMenu4Highlight[i].SetActive(false);
            }

            if (soundSource.isPlaying)
                soundSource.Stop();
            soundSource.clip = selectSound;
            soundSource.Play();

            switch (currentSelection)
            {
                case 0:
                    {
                        MainMenuText.SetActive(false);
                        CurrMenu = Menu.Continue;
                        break;
                    }
                case 1:
                    {
                        MainMenuText.SetActive(false);
                        CurrMenu = Menu.Options;
                        break;
                    }
                case 2:
                    {
                        MainMenuText.SetActive(false);
                        //LevelManager.Load("Credits");
                        CurrMenu = Menu.Credits;
                        Application.LoadLevel("Credits");
                        break;
                    }
                case 3:
                    {
                        MainMenuText.SetActive(false);
                        CurrMenu = Menu.Achievements;
                        achvState = 0;
                        break;
                    }
                case 4:
                    {
#if UNITY_STANDALONE
                        Application.Quit();
#elif UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;
#endif
                        break;
                    }
            }

            currentSelection = 0;
        }
        if ((Input.GetButtonDown("CMenuCancel") || Input.GetButtonDown("KBPause")) && timer < 0)
        {
            timer = .1f;
            currentSelection = 4;

            if (soundSource.isPlaying)
                soundSource.Stop();
            soundSource.clip = selectSound;
            soundSource.Play();

        }
    }

    void OptionsMenu()
    {
        maxchoices = 4;
        choices[0] = 2.9f;
        choices[1] = 1.9f;
        choices[2] = 1f;
        choices[3] = 0f;
        choices[4] = -2.72f;
        choices[8] = 2.9f;
        choices[9] = 1.9f;
        OptionsMenuText.SetActive(true);

        sfxInt.text = theOptions.sfxVolume.ToString();
        sfxInt2.text = theOptions.sfxVolume.ToString();
        musicInt.text = theOptions.musicVolume.ToString();
        musicInt2.text = theOptions.musicVolume.ToString();



        switch (currentSelection)
        {
            //This is used for Highlighting the currently selected menu option
            case 0:
                {
                    for (int i = 0; i < 7; i++)
                    {
                        OptionsMenuText4Highlight[i].SetActive(false);
                    }
                    OptionsMenuText4Highlight[0].SetActive(true);
                    break;
                }
            case 1:
                {
                    for (int i = 0; i < 7; i++)
                    {
                        OptionsMenuText4Highlight[i].SetActive(false);
                    }
                    OptionsMenuText4Highlight[1].SetActive(true);
                    break;
                }
            case 2:
                {
                    for (int i = 0; i < 7; i++)
                    {
                        OptionsMenuText4Highlight[i].SetActive(false);
                    }
                    OptionsMenuText4Highlight[2].SetActive(true);
                    break;
                }
            case 3:
                {
                    for (int i = 0; i < 7; i++)
                    {
                        OptionsMenuText4Highlight[i].SetActive(false);
                    }
                    OptionsMenuText4Highlight[3].SetActive(true);
                    break;
                }
            case 4:
                {
                    for (int i = 0; i < 7; i++)
                    {
                        OptionsMenuText4Highlight[i].SetActive(false);
                    }
                    OptionsMenuText4Highlight[4].SetActive(true);
                    break;
                }
            case 8:
                {
                    for (int i = 0; i < 7; i++)
                    {
                        OptionsMenuText4Highlight[i].SetActive(false);
                    }
                    OptionsMenuText4Highlight[5].SetActive(true);
                    break;
                }
            case 9:
                {
                    for (int i = 0; i < 7; i++)
                    {
                        OptionsMenuText4Highlight[i].SetActive(false);
                    }
                    OptionsMenuText4Highlight[6].SetActive(true);
                    break;
                }
        }

        if ((Input.GetButtonDown("CMenuAccept") || Input.GetButtonDown("KBMenuAccept")) || (Input.GetKeyDown(KeyCode.Mouse0) && MouseCanClick) && timer < 0)
        {
            timer = .5f;
            for (int i = 0; i < 5; i++)
            {
                OptionsMenuText4Highlight[i].SetActive(false);
            }

            if (soundSource.isPlaying)
                soundSource.Stop();
            soundSource.clip = selectSound;
            soundSource.Play();

            switch (currentSelection)
            {
                case 0:
                    {
                        disablemouse = true;
                        currentSelection = 8;
                        break;
                    }
                case 1:
                    {
                        disablemouse = true;
                        currentSelection = 9;
                        break;
                    }
                case 2:
                    {
#if UNITY_WEBPLAYER
                        break;
#endif
                        if (Screen.fullScreen)
                        {
                            Screen.SetResolution(1280, 720, !Screen.fullScreen);

                        }
                        else
                        {
                            Screen.SetResolution(1280, 720, Screen.fullScreen);

                        }
                        Screen.fullScreen = !Screen.fullScreen;
                        break;
                    }
                case 3:
                    {
                        //CHANGE CONTROLS FROM KB/MOUSE to CONTROLLER
                        InputManager.controller = !InputManager.controller;
                        //CHANGE TEXT TO REFLECT CHANGE
                        if (ControllerOptionToggle)
                        {
                            ControlsText.text = "Controls: KB/Mouse";
                            ControlsTextW.text = "Controls: KB/Mouse";
                            ControllerOptionToggle = false;
                        }
                        else
                        {
                            ControlsText.text = "Controls: Controller";
                            ControlsTextW.text = "Controls: Controller";

                            ControllerOptionToggle = true;
                        }
                        break;
                    }
                case 4:
                    {
                        OptionsMenuText.SetActive(false);
                        CurrMenu = Menu.Main;
                        currentSelection = 1;
                        break;
                    }
                case 8:
                    {
                        disablemouse = false;
                        currentSelection = 0;
                        break;
                    }
                case 9:
                    {
                        disablemouse = false;
                        currentSelection = 1;
                        break;
                    }
            }
        }
        if ((Input.GetButtonDown("CMenuCancel") || Input.GetButtonDown("KBPause")) && timer < 0)
        {
            timer = .5f;
            for (int i = 0; i < 4; i++)
            {
                OptionsMenuText4Highlight[i].SetActive(false);
            }

            if (soundSource.isPlaying)
                soundSource.Stop();
            soundSource.clip = selectSound;
            soundSource.Play();

            switch (currentSelection)
            {
                case 8:
                    {
                        currentSelection = 0;
                        break;
                    }
                case 9:
                    {
                        currentSelection = 1;
                        break;
                    }
                default:
                    {
                        OptionsMenuText.SetActive(false);
                        CurrMenu = Menu.Main;
                        currentSelection = 1;
                        break;
                    }
            }
        }
        //Sound Fx Volume up/down
        if ((Input.GetAxis("CLSVertical") > .7f || Input.GetAxis("KBVertical") > 0 || Input.GetAxis("CDPadVertical") > .7f || Input.GetAxis("CLSHorizontal") > .7f || Input.GetAxis("KBHorizontal") > 0 || Input.GetAxis("CDPadHorizontal") > .7f) && AxisChanged == false && currentSelection == 8)
        {
            theOptions.sfxIncrease();
            if (soundSource.isPlaying)
                soundSource.Stop();
            soundSource.PlayOneShot(selectSound);
            sfxInt.text = theOptions.sfxVolume.ToString();
            sfxInt2.text = theOptions.sfxVolume.ToString();

            AxisChanged = true;
        }
        if ((Input.GetAxis("CLSVertical") < -0.7f || Input.GetAxis("KBVertical") < 0 || Input.GetAxis("CDPadVertical") < -0.7f || Input.GetAxis("CLSHorizontal") < -0.7f || Input.GetAxis("KBHorizontal") < 0 || Input.GetAxis("CDPadHorizontal") < -0.7f) && AxisChanged == false && currentSelection == 8)
        {
            theOptions.sfxDecrease();
            if (soundSource.isPlaying)
                soundSource.Stop();
            soundSource.PlayOneShot(selectSound);
            sfxInt.text = theOptions.sfxVolume.ToString();
            sfxInt2.text = theOptions.sfxVolume.ToString();
            AxisChanged = true;

        }
        if ((Input.GetAxis("CLSVertical") == 0 && Input.GetAxis("KBVertical") == 0 && Input.GetAxis("CDPadVertical") == 0 && Input.GetAxis("CLSHorizontal") == 0 && Input.GetAxis("KBHorizontal") == 0 && Input.GetAxis("CDPadHorizontal") == 0) && currentSelection == 8)
        {
            AxisChanged = false;
        }


        //Music Volume up/down
        if ((Input.GetAxis("CLSVertical") > .7f || Input.GetAxis("KBVertical") > 0 || Input.GetAxis("CDPadVertical") > .7f || Input.GetAxis("CLSHorizontal") > .7f || Input.GetAxis("KBHorizontal") > 0 || Input.GetAxis("CDPadHorizontal") > .7f) && AxisChanged == false && currentSelection == 9)
        {
            theOptions.musicIncrease();
            musicInt.text = theOptions.musicVolume.ToString();
            musicInt2.text = theOptions.musicVolume.ToString();
            AxisChanged = true;
        }
        if ((Input.GetAxis("CLSVertical") < -0.7f || Input.GetAxis("KBVertical") < 0 || Input.GetAxis("CDPadVertical") < -0.7f || Input.GetAxis("CLSHorizontal") < -0.7f || Input.GetAxis("KBHorizontal") < 0 || Input.GetAxis("CDPadHorizontal") < -0.7f) && AxisChanged == false && currentSelection == 9)
        {
            theOptions.musicDecrease();
            musicInt.text = theOptions.musicVolume.ToString();
            musicInt2.text = theOptions.musicVolume.ToString();
            AxisChanged = true;

        }
        if ((Input.GetAxis("CLSVertical") == 0 && Input.GetAxis("KBVertical") == 0 && Input.GetAxis("CDPadVertical") == 0 && Input.GetAxis("CLSHorizontal") == 0 && Input.GetAxis("KBHorizontal") == 0 && Input.GetAxis("CDPadHorizontal") == 0) && currentSelection == 9)
        {
            AxisChanged = false;
        }
    }

    void CreditsMenu()
    {
        maxchoices = 0;
        choices[0] = -2.72f;
        Credits.SetActive(true);



        switch (currentSelection)
        {
            //This is used for Highlighting the currently selected menu option
            case 0:
                {
                    for (int i = 0; i < 1; i++)
                    {
                        Credits4Highlight[i].SetActive(false);
                    }
                    Credits4Highlight[0].SetActive(true);
                    break;
                }
        }
        if ((Input.GetButtonDown("CMenuAccept") || Input.GetButtonDown("KBMenuAccept")) || (Input.GetKeyDown(KeyCode.Mouse0) && MouseCanClick) && timer < 0)
        {
            timer = 1;
            for (int i = 0; i < 1; i++)
            {
                Credits4Highlight[i].SetActive(false);
            }

            if (soundSource.isPlaying)
                soundSource.Stop();
            soundSource.clip = selectSound;
            soundSource.Play();

            switch (currentSelection)
            {
                case 0:
                    {
                        SelectRects[0] = new Rect(516, 125, 300, 150);
                        SelectRects[1] = new Rect(516, 192, 300, 150);
                        SelectRects[2] = new Rect(516, 259, 300, 150);
                        SelectRects[3] = new Rect(516, 326, 300, 150);
                        SelectRects[4] = new Rect(516, 525, 300, 150);
                        SelectRects[5] = new Rect(516, 1867, 300, 150);
                        SelectRects[6] = new Rect(516, 1867, 300, 150);
                        Credits.SetActive(false);
                        CurrMenu = Menu.Main;
                        currentSelection = 2;
                        break;
                    }
            }
        }

        //GO Back to Main
        if ((Input.GetButtonDown("CMenuCancel") || Input.GetButtonDown("KBPause")) && timer < 0)
        {
            timer = .5f;
            for (int i = 0; i < 1; i++)
            {
                Credits4Highlight[i].SetActive(false);
            }

            if (soundSource.isPlaying)
                soundSource.Stop();
            soundSource.clip = selectSound;
            soundSource.Play();

            switch (currentSelection)
            {
                default:
                    {
                        SelectRects[0] = new Rect(516, 125, 300, 150);
                        SelectRects[1] = new Rect(516, 192, 300, 150);
                        SelectRects[2] = new Rect(516, 259, 300, 150);
                        SelectRects[3] = new Rect(516, 326, 300, 150);
                        SelectRects[4] = new Rect(516, 525, 300, 150);
                        SelectRects[5] = new Rect(516, 1867, 300, 150);
                        SelectRects[6] = new Rect(516, 1867, 300, 150);
                        Credits.SetActive(false);
                        CurrMenu = Menu.Main;
                        currentSelection = 2;
                        break;
                    }
            }
        }


    }


    void AchievementsMenu()
    {
        disablemouse = true;
        maxchoices = 2;
        choices[0] = -4.25f;
        choices[1] = -4.25f;
        choices[2] = -4.25f;
        Achievements.SetActive(true);

        achvState = currentSelection;

        switch (currentSelection)
        {
            //This is used for Highlighting the currently selected menu option
            case 0:
                {
                    for (int i = 0; i < 1; i++)
                    {
                        Achievements4Highlight[i].SetActive(false);
                    }
                    Achievements4Highlight[0].SetActive(true);
                    break;
                }
        }


        if ((Input.GetButtonDown("CMenuAccept") || Input.GetButtonDown("KBMenuAccept")) || (Input.GetKeyDown(KeyCode.Mouse0) && MouseCanClick) && timer < 0)
        {
            timer = 1;
            for (int i = 0; i < 1; i++)
            {
                Achievements4Highlight[i].SetActive(false);
            }

            if (soundSource.isPlaying)
                soundSource.Stop();
            soundSource.clip = selectSound;
            soundSource.Play();


            switch (currentSelection)
            {
                default:
                    {
                        SelectRects[0] = new Rect(516, 125, 300, 150);
                        SelectRects[1] = new Rect(516, 192, 300, 150);
                        SelectRects[2] = new Rect(516, 259, 300, 150);
                        SelectRects[3] = new Rect(516, 326, 300, 150);
                        SelectRects[4] = new Rect(516, 525, 300, 150);
                        SelectRects[5] = new Rect(516, 1867, 300, 150);
                        SelectRects[6] = new Rect(516, 1867, 300, 150);
                        Achievements.SetActive(false);
                        CurrMenu = Menu.Main;
                        currentSelection = 3;
                        disablemouse = false;
                        break;
                    }
            }
        }

        //GO Back to Main
        if ((Input.GetButtonDown("CMenuCancel") || Input.GetButtonDown("KBPause")) && timer < 0)
        {
            timer = .5f;
            for (int i = 0; i < 1; i++)
            {
                Achievements4Highlight[i].SetActive(false);
            }

            if (soundSource.isPlaying)
                soundSource.Stop();
            soundSource.clip = selectSound;
            soundSource.Play();

            switch (currentSelection)
            {
                default:
                    {
                        SelectRects[0] = new Rect(516, 125, 300, 150);
                        SelectRects[1] = new Rect(516, 192, 300, 150);
                        SelectRects[2] = new Rect(516, 259, 300, 150);
                        SelectRects[3] = new Rect(516, 326, 300, 150);
                        SelectRects[4] = new Rect(516, 525, 300, 150);
                        SelectRects[5] = new Rect(516, 1867, 300, 150);
                        SelectRects[6] = new Rect(516, 1867, 300, 150);
                        Achievements.SetActive(false);
                        CurrMenu = Menu.Main;
                        disablemouse = false;
                        currentSelection = 3;
                        break;
                    }
            }
        }
    }

    void ContinueMenu()
    {
        maxchoices = 2;
        choices[0] = 1.9f;
        choices[1] = 1f;
        choices[2] = -2.72f;
        ContinueText.SetActive(true);

        if (Application.platform != RuntimePlatform.OSXWebPlayer
    || Application.platform != RuntimePlatform.WindowsWebPlayer)
        {
            if (!File.Exists(Application.persistentDataPath + "/playerinfo.dat"))
            {
                continueForGraying.color = new Color(.25f, .25f, .25f);
                currentSelection = 1;
            }
        }


        switch (currentSelection)
        {
            //This is used for Highlighting the currently selected menu option
            case 0:
                {
                    for (int i = 0; i < 3; i++)
                    {
                        ContinueTextHighlight[i].SetActive(false);
                    }
                    ContinueTextHighlight[0].SetActive(true);
                    break;
                }
            case 1:
                {
                    for (int i = 0; i < 3; i++)
                    {
                        ContinueTextHighlight[i].SetActive(false);
                    }
                    ContinueTextHighlight[1].SetActive(true);
                    break;
                }
            case 2:
                {
                    for (int i = 0; i < 3; i++)
                    {
                        ContinueTextHighlight[i].SetActive(false);
                    }
                    ContinueTextHighlight[2].SetActive(true);
                    break;
                }
        }
        if ((Input.GetButtonDown("CMenuAccept") || Input.GetButtonDown("KBMenuAccept")) || (Input.GetKeyDown(KeyCode.Mouse0) && MouseCanClick) && timer < 0)
        {
            timer = 1;
            for (int i = 0; i < 3; i++)
            {
                ContinueTextHighlight[i].SetActive(false);
            }

            if (soundSource.isPlaying)
                soundSource.Stop();
            soundSource.clip = selectSound;
            soundSource.Play();


            switch (currentSelection)
            {
                case 0:
                    {
                        //CONTINUE PREVIOUS GAME
                        //  LoadStats();
                        if (theOptions.easyMode != true)
                        {

                            //Debug.Log(theOptions.GetComponent<SaveTest>().data.canyouload == true);
                            if (Application.platform != RuntimePlatform.OSXWebPlayer || Application.platform != RuntimePlatform.WindowsWebPlayer)
                            {
                                if (File.Exists(Application.persistentDataPath + "/playerinfo.dat"))
                                {
                                    theOptions.shouldload = true;
                                    LevelManager.Load("Game");
                                }
                            }
                        }
                    }
                    break;
                case 1:
                    {
                        ContinueText.SetActive(false);
                        CurrMenu = Menu.SelectDifficulty;
                        currentSelection = 0;
                        break;
                    }
                case 2:
                    {
                        SelectRects[0] = new Rect(516, 125, 300, 150);
                        SelectRects[1] = new Rect(516, 192, 300, 150);
                        SelectRects[2] = new Rect(516, 259, 300, 150);
                        SelectRects[3] = new Rect(516, 326, 300, 150);
                        SelectRects[4] = new Rect(516, 525, 300, 150);
                        SelectRects[5] = new Rect(516, 1867, 300, 150);
                        SelectRects[6] = new Rect(516, 1867, 300, 150);
                        ContinueText.SetActive(false);
                        CurrMenu = Menu.Main;
                        currentSelection = 0;
                        break;
                    }
            }
        }

        //GO Back to Main
        if ((Input.GetButtonDown("CMenuCancel") || Input.GetButtonDown("KBPause")) && timer < 0)
        {
            timer = .5f;
            for (int i = 0; i < 3; i++)
            {
                ContinueTextHighlight[i].SetActive(false);
            }

            if (soundSource.isPlaying)
                soundSource.Stop();
            soundSource.clip = selectSound;
            soundSource.Play();

            switch (currentSelection)
            {
                default:
                    {
                        SelectRects[0] = new Rect(516, 125, 300, 150);
                        SelectRects[1] = new Rect(516, 192, 300, 150);
                        SelectRects[2] = new Rect(516, 259, 300, 150);
                        SelectRects[3] = new Rect(516, 326, 300, 150);
                        SelectRects[4] = new Rect(516, 525, 300, 150);
                        SelectRects[5] = new Rect(516, 1867, 300, 150);
                        SelectRects[6] = new Rect(516, 1867, 300, 150);
                        ContinueText.SetActive(false);
                        CurrMenu = Menu.Main;
                        currentSelection = 3;
                        break;
                    }
            }
        }
    }

    void SelectDifficultyMenu()
    {
        maxchoices = 2;
        choices[0] = 1.9f;
        choices[1] = 1f;
        choices[2] = -2.72f;
        SelectDifficulty.SetActive(true);

        switch (currentSelection)
        {
            //This is used for Highlighting the currently selected menu option
            case 0:
                {
                    for (int i = 0; i < 3; i++)
                    {
                        SelectDifficulty4Highlight[i].SetActive(false);
                    }
                    SelectDifficulty4Highlight[0].SetActive(true);
                    break;
                }
            case 1:
                {
                    for (int i = 0; i < 3; i++)
                    {
                        SelectDifficulty4Highlight[i].SetActive(false);
                    }
                    SelectDifficulty4Highlight[1].SetActive(true);
                    break;
                }
            case 2:
                {
                    for (int i = 0; i < 3; i++)
                    {
                        SelectDifficulty4Highlight[i].SetActive(false);
                    }
                    SelectDifficulty4Highlight[2].SetActive(true);
                    break;
                }
        }
        if ((Input.GetButtonDown("CMenuAccept") || Input.GetButtonDown("KBMenuAccept")) || (Input.GetKeyDown(KeyCode.Mouse0) && MouseCanClick) && timer < 0)
        {
            timer = 1;
            for (int i = 0; i < 3; i++)
            {
                SelectDifficulty4Highlight[i].SetActive(false);
            }

            if (soundSource.isPlaying)
                soundSource.Stop();
            soundSource.clip = selectSound;
            soundSource.Play();

            switch (currentSelection)
            {
                case 0:
                    {
                        //EASY DIFFICULTY
                        LevelManager.Load("IntroCutscene");
                        theOptions.shouldload = false;
                        GameObject.FindObjectOfType<Options>().easyMode = true;
                        break;
                    }
                case 1:
                    {
                        //NORMAL DIFFICULTY
                        theOptions.shouldload = false;
                        LevelManager.Load("IntroCutscene");
                        GameObject.FindObjectOfType<Options>().easyMode = false;
                        break;
                    }
                case 2:
                    {
                        SelectRects[0] = new Rect(516, 125 + 70, 300, 150);
                        SelectRects[1] = new Rect(516, 125 + 140, 300, 150);
                        SelectRects[2] = new Rect(516, 525, 300, 150);
                        SelectRects[3] = new Rect(516, 1867, 300, 150);
                        SelectRects[4] = new Rect(516, 1867, 300, 150);
                        SelectRects[5] = new Rect(516, 1867, 300, 150);
                        SelectRects[6] = new Rect(516, 1867, 300, 150);
                        ContinueMenu();
                        SelectDifficulty.SetActive(false);
                        CurrMenu = Menu.Continue;
                        currentSelection = 0;
                        break;
                    }
            }
        }

        //GO Back to Main
        if ((Input.GetButtonDown("CMenuCancel") || Input.GetButtonDown("KBPause")) && timer < 0)
        {
            timer = .5f;
            for (int i = 0; i < 3; i++)
            {
                SelectDifficulty4Highlight[i].SetActive(false);
            }

            if (soundSource.isPlaying)
                soundSource.Stop();
            soundSource.clip = selectSound;
            soundSource.Play();

            switch (currentSelection)
            {
                default:
                    {
                        SelectRects[0] = new Rect(516, 125 + 70, 300, 150);
                        SelectRects[1] = new Rect(516, 125 + 140, 300, 150);
                        SelectRects[2] = new Rect(516, 525, 300, 150);
                        SelectRects[3] = new Rect(516, 1867, 300, 150);
                        SelectRects[4] = new Rect(516, 1867, 300, 150);
                        SelectRects[5] = new Rect(516, 1867, 300, 150);
                        SelectRects[6] = new Rect(516, 1867, 300, 150);
                        ContinueMenu();
                        SelectDifficulty.SetActive(false);
                        CurrMenu = Menu.Continue;
                        currentSelection = 0;
                        break;
                    }
            }
        }
    }


    //    void LoadStats()
    //    {
    //        playerhealth.text = "100";


    //        if (File.Exists(Application.persistentDataPath + "/playerinfo.dat"))
    //        {
    //            BinaryFormatter bin = new BinaryFormatter();
    //            FileStream file = File.Open(Application.persistentDataPath + "/playerinfo.dat", FileMode.Open);
    //            PlayerData data = (PlayerData)bin.Deserialize(file);
    //            playerhealth.text = data.health.ToString();
    //            playerlight.text = data.light.ToString();
    //            file.Close();


    //        }

    //    }

    //void LoadOptions()
    //{

    //    if (File.Exists(Application.persistentDataPath + "/optioninfo.dat"))
    //    {
    //        BinaryFormatter bin = new BinaryFormatter();
    //        FileStream file = File.Open(Application.persistentDataPath + "/optioninfo.dat", FileMode.Open);
    //        OptionData data = (OptionData)bin.Deserialize(file);
    //        theOptions.sfxVolume = data.sfxVolume;
    //        theOptions.musicVolume = data.musicVolume;
    //        file.Close();

    //    }
    //}
}
//    void Save()
//    {
//        if (Application.platform == RuntimePlatform.OSXWebPlayer
//           || Application.platform == RuntimePlatform.WindowsWebPlayer) 
//        {


//        }
//        else
//        {
//            BinaryFormatter bin = new BinaryFormatter();
//            FileStream file = File.Create(Application.persistentDataPath + "/playerinfo.dat");
//            PlayerData data = new PlayerData();
//            data.health = health;
//            data.light = theLight;
//            bin.Serialize(file, data);
//            file.Close();
//        }

//    }
//}


//[System.Serializable]
//class PlayerData
//{
//    public int health;
//    public int light;
//}

