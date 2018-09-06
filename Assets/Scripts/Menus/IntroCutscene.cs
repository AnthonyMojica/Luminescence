using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class IntroCutscene : MonoBehaviour
{


    public Image FadeBlack;
    public Image FadeText;
    public GameObject Particlecircle;
    public Text FirstText;
    public Text SkipText;
    public GameObject Scene1; 
    public GameObject Scene2;
    public GameObject SprintingUndead;
    public GameObject RunningSkeleton;
    public GameObject FlyingWraith;
    public GameObject HappyCloud;
    public GameObject FloatingFairy;
    public GameObject ShadowyShadowSpawn;
    public GameObject CommandingCommander;
    public GameObject CHero;

//    bool FadeInBool;
    float timer;
//    float textTimer;
    int Stage;
    int i;

   
    string Text1 = "The Evil Dark Morrius, jealous of the world's happiness, stole the world's light and trapped it in a crystal in his lair.";
    string Text2 = "The world was left in everlasting darkness.";
    string Text3 = "With the world in darkness all hope seemed lost... Morrius's Dark Minions flooded the land.";
    string Text4 = "The only thing that seemed to keep the darkness away was an ancient relic infused with light.";
    string Text5 = "This Staff of Light was entrusted to a hero to find Morrius and restore light to the world.";

    // Use this for initialization
    void Start()
    {
        RenderSettings.ambientLight = new Color(137f/255f, 147f/255f, 167f/255f);
//        FadeInBool = true;
        timer = 3f;
//        textTimer = 2f;
        Stage = 1;
        i = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        //Fade In
        if (Stage == 1)
            FadeIn();

        if (Input.GetButtonDown("KBPause") || Input.GetButtonDown("CPause"))
        {
            LevelManager.Load("Tutorial");
        }
        //Begin Text
        if (Stage == 1 && timer < 0 && i < Text1.Length)
        {
            SkipText.text = " ";
            timer = .04f;
            ScrollingText();
            i++;
        }

        //Leave text on screen for 2 seconds
        if (i == Text1.Length)
        {
            Stage = 2;
            timer = 2f;
            i = 0;
        }

        //Erase First Text and begin second text
        if (Stage == 2 && timer < 0 && i < Text2.Length)
        {
            if(i == 0)
            FirstText.text = " ";
            timer = .04f;
            ScrollingText();
            i++;
        }
        
        //Begin Fading out
        if (i == Text2.Length && Stage == 2)
        {
            Stage = 3;
            timer = 6f;
        }
        if (Stage == 3)
            FadeOut();

        //Change to Scene 2 
        if(Stage == 3 && timer < 0)
        {
            //Change Scene
            RenderSettings.ambientLight = new Color(37f / 255f, 37f / 255f, 37f / 255f);
            Scene1.SetActive(false);
            FirstText.text = " ";
            Scene2.SetActive(true);
            Stage = 4;
            timer = 1;
            i = 0;
        }

        //Hold Black Screen
        if (Stage == 4 && timer < 0)
        {
            Stage = 5;
            timer = 3;
            SpawnTheHorde();
        }

        //Fade in Scene 2
        if( Stage == 5)
        {
            FadeIn();
        }

        //After Fade in timer, begin Text
        if (Stage == 5 && timer < 0)
        {
            ScrollingText();
            i++;
            timer = .04f;

            if (i == 50)
            {
                SpawnTheHorde();
                timer = 2;
            }
        }

        if(Stage == 5 && i == Text3.Length)
        {
            Stage = 6;
            timer = 2f;
        }

        if(Stage == 6 && timer < 0)
        {
            FirstText.text = " ";
            Stage = 7;
            i = 0;
        }

        if(Stage == 7 && timer < 0 )
        {
            timer = .04f;
            ScrollingText();
            i++;
        }

        if(Stage == 7 && i == Text4.Length)
        {
            Instantiate(CHero);
            Stage = 8;
            timer = 2f;
            i = 0;
        }


        if(Stage == 8 && timer < 0)
        {  

            Stage = 9;
            timer = 2f;
        }

        if(Stage == 9 && timer < 0)
        {
            if(i == 0)
                FirstText.text = " ";
            ScrollingText();
            i++;
            timer = .04f;
        }

        if(Stage == 9 && i == Text5.Length)
        {
            timer = .3f;
            Stage = 10;
        }

        if(Stage == 10 && timer < 0)
        {
            FadeOut();
        }

        if(Stage == 10 && timer < -7)
        {
            NextLevel();
        }



        Particlecircle.transform.Rotate(new Vector3(0, 0, 1), 1 * Time.deltaTime);
    }
    void FadeIn()
    {
        FadeBlack.color -= new Color(0, 0, 0, .3f * Time.deltaTime);
        FadeText.color += new Color(0, 0, 0, .2f * Time.deltaTime);
    }

    void FadeOut()
    {
        FadeBlack.color += new Color(0, 0, 0, .4f * Time.deltaTime);
        FadeText.color -= new Color(0, 0, 0, .3f * Time.deltaTime);
    }
    void ScrollingText()
    {
        if (Stage == 1)
            FirstText.text += Text1[i];

        if (Stage == 2)
            FirstText.text += Text2[i];

        if (Stage == 5)
            FirstText.text += Text3[i];

        if (Stage == 7)
            FirstText.text += Text4[i];

        if (Stage == 9)
            FirstText.text += Text5[i];

    }

    void SpawnTheHorde()
    {
        for(int p = 0; p < 8; p++)
        {
            Instantiate(SprintingUndead);
        }
        for (int p = 0; p < 4; p++)
        {
            Instantiate(RunningSkeleton);
        }
        for (int p = 0; p < 3; p++)
        {
            Instantiate(FlyingWraith);
        }
        for (int p = 0; p < 2; p++)
        {
            Instantiate(HappyCloud);
        }
        for (int p = 0; p < 3; p++)
        {
            Instantiate(FloatingFairy);
        }
        for (int p = 0; p < 4; p++)
        {
            Instantiate(ShadowyShadowSpawn);
        }
                for (int p = 0; p < 4; p++)
        {
            Instantiate(CommandingCommander);
        }


    }

    void NextLevel()
    {
        GameObject.Find("TheOptions").GetComponent<Options>().watchIntro = true;
        LevelManager.Load("Tutorial");
    }


}
