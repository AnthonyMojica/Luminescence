using UnityEngine;
using System.Collections;

public class Figure8 : MonoBehaviour
{


    bool HIncrease;
    bool VIncrease;
    Vector3 OriginalPosition;
    // Use this for initialization
    void Start()
    {
        OriginalPosition = new Vector3(0, 0, 0);
        VIncrease = true;
        HIncrease = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.localPosition.x < -.4f)
        {
            HIncrease = true;
        }
        if (transform.localPosition.x > .4f)
        {
            HIncrease = false;
        }
        if (transform.localPosition.y < -.4f)
        {
            VIncrease = false;
        }
        if (transform.localPosition.y > .4f)
        {
            VIncrease = true;
        }


        if (HIncrease)
        {
            if (transform.localPosition.x < -.2f)
                transform.localPosition = new Vector3(transform.localPosition.x + (Time.deltaTime * 0.35f), transform.localPosition.y, -1.5f);
            else
                transform.localPosition = new Vector3(transform.localPosition.x + (Time.deltaTime * 0.40f), transform.localPosition.y, -1.5f);
        }
        else
        {
            if (transform.localPosition.x < -.2f)
                transform.localPosition = new Vector3(transform.localPosition.x - (Time.deltaTime * 0.35f), transform.localPosition.y, -1.5f);
            else
                transform.localPosition = new Vector3(transform.localPosition.x - (Time.deltaTime * 0.40f), transform.localPosition.y, -1.5f);


        }


        if (VIncrease)
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - (Time.deltaTime * 0.14f), -1.5f);
        else
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + (Time.deltaTime * 0.04f), -1.5f);



    }
}
