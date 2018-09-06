using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndCutscene : MonoBehaviour
{

    public GameObject Particlecircle;
    public GameObject Particlecircle1;
    public GameObject Particlecircle2;
    public GameObject Particlecircle3;
    public GameObject ParticleSpell;
    public GameObject ParticleSpell1;
    public GameObject ParticleSpell2;
    public GameObject ParticleSpell3;
    public GameObject Crystal;
    public GameObject ExplosionCrystal;
    public Light pulselight;
    bool Lightup;
    bool Explosionoccured;
    bool HeroSpawned;
    bool Begin;
    float CHeroSpawnTimer;
    float CountDown;
    float LightChangeVar1;
    public GameObject Chero;
    GameObject theCutSceneHero;
    public Image FadeBlack;
    public Image FadeBlack2;
    public Text Text;
    bool TextOn;
    bool DoOnce1, DoOnce2, DoOnce3, DoOnce4, DoOnce5;
    // Use this for initialization
    void Start()
    {
        Lightup = false;
        HeroSpawned = false;
        LightChangeVar1 = 1.2f;
        Explosionoccured = false;
        Begin = true;
        TextOn = false;
        DoOnce1 = DoOnce2 = DoOnce3 = DoOnce4 = DoOnce5 = false;
        CHeroSpawnTimer = 3f;
        CountDown = 10000; //Dont countdown till ready
    }

    // Update is called once per frame
    void Update()
    {
        CHeroSpawnTimer -= Time.deltaTime;

        if (Begin)
            FadeIn();

        if (CHeroSpawnTimer < -5)
        {
            Begin = false;
        }
        if (Lightup)
        {
            pulselight.intensity += Time.deltaTime * LightChangeVar1;
        }
        else
        {
            pulselight.intensity -= Time.deltaTime * LightChangeVar1;
        }

        if (pulselight.intensity <= 1)
            Lightup = true;
        else if (pulselight.intensity >= 3)
            Lightup = false;

        if (CHeroSpawnTimer < 0 && HeroSpawned == false)
            SpawnCHero();
        if (theCutSceneHero != null)
        {
            if (theCutSceneHero.transform.position.y < -3)
            {
                theCutSceneHero.transform.position += new Vector3(0, 1.2f * Time.deltaTime, 0);
                print("Moving");
                CountDown = 1;
            }
            else
            {
                CountDown -= Time.deltaTime;
            }


            if (CountDown < 0)
            {
                ParticleSpell.SetActive(true);
            }
            if (CountDown < -1)
            {
                ParticleSpell1.SetActive(true);
            }
            if (CountDown < -1.5f)
            {
                ParticleSpell2.SetActive(true);

            }
            if (CountDown < -2f)
            {
                ParticleSpell3.SetActive(true);

            }
            if (CountDown < -4 && Explosionoccured == false)
            {
                Explosionoccured = true;
                Particlecircle.SetActive(true);
                Particlecircle1.SetActive(true);
                Particlecircle2.SetActive(true);
                Particlecircle3.SetActive(true);
                Crystal.SetActive(false);
                ExplosionCrystal.SetActive(false);
            }
            if (CountDown < -4f)
            {
                FadeOut();
                if (DoOnce1 == false)
                {
                    DoOnce1 = true;
                    ToggleTextFade();
                }
                if (TextOn)
                    FadeText();
                else
                    UnFadeText();
            }
            if (CountDown < -9.5f)
            {
                if (DoOnce2 == false)
                {
                    Text.text = "You have defeated Morrius and restored the world's light!"; ;
                    DoOnce2 = true;
                    ToggleTextFade();
                }
            }
            if (CountDown < -13)
            {
                if (DoOnce3 == false)
                {
                    ToggleTextFade();
                    DoOnce3 = true;
                }
            }
            if (CountDown < -15)
            {
                if (DoOnce4 == false)
                {
                    Text.text = "           Thank You for playing Luminescence!";
                    ToggleTextFade();
                    DoOnce4 = true;
                }
            }
            if (CountDown < -18)
            {
                if (DoOnce5 == false)
                {
                    ToggleTextFade();
                    DoOnce5 = true;
                }
            }

            if (CountDown < -21)
            {
                LevelManager.Load("Credits");
            }


        }




        if (Explosionoccured)
        {
            Particlecircle.transform.Rotate(new Vector3(0, 0, 1), 29);
            Particlecircle1.transform.Rotate(new Vector3(0, 0, 1), 19);
            Particlecircle2.transform.Rotate(new Vector3(0, 0, 1), 3);
            Particlecircle3.transform.Rotate(new Vector3(0, 0, 1), 9);
        }
    }

    void SpawnCHero()
    {
        Instantiate(Chero, new Vector3(0, -7.5f, -1), new Quaternion(0, 0, 0, 0));
        theCutSceneHero = GameObject.FindGameObjectWithTag("Player");
        HeroSpawned = true;
    }

    void FadeIn()
    {
        FadeBlack2.color -= new Color(0, 0, 0, .4f * Time.deltaTime);
        FadeBlack.color -= new Color(0, 0, 0, .3f * Time.deltaTime);
        // FadeText.color += new Color(0, 0, 0, .2f * Time.deltaTime);
    }

    void FadeOut()
    {
        FadeBlack.color += new Color(0, 0, 0, .4f * Time.deltaTime);
        //FadeText.color -= new Color(0, 0, 0, .3f * Time.deltaTime);
    }
    void FadeText()
    {
        FadeBlack2.color += new Color(0, 0, 0, .8f * Time.deltaTime);
        //FadeText.color -= new Color(0, 0, 0, .3f * Time.deltaTime);
    }
    void UnFadeText()
    {
        FadeBlack2.color -= new Color(0, 0, 0, .8f * Time.deltaTime);
        //FadeText.color -= new Color(0, 0, 0, .3f * Time.deltaTime);
    }

    void ToggleTextFade()
    {
        TextOn = !TextOn;
    }
}
