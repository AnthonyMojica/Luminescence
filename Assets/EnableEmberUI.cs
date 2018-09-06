using UnityEngine;
using System.Collections;

public class EnableEmberUI : MonoBehaviour
{

    public GameObject emberUI;

    PlayerEquipment heroEquip;
    float timer;

    // Use this for initialization
    void Start()
    {
        heroEquip = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
        timer = 1;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 0.5f)
        {
            if (heroEquip.emberDurability <= 0)
            {
                emberUI.SetActive(false);

            }
            else
            {
                emberUI.SetActive(true);
            }
            timer = 0;
        }
    }
}
