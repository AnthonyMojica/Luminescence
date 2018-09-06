using UnityEngine;
using System.Collections;

public class TreasureDoor : MonoBehaviour
{

    public bool isLocked;
    public bool displaytooltips = false;

    void OnGUI()
    {
        if (displaytooltips && isLocked)
        {
            GUI.Box(new Rect(0, 0, 140, 20), "Press E to open");
        }
    }

    void Start()
    {

    }

    void Update()
    {

    }

    void DisplayTooltip()
    {
        displaytooltips = true;
    }

    void DoNotDisplayTooltip()
    {
        displaytooltips = false;
    }
}
