using UnityEngine;
using System.Collections;

public class PingMissile : MonoBehaviour {

    GameObject player;
    public AudioClip lightSound;

    AudioSource aPlayer;

    ParticleSystem particles;

    bool once;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        particles = gameObject.GetComponent<ParticleSystem>();
        aPlayer = gameObject.GetComponent<AudioSource>();
        once = true;
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 moveTo = (player.transform.position - transform.position).normalized;

        if (Mathf.Abs(transform.position.x - player.transform.position.x) <= 1.0f && Mathf.Abs(transform.position.y - player.transform.position.y) <= 1.0f)
        {
            moveTo = moveTo * Time.deltaTime * 25;

        }
        else
        {
            moveTo = moveTo * Time.deltaTime * 7;

        }



        transform.position = new Vector3(moveTo.x + transform.position.x, moveTo.y + transform.position.y, -3.1f);

        
        if ( Mathf.Abs(transform.position.x - player.transform.position.x) <= 0.3f && Mathf.Abs(transform.position.y - player.transform.position.y) <= 0.3f )
        {
            if (once)
            {
                aPlayer.PlayOneShot(lightSound);

                particles.enableEmission = false;
                Destroy(gameObject, 1);
                once = false;
            }
        }
	}
}
