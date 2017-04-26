using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour {

    //AudioSource
    public AudioSource audioHandler;

    //AudioClip
    public AudioClip keyPickUp;
    public AudioClip babySalatPickUp;
    public AudioClip doorPickUp;

    //Keys
    public int greenKey = 1;
    public int yellowKey = 1;
    public int redKey = 0;
    public int blueKey = 0;

    //Baby_Salats
    public int babySalat = 3;

    //Doors
    public int doors = 2;

	// Use this for initialization
	void Start () {
        keyPickUp = GetComponent<AudioClip>();
        babySalatPickUp = GetComponent<AudioClip>();
        doorPickUp = GetComponent<AudioClip>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void pickUpTrigger()
    {
        if (greenKey <= 0 )
        {
            audioHandler.PlayOneShot(keyPickUp);
        }
    }
}
