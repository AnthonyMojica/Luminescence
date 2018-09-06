using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class IconBoot : MonoBehaviour {

    Image theImage;

    public Sprite blink;
    public Sprite charge;
    public Sprite decoy;
    public Sprite trail;
    public Sprite whirl;

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
            if (hEqp.equippedBoot == boot.Blink)
            {
                theImage.sprite = blink;
            }
            else if (hEqp.equippedBoot == boot.Charge)
            {
                theImage.sprite = charge;

            }
            else if (hEqp.equippedBoot == boot.Decoy)
            {
                theImage.sprite = decoy;

            }
            else if (hEqp.equippedBoot == boot.Trailblazer)
            {
                theImage.sprite = trail;

            }
            else if (hEqp.equippedBoot == boot.Whirlwind)
            {
                theImage.sprite = whirl;

            }
        }
    }
}
