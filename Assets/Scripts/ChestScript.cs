using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChestScript : MonoBehaviour
{

    public bool displaytooltips = false;
    public GameObject player;
    public GameObject loot;
    PlayerLight heroLight;
    Camera cameras;
    public GameObject TooltipWindow;
    RawImage ToolTipBack;
    public Vector3 ToolPOS;
    SetToolTipTexts ToolTipTexts;
    public GameObject temp;

    Options theoptions;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        heroLight = player.gameObject.GetComponent<PlayerLight>();
        cameras = GameObject.FindObjectOfType<Camera>();
        theoptions = GameObject.Find("TheOptions").GetComponent<Options>();
        temp = Instantiate(TooltipWindow);
        temp.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dist = transform.position - player.transform.position;
        if (dist.magnitude < 1.0f)
        {
            displaytooltips = true;
            temp.SetActive(true);
        }
        else
        {
            displaytooltips = false;
            temp.SetActive(false);
        }

        if (ToolTipBack != null && displaytooltips == true)
        {
            ToolPOS = Camera.main.WorldToScreenPoint(transform.position);
            float offsetY = Screen.height - 310;
            ToolTipBack.transform.localPosition = new Vector3(ToolPOS.x - 550, ToolPOS.y - offsetY, -5);
        }
    }

    void OnGUI()
    {
        //	if (displaytooltips)
        //	{
        //   string temp="Press E to open";
        //     GUI.Box(new Rect(cameras.WorldToScreenPoint(player.transform.position).x + 32, Screen.height - cameras.WorldToScreenPoint(player.transform.position).y, 100, 40), temp);

        //}

    }

    void Interact()
    {
        Vector3 dist = transform.position - player.transform.position;
        if (dist.magnitude < 1.0f)
        {

            theoptions.OpenChest();
            heroLight.LoseLight(5);
            gameObject.GetComponent<GenerateLoot>().Generateloot();
            //Instantiate (loot, transform.position, new Quaternion (0, 0, 0, 0));
            temp.SetActive(false);
            Destroy(temp);
            Destroy(this.gameObject);
        }

    }

    void DisplayTooltip()
    {
        displaytooltips = true;
        if (temp != null)
        {
            temp.SetActive(true);
            ToolTipBack = temp.GetComponentInChildren<RawImage>();

            ToolTipBack.SendMessage("ToolSetTexts", ToolTipTexts, SendMessageOptions.DontRequireReceiver);
        }

    }

    void DoNotDisplayTooltip()
    {
        displaytooltips = false;
        if (temp != null)
            temp.SetActive(false);

    }

}

