using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class AchievementGoAway : MonoBehaviour {

    Image theImage;
	// Use this for initialization
	void Start () {
        theImage = gameObject.GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        if (theImage.enabled == true)
        {
            Destroy(gameObject, 3.5f);
        }
	}
}
