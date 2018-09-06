using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class IconSpell : MonoBehaviour
{

    Image theImage;

    public Sprite orb;
    public Sprite bolt;
    public Sprite sing;
    public Sprite mine;
    public Sprite snare;
    public Sprite chain;
    public Sprite blast;

    float timer;
    PlayerEquipment hEqp;
    // Use this for initialization
    void Start()
    {
        theImage = gameObject.GetComponent<Image>();
        hEqp = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
        timer = 0.25f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 0.25f)
        {
            if (hEqp.equippedAccessory == accessory.BoltOfLight)
            {
                theImage.sprite = bolt;
            }
            else if (hEqp.equippedAccessory == accessory.OrbOfLight)
            {
                theImage.sprite = orb;

            }
            else if (hEqp.equippedAccessory == accessory.LightMine)
            {
                theImage.sprite = mine;

            }
            else if (hEqp.equippedAccessory == accessory.Snare)
            {
                theImage.sprite = snare;

            }
            else if (hEqp.equippedAccessory == accessory.Singularity)
            {
                theImage.sprite = sing;

            }
            else if (hEqp.equippedAccessory == accessory.BlastOfLight)
            {
                theImage.sprite = blast;

            }
            else if (hEqp.equippedAccessory == accessory.ChainLightning)
            {
                theImage.sprite = chain;

            }
        }
    }
}
