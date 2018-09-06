using UnityEngine;
using System.Collections;

public class AchvPage : MonoBehaviour
{

    public GameObject lock1;
    public GameObject lock2;
    public GameObject lock3;
    public GameObject lock4;
    public GameObject lock5;
    public GameObject lock6;
    public GameObject lock7;
    public GameObject lock8;

    float timer;
    // Use this for initialization

    MainMenu theMenu;

    public int page;


    Options theoptions;
    void Start()
    {
        timer = 0;
        theoptions = GameObject.Find("TheOptions").GetComponent<Options>();
        theMenu = GameObject.Find("Menu").GetComponent<MainMenu>();

    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = 0.3f;

            if (page == 1)
            {

                if (theoptions.achievements[0] == true)
                {
                    lock1.SetActive(false);
                }
                else
                {
                    lock1.SetActive(true);
                }

                if (theoptions.achievements[1] == true)
                {
                    lock2.SetActive(false);
                }
                else
                {
                    lock2.SetActive(true);
                }

                if (theoptions.achievements[2] == true)
                {
                    lock3.SetActive(false);
                }
                else
                {
                    lock3.SetActive(true);
                }

                if (theoptions.achievements[3] == true)
                {
                    lock4.SetActive(false);
                }
                else
                {
                    lock4.SetActive(true);
                }

                if (theoptions.achievements[4] == true)
                {
                    lock5.SetActive(false);
                }
                else
                {
                    lock5.SetActive(true);
                }

                if (theoptions.achievements[5] == true)
                {
                    lock6.SetActive(false);
                }
                else
                {
                    lock6.SetActive(true);
                }

                if (theoptions.achievements[6] == true)
                {
                    lock7.SetActive(false);
                }
                else
                {
                    lock7.SetActive(true);
                }

                if (theoptions.achievements[7] == true)
                {
                    lock8.SetActive(false);
                }
                else
                {
                    lock8.SetActive(true);
                }

            }
            else if (page == 2)
            {

                if (theoptions.achievements[8] == true)
                {
                    lock1.SetActive(false);
                }
                else
                {
                    lock1.SetActive(true);
                }

                if (theoptions.achievements[9] == true)
                {
                    lock2.SetActive(false);
                }
                else
                {
                    lock2.SetActive(true);
                }

                if (theoptions.achievements[10] == true)
                {
                    lock3.SetActive(false);
                }
                else
                {
                    lock3.SetActive(true);
                }

                if (theoptions.achievements[11] == true)
                {
                    lock4.SetActive(false);
                }
                else
                {
                    lock4.SetActive(true);
                }

                if (theoptions.achievements[12] == true)
                {
                    lock5.SetActive(false);
                }
                else
                {
                    lock5.SetActive(true);
                }

                if (theoptions.achievements[13] == true)
                {
                    lock6.SetActive(false);
                }
                else
                {
                    lock6.SetActive(true);
                }

                if (theoptions.achievements[14] == true)
                {
                    lock7.SetActive(false);
                }
                else
                {
                    lock7.SetActive(true);
                }

                if (theoptions.achievements[15] == true)
                {
                    lock8.SetActive(false);
                }
                else
                {
                    lock8.SetActive(true);
                }



            }

            else if (page == 3)
            {

                if (theoptions.achievements[16] == true)
                {
                    lock1.SetActive(false);
                }
                else
                {
                    lock1.SetActive(true);
                }

                if (theoptions.achievements[17] == true)
                {
                    lock2.SetActive(false);
                }
                else
                {
                    lock2.SetActive(true);
                }

                if (theoptions.achievements[18] == true)
                {
                    lock3.SetActive(false);
                }
                else
                {
                    lock3.SetActive(true);
                }

                if (theoptions.achievements[19] == true)
                {
                    lock4.SetActive(false);
                }
                else
                {
                    lock4.SetActive(true);
                }



            }

        }

    }
}
