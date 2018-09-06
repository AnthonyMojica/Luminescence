using UnityEngine;
using System.Collections;

public class Reinforced : MonoBehaviour {

	// Use this for initialization
    public float timer;
    public float timerref;
    public float timer2;
    public float timer2ref;
    public GameObject symbol;
    public bool isReinforced = false;
    public GameObject temp;
	GameObject player;
    Color color;

	void Start () 
    {
		player = GameObject.FindGameObjectWithTag ("Player");
        symbol.transform.position = new Vector3(0, 0, -1);
        timer = 0.25f;
        timer2 = 0.0f;
        timer2ref =3.0f;
        timerref = timer;
       color= symbol.GetComponent<SpriteRenderer>().color  ;
       color.a = 0.5f;
       symbol.GetComponent<SpriteRenderer>().color = color;
          temp=  (GameObject)Instantiate(symbol, transform.position, transform.rotation);
          temp.SetActive(false);
  
	}
	
	// Update is called once per frame
	void Update ()
    {
   if(gameObject==null)
            Destroy(temp);
		if (temp != null) {
			if (isReinforced) {
				temp.gameObject.transform.position = transform.position;
				if (timer2 < 0.0f) {
					temp.SetActive (true);
					timer2 = 0.0f;
					color.a = 0.0f;

				} else if (timer2 == 0) {
					if (timer > 0.0f) {
						temp.transform.localScale += new Vector3 (0.01F, 0.01f, 0f);
						color.a += 0.05f;
						symbol.GetComponent<SpriteRenderer> ().color = color;
					} else {
						timer = timerref;
						temp.SetActive (false);
						temp.transform.localScale = new Vector3 (0.1f, 0.1f, 0);
						timer2 = timer2ref;
					}
					timer -= Time.deltaTime;
				} else {
					timer2 -= Time.deltaTime;
				}

			}
		}
	}

    void Reinforce()
    {
       
            isReinforced = true;
       

    }

    void UnReinforce()
    {
       
            isReinforced = false;
            temp.SetActive(false);
         

    }
	void UnReinforcin()
	{
		Destroy (temp);

	}
}
