using UnityEngine;
using System.Collections;
//using UnityEditor;


public class ExpPlacer : MonoBehaviour
{
    float timer;

    public GameObject smlExp;
    public GameObject medExp;
    public GameObject lrgExp;

    bool once;

    public bool returnsLight;

    // Use this for initialization
    void Start()
    {
        timer = 0;
        once = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * 25 * Time.deltaTime;
        timer += Time.deltaTime;
    }

    void Detonate()
    {
        if (once)
        {
            GameObject expl;

            if (timer <= 0.05f)
            {
                expl = (GameObject)Instantiate(smlExp, transform.position, transform.rotation);
            }
            else if (timer <= 0.1f)
            {
                expl = (GameObject)Instantiate(medExp, transform.position, transform.rotation);

            }
            else
            {
                expl = (GameObject)Instantiate(lrgExp, transform.position, transform.rotation);

            }

            expl.GetComponent<BoltExp>().dropLight = returnsLight;

            once = false;
            Destroy(gameObject);
        }
    }
}
