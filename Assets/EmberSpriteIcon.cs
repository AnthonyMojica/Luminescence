using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EmberSpriteIcon : MonoBehaviour {

    Image theImage;

    public Sprite fire;
    public Sprite ice;
    public Sprite wind;
    public Sprite earth;
    public Sprite death;
    public Sprite life;


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
            if (hEqp.equippedEmber == ember.Fire)
            {
                theImage.sprite = fire;
            }
            else if (hEqp.equippedEmber == ember.Wind)
            {
                theImage.sprite = wind;

            }
            else if (hEqp.equippedEmber == ember.Life)
            {
                theImage.sprite = life;

            }
            else if (hEqp.equippedEmber == ember.Death)
            {
                theImage.sprite = death;

            }
            else if (hEqp.equippedEmber == ember.Ice)
            {
                theImage.sprite = ice;

            }
            else if (hEqp.equippedEmber == ember.Earth)
            {
                theImage.sprite = earth;

            }

        }
    }
}