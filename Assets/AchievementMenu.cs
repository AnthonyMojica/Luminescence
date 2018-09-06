using UnityEngine;
using System.Collections;

public class AchievementMenu : MonoBehaviour {

    public GameObject page1;
    public GameObject page2;
    public GameObject page3;

    float timer;
	// Use this for initialization

    MainMenu theMenu;
	void Start () {
        timer = 0;
        theMenu = GameObject.Find("Menu").GetComponent<MainMenu>();
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if (timer <=0)
        {
            timer = 0.1f;

            if (theMenu.achvState == 0)
            {
                page1.SetActive(true);
                page2.SetActive(false);
                page3.SetActive(false);
            }
            else if (theMenu.achvState == 1)
            {
                page1.SetActive(false);
                page2.SetActive(true);
                page3.SetActive(false);
            }
            else if (theMenu.achvState == 2)
            {
                page1.SetActive(false);
                page2.SetActive(false);
                page3.SetActive(true);
            }

        }
    
	}
}
