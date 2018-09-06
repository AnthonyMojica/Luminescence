using UnityEngine;
using System.Collections;

public class BlinkParticle : MonoBehaviour {

	public GameObject lightleftbehind;
	public GameObject[] enemies;
	public float timer=0.0f;
	public float range = 3.0f;
    public float damage;
    PlayerStats theStats;

	// Use this for initialization
	void Start () {
        theStats= GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        damage = 5.0f;
		enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach (var item in enemies) {
			if((transform.position-item.transform.position).magnitude<range)
			{
				item.GetComponent<Health>().LoseHealth(damage+theStats.spellModifier);
				item.GetComponent<Knockback>().SendMessage("GetWrecked",SendMessageOptions.DontRequireReceiver);
			}
			
		}
	

	
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		if (timer > 0.5f) 
		{

			Destroy (gameObject);
			Instantiate(lightleftbehind, transform.position, transform.rotation);  
		
		}
	
	}
}
