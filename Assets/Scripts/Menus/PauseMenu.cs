using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
    bool isPaused = false;
    GameObject pauseCanvas;
    int currOption = 0;
    int numOptions = 3;
    PlayerEquipment heroEquipment;
    bool axisChanged = false;
    public GameObject SelectorRemains;

    int currOptionOption = 0;
    int numOptionOptions = 5;

    public Text resume;
    public Text options;
    public Text quit;

    public Text sVolume;
    public Text mVolume;
    public Text fullscreen;
    public Text controlMode;
    public Text back;

    bool optionsMode = false;

    Rect resumeRect;
    Rect optionsRect;
    Rect quitRect;

    Rect sfxRect;
    Rect sfxLeftRect;
    Rect sfxRightRect;
    Rect musicRect;
    Rect musicLeftRect;
    Rect musicRightRect;
    Rect fullscreenRect;
    Rect controlsRect;
    Rect backRect;

    Vector2 mousePrevPos;

    //    Vector3 RemainsPOS;
    void Start()
    {
        //        RemainsPOS = new Vector3(1, 1, 1);
        pauseCanvas = GameObject.FindGameObjectWithTag("PauseCanvas");
        heroEquipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
        pauseCanvas.SetActive(false);
        isPaused = false;
        sVolume.enabled = false;
        mVolume.enabled = false;
        fullscreen.enabled = false;
        controlMode.enabled = false;
        back.enabled = false;
        resumeRect = new Rect(570, 410, 140, 50);
        optionsRect = new Rect(570, 360, 140, 50);
        if (Application.loadedLevelName == "Tutorial")
            quitRect = new Rect(600, 250, 80, 50);
        else
            quitRect = new Rect(540, 250, 200, 50);
        sfxRect = new Rect(500, 465, 280, 50);
        sfxLeftRect = new Rect(715, 465, 30, 50);
        sfxRightRect = new Rect(750, 465, 30, 50);
        musicRect = new Rect(490, 420, 300, 45);
        musicLeftRect = new Rect(730, 420, 30, 45);
        musicRightRect = new Rect(765, 420, 30, 45);
        fullscreenRect = new Rect(560, 360, 160, 50);
        controlsRect = new Rect(480, 310, 320, 50);
        backRect = new Rect(600, 255, 80, 45);
    }

    void Update()
    {
        if (isPaused)
        {
            Debug.Log(Input.mousePosition);
            //if (InputManager.controller)
            {
                #region Controller Input
                if (axisChanged && Input.GetAxis("CLSVertical") == 0.0f && Input.GetAxis("CDPadVertical") == 0.0f
                    && Input.GetAxis("CLSHorizontal") == 0.0f && Input.GetAxis("CDPadHorizontal") == 0.0f
                    && Input.GetAxis("KBVertical") == 0.0f && Input.GetAxis("KBHorizontal") == 0f)
                {
                    axisChanged = false;
                }
                if (!axisChanged && (Input.GetAxis("CLSVertical") > 0.0f || Input.GetAxis("CDPadVertical") > 0.0f))
                {
                    if (!optionsMode)
                        currOption--;
                    else
                        currOptionOption--;
                    axisChanged = true;
                }
                if (!axisChanged && (Input.GetAxis("CLSVertical") < 0.0f || Input.GetAxis("CDPadVertical") < 0.0f))
                {
                    if (!optionsMode)
                        currOption++;
                    else
                        currOptionOption++;
                    axisChanged = true;
                }
                #endregion
            }
            //else
            {
                #region KB/M Input
                if (!axisChanged && Input.GetAxis("KBVertical") > 0.0f)
                {
                    if (!optionsMode)
                        currOption--;
                    else
                        currOptionOption--;
                    axisChanged = true;
                }
                if (!axisChanged && Input.GetAxis("KBVertical") < 0.0f)
                {
                    if (!optionsMode)
                        currOption++;
                    else
                        currOptionOption++;
                    axisChanged = true;
                }
                #endregion
            }
            #region Wrapping
            if (currOption < 0)
                currOption = numOptions - 1;
            else if (currOption >= numOptions)
                currOption = 0;

            if (currOptionOption < 0)
                currOptionOption = numOptionOptions - 1;
            else if (currOptionOption >= numOptionOptions)
                currOptionOption = 0;
            #endregion

            if (!optionsMode)
            {
                if (mousePrevPos.x != Input.mousePosition.x || mousePrevPos.y != Input.mousePosition.y)
                {
                    if (resumeRect.Contains(Input.mousePosition))
                        currOption = 0;
                    if (optionsRect.Contains(Input.mousePosition))
                        currOption = 1;
                    if (quitRect.Contains(Input.mousePosition))
                        currOption = 2;
                }
                switch (currOption)
                {
                    case 0:
                        #region Resume
                        SelectorRemains.transform.localPosition = new Vector3(-100f, 80f, -5.1f);
                        if (Input.GetButtonDown("CMenuAccept"))
                        {
                            // Resume Code Here
                            GameObject[] allObjects;
                            allObjects = GameObject.FindObjectsOfType<GameObject>();
                            //isPaused = !isPaused;
                            foreach (GameObject obj in allObjects)
                            {
                                obj.SendMessage("UnPause", SendMessageOptions.DontRequireReceiver);
                            }
                            GameObject.Find("InputManager").GetComponent<InputManager>().isPaused = false;
                        }
                        else if (Input.GetButtonDown("KBMenuAccept"))
                        {
                            // Resume Code Here
                            GameObject[] allObjects;
                            allObjects = GameObject.FindObjectsOfType<GameObject>();
                            //isPaused = !isPaused;
                            foreach (GameObject obj in allObjects)
                            {
                                obj.SendMessage("UnPause", SendMessageOptions.DontRequireReceiver);
                            }
                            GameObject.Find("InputManager").GetComponent<InputManager>().isPaused = false;
                        }
                        if (resumeRect.Contains(Input.mousePosition) && Input.GetButtonDown("KBSpells"))
                        {
                            // Resume Code Here
                            GameObject[] allObjects;
                            allObjects = GameObject.FindObjectsOfType<GameObject>();
                            //isPaused = !isPaused;
                            foreach (GameObject obj in allObjects)
                            {
                                obj.SendMessage("UnPause", SendMessageOptions.DontRequireReceiver);
                            }
                            GameObject.Find("InputManager").GetComponent<InputManager>().isPaused = false;
                        }
                        break;
                        #endregion
                    case 1:
                        #region Options Menu Stuff
                        SelectorRemains.transform.localPosition = new Vector3(-105f, 30f, -5.1f);
                        if (Input.GetButtonDown("CMenuAccept"))
                        {
                            // Options Code Here
                            SwapMode();
                        }
                        else if (Input.GetButtonDown("KBMenuAccept"))
                        {
                            // Options Code Here
                            SwapMode();
                        }
                        if (optionsRect.Contains(Input.mousePosition) && Input.GetButtonDown("KBSpells"))
                            SwapMode();
                        break;
                        #endregion
                    case 2:
                        #region Quit
                        SelectorRemains.transform.localPosition = new Vector3(-70f, -85f, -5.1f);
                        if (Application.loadedLevelName == "Game")
                            SelectorRemains.transform.localPosition = new Vector3(-120f, -85f, -5.1f);
                        if (Input.GetButtonDown("CMenuAccept"))
                        {
                            // Save & Quit Code Here
                            GameObject dungeon = GameObject.FindGameObjectWithTag("Dungeon");
                            if (dungeon)
                            {
                                RoomGeneration gen = dungeon.GetComponent<RoomGeneration>();
                                if (GameObject.FindGameObjectsWithTag("Enemy").Length + GameObject.FindGameObjectsWithTag("Invincible").Length > 0)
                                {
                                    gen.finalRoomInfoArray[gen.currentRoom].beenThere = false;
                                    if (gen.currentRoom > 0)
                                        gen.currentRoom -= 1;
                                }
                                GameObject.FindObjectOfType<Options>().GetComponent<SaveTest>().Save();
                            }
                            Application.LoadLevel("MainMenu");
                        }
                        else if (Input.GetButtonDown("KBMenuAccept"))
                        {
                            // Save & Quit Code Here
                            GameObject dungeon = GameObject.FindGameObjectWithTag("Dungeon");
                            if (dungeon)
                            {
                                RoomGeneration gen = dungeon.GetComponent<RoomGeneration>();
                                if (GameObject.FindGameObjectsWithTag("Enemy").Length + GameObject.FindGameObjectsWithTag("Invincible").Length > 0)
                                {
                                    gen.finalRoomInfoArray[gen.currentRoom].beenThere = false;
                                    if (gen.currentRoom > 0)
                                        gen.currentRoom -= 1;
                                }
                                GameObject.FindObjectOfType<Options>().GetComponent<SaveTest>().Save();
                            }
                            Application.LoadLevel("MainMenu");
                        }
                        if (quitRect.Contains(Input.mousePosition) && Input.GetButtonDown("KBSpells"))
                        {
                            GameObject dungeon = GameObject.FindGameObjectWithTag("Dungeon");
                            if (dungeon)
                            {
                                RoomGeneration gen = dungeon.GetComponent<RoomGeneration>();
                                if (GameObject.FindGameObjectsWithTag("Enemy").Length + GameObject.FindGameObjectsWithTag("Invincible").Length > 0)
                                {
                                    gen.finalRoomInfoArray[gen.currentRoom].beenThere = false;
                                    if (gen.currentRoom > 0)
                                        gen.currentRoom -= 1;
                                }
                                GameObject.FindObjectOfType<Options>().GetComponent<SaveTest>().Save();
                            }
                            Application.LoadLevel("MainMenu");
                        }
                        break;
                        #endregion
                    default:
                        break;
                }
            }
            else
            {
                if (mousePrevPos.x != Input.mousePosition.x || mousePrevPos.y != Input.mousePosition.y)
                {
                    if (sfxRect.Contains(Input.mousePosition))
                        currOptionOption = 0;
                    if (musicRect.Contains(Input.mousePosition))
                        currOptionOption = 1;
                    if (fullscreenRect.Contains(Input.mousePosition))
                        currOptionOption = 2;
                    if (controlsRect.Contains(Input.mousePosition))
                        currOptionOption = 3;
                    if (backRect.Contains(Input.mousePosition))
                        currOptionOption = 4;
                }                
                switch (currOptionOption)
                {
                    case 0:
                        #region SFX
                        SelectorRemains.transform.localPosition = new Vector3(-150f, 130f, -5.1f);
                        if (!axisChanged && (Input.GetAxis("CLSHorizontal") < 0.0f || Input.GetAxis("CDPadHorizontal") < 0.0f))
                        {
                            // Decrease SFX Volume
                            GameObject.FindObjectOfType<Options>().sfxDecrease();
                            sVolume.text = "SFX Volume: " + GameObject.FindObjectOfType<Options>().sfxVolume.ToString();
                            axisChanged = true;
                        }
                        else if (!axisChanged && (Input.GetAxis("CLSHorizontal") > 0.0f || Input.GetAxis("CDPadHorizontal") > 0.0f))
                        {
                            // Increase SFX Volume
                            GameObject.FindObjectOfType<Options>().sfxIncrease();
                            sVolume.text = "SFX Volume: " + GameObject.FindObjectOfType<Options>().sfxVolume.ToString();
                            axisChanged = true;
                        }
                        else if (!axisChanged && Input.GetAxis("KBHorizontal") < 0.0f)
                        {
                            // Decrease SFX Volume
                            GameObject.FindObjectOfType<Options>().sfxDecrease();
                            sVolume.text = "SFX Volume: " + GameObject.FindObjectOfType<Options>().sfxVolume.ToString();
                            axisChanged = true;
                        }
                        else if (!axisChanged && Input.GetAxis("KBHorizontal") > 0.0f)
                        {
                            // Increase SFX Volume
                            GameObject.FindObjectOfType<Options>().sfxIncrease();
                            sVolume.text = "SFX Volume: " + GameObject.FindObjectOfType<Options>().sfxVolume.ToString();
                            axisChanged = true;
                        }
                        if (sfxLeftRect.Contains(Input.mousePosition) && Input.GetButtonDown("KBSpells"))
                        {
                            GameObject.FindObjectOfType<Options>().sfxDecrease();
                            sVolume.text = "SFX Volume: " + GameObject.FindObjectOfType<Options>().sfxVolume.ToString();
                        }
                        if (sfxRightRect.Contains(Input.mousePosition) && Input.GetButtonDown("KBSpells"))
                        {
                            GameObject.FindObjectOfType<Options>().sfxIncrease();
                            sVolume.text = "SFX Volume: " + GameObject.FindObjectOfType<Options>().sfxVolume.ToString();
                        }
                        #endregion
                        break;
                    case 1:
                        #region Music
                        SelectorRemains.transform.localPosition = new Vector3(-160f, 80f, -5.1f);
                        if (!axisChanged && (Input.GetAxis("CLSHorizontal") < 0.0f || Input.GetAxis("CDPadHorizontal") < 0.0f))
                        {
                            // Decrease Music Volume
                            GameObject.FindObjectOfType<Options>().musicDecrease();
                            mVolume.text = "Music Volume: " + GameObject.FindObjectOfType<Options>().musicVolume.ToString();
                            axisChanged = true;
                        }
                        else if (!axisChanged && (Input.GetAxis("CLSHorizontal") > 0.0f || Input.GetAxis("CDPadHorizontal") > 0.0f))
                        {
                            // Increase Music Volume
                            GameObject.FindObjectOfType<Options>().musicIncrease();
                            mVolume.text = "Music Volume: " + GameObject.FindObjectOfType<Options>().musicVolume.ToString();
                            axisChanged = true;
                        }
                        else if (!axisChanged && Input.GetAxis("KBHorizontal") < 0.0f)
                        {
                            // Decrease Music Volume
                            GameObject.FindObjectOfType<Options>().musicDecrease();
                            mVolume.text = "Music Volume: " + GameObject.FindObjectOfType<Options>().musicVolume.ToString();
                            axisChanged = true;
                        }
                        else if (!axisChanged && Input.GetAxis("KBHorizontal") > 0.0f)
                        {
                            // Increase Music Volume
                            GameObject.FindObjectOfType<Options>().musicIncrease();
                            mVolume.text = "Music Volume: " + GameObject.FindObjectOfType<Options>().musicVolume.ToString();
                            axisChanged = true;
                        }
                        if (musicLeftRect.Contains(Input.mousePosition) && Input.GetButtonDown("KBSpells"))
                        {
                            GameObject.FindObjectOfType<Options>().musicDecrease();
                            mVolume.text = "Music Volume: " + GameObject.FindObjectOfType<Options>().musicVolume.ToString();
                        }
                        if (musicRightRect.Contains(Input.mousePosition) && Input.GetButtonDown("KBSpells"))
                        {
                            GameObject.FindObjectOfType<Options>().musicIncrease();
                            mVolume.text = "Music Volume: " + GameObject.FindObjectOfType<Options>().musicVolume.ToString();
                        }
                        #endregion
                        break;
                    case 2:
                        #region Fullscreen
                        SelectorRemains.transform.localPosition = new Vector3(-100f, 30f, -5.1f);
                        if (Input.GetButtonDown("CMenuAccept"))
                        {
                            if (Screen.fullScreen)
                            {
                                Screen.SetResolution(1280, 720, !Screen.fullScreen);
                            }
                            else
                            {
                                Screen.SetResolution(1280, 720, Screen.fullScreen);
                            }
                            Screen.fullScreen = !Screen.fullScreen;
                        }
                        else if (Input.GetButtonDown("KBMenuAccept"))
                        {
                            if (Screen.fullScreen)
                            {
                                Screen.SetResolution(1280, 720, !Screen.fullScreen);
                            }
                            else
                            {
                                Screen.SetResolution(1280, 720, Screen.fullScreen);
                            }
                            Screen.fullScreen = !Screen.fullScreen;
                        }
                        if (fullscreenRect.Contains(Input.mousePosition) && Input.GetButtonDown("KBSpells"))
                        {
                            if (Screen.fullScreen)
                            {
                                Screen.SetResolution(1280, 720, !Screen.fullScreen);
                            }
                            else
                            {
                                Screen.SetResolution(1280, 720, Screen.fullScreen);
                            }
                            Screen.fullScreen = !Screen.fullScreen;
                        }
                        #endregion
                        break;
                    case 3:
                        #region Control Mode
                        SelectorRemains.transform.localPosition = new Vector3(-180f, -25f, -5.1f);
                        if (Input.GetButtonDown("CMenuAccept"))
                        {
                            InputManager.controller = !InputManager.controller;
                            controlMode.text = "Controls: " + (InputManager.controller ? "Controller" : "KB/Mouse");
                        }
                        else if (Input.GetButtonDown("KBMenuAccept"))
                        {
                            InputManager.controller = !InputManager.controller;
                            controlMode.text = "Controls: " + (InputManager.controller ? "Controller" : "KB/Mouse");
                        }
                        if (controlsRect.Contains(Input.mousePosition) && Input.GetButtonDown("KBSpells"))
                        {
                            InputManager.controller = !InputManager.controller;
                            controlMode.text = "Controls: " + (InputManager.controller ? "Controller" : "KB/Mouse");
                        }
                        #endregion
                        break;
                    case 4:
                        #region Back
                        SelectorRemains.transform.localPosition = new Vector3(-70f, -85f, -5.1f);
                        if (InputManager.controller && Input.GetButtonDown("CMenuAccept"))
                        {
                            SwapMode();
                        }
                        else if (!InputManager.controller && Input.GetButtonDown("KBMenuAccept"))
                        {
                            SwapMode();
                        }
                        if (backRect.Contains(Input.mousePosition) && Input.GetButtonDown("KBSpells"))
                            SwapMode();
                        #endregion
                        break;
                    default:
                        break;
                }
            }
        }
        mousePrevPos = Input.mousePosition;
    }

    void SwapMode()
    {
        optionsMode = !optionsMode;
        if (optionsMode)
        {
            resume.enabled = false;
            options.enabled = false;
            quit.enabled = false;

            sVolume.enabled = true;
            sVolume.text = "SFX Volume: " + GameObject.FindObjectOfType<Options>().sfxVolume.ToString();
            mVolume.enabled = true;
            mVolume.text = "Music Volume: " + GameObject.FindObjectOfType<Options>().musicVolume.ToString();
            fullscreen.enabled = true;
            controlMode.enabled = true;
            if (!InputManager.controller)
            {
                controlMode.text = "Controls: KB/Mouse";
            }
            else
            {
                controlMode.text = "Controls: Controller";
            }
            back.enabled = true;
        }
        else
        {
            resume.enabled = true;
            options.enabled = true;
            quit.enabled = true;

            sVolume.enabled = false;
            mVolume.enabled = false;
            fullscreen.enabled = false;
            controlMode.enabled = false;
            back.enabled = false;
        }
    }

    void Pause()
    {
        isPaused = true;
        pauseCanvas.SetActive(true);
        heroEquipment.paused = true;
        if (optionsMode)
            SwapMode();
    }

    void UnPause()
    {
        isPaused = false;
        pauseCanvas.SetActive(false);
        heroEquipment.paused = false;
    }
}
