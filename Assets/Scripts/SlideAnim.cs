using UnityEngine;
using System.Collections;

public class SlideAnim : MonoBehaviour {


    public Sprite Image1;
    public Sprite Image2;
    SpriteRenderer CurrentSprite;
    float imageTimer;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	        imageTimer -= Time.deltaTime;
        
        if(imageTimer < 0)
        {
            imageTimer = .5f;
            if (CurrentSprite.sprite == Image1)
                CurrentSprite.sprite = Image1;
            else
                CurrentSprite.sprite = Image2;
        }

	}
}


