using UnityEngine;
using System.Collections;
using UnityEngine.UI;



public struct SetToolTipTexts
{
    public string _ItemName;
    public string _ItemStat1;
    public string _ItemAmount1;
    public string _ItemStat2;
    public string _ItemAmount2;
    public string _ExtraInfo;
};



public class UpdateToolText : MonoBehaviour
{

    public Text ItemName;
    public Text ItemStat1;
    public Text ItemAmount1;
    public Text ItemStat2;
    public Text ItemAmount2;
    public Text ExtraInfo;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent.name.Contains("Chest"))
        {
            GetComponentInChildren<Text>().text = ("Press " + (InputManager.controller ? "X" : "E") + " To Open");
        }
        if (transform.parent == null)
            Destroy(gameObject);
    }

    void ToolSetTexts(SetToolTipTexts theInfo)
    {
        ItemName.text = theInfo._ItemName;
        ItemStat1.text = theInfo._ItemStat1;
        ItemAmount1.text = theInfo._ItemAmount1;
        ItemStat2.text = theInfo._ItemStat2;
        ItemAmount2.text = theInfo._ItemAmount2;
        ExtraInfo.text = theInfo._ExtraInfo;
    }
}