using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RoomEntryText : MonoBehaviour
{
    float currTime = 0.0f;
    public string toDisplay = "";
    bool increasing = true;
    bool done = false;
    Text theText;

    void Start()
    {
        theText = GetComponentInChildren<Text>();
    }

    void Update()
    {
        if (!done)
        {
            theText.text = toDisplay;
            theText.color = new Color(theText.color.r, theText.color.g, theText.color.b, currTime);
            if (increasing)
            {
                currTime += Time.deltaTime;
                if (currTime >= 1f)
                {
                    increasing = false;
                }
            }
            else
            {
                currTime -= Time.deltaTime;
                if (currTime <= 0.0f)
                {
                    done = true;
                }
            }
        }
        else
        {
            theText.color = new Color(theText.color.r, theText.color.g, theText.color.b, 0f);
        }
    }
}
