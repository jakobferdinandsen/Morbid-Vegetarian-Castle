using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour
{

    //AudioSource
    public AudioSource audioHandler;

    //AudioClip
    public AudioClip keyPickUp;             //COMPLETE
    public AudioClip babySalatPickUp;       //COMPLETE
    public AudioClip doorPickUp;            //COMPLETE
    public AudioClip movement1;             //COMPLETE
    public AudioClip movement2;             //COMPLETE
    public AudioClip rangedHit;             //COMPLETE
    public AudioClip rangedHit2;            //COMPLETE
    public AudioClip meleeHit;

    private bool rangedHitSound = false;
    private bool movementSound = false;

    // Use this for initialization
    void Start()
    {
        audioHandler = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        movementHandler();
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        //Keys PickUp
        if (coll.gameObject.tag == "greenKey" || coll.gameObject.tag == "yellowKey")
        {
            audioHandler.PlayOneShot(keyPickUp);
        }

        //Baby_Salat PickUP
        if (coll.gameObject.tag == "Baby_salat")
        {
            audioHandler.PlayOneShot(babySalatPickUp);
        }

        //Door_removed
        if (coll.gameObject.tag == "Green_Door" || coll.gameObject.tag == "Yellow_door")
        {
            audioHandler.PlayOneShot(doorPickUp);
        }

        //Ranged Hit
        if (Input.GetMouseButton(0) && rangedHitSound == true)
        {
            audioHandler.PlayOneShot(rangedHit);
            rangedHitSound = false;
        }
        else if (Input.GetMouseButton(0) && rangedHitSound == false)
        {
            audioHandler.PlayOneShot(rangedHit2);
            rangedHitSound = true;
        }

        //Melee Hit
        if (Input.GetMouseButton(1))
        {
            audioHandler.PlayOneShot(meleeHit);
        }
    }

    void movementHandler()
    {
        if (!audioHandler.isPlaying)
        {
            //Player_Movement
            //Up-arrow
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) && movementSound == true)
            {
                audioHandler.PlayOneShot(movement1);
                movementSound = false;
            }
            else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) && movementSound == false)
            {
                audioHandler.PlayOneShot(movement2);
                movementSound = true;
            }

            //Down-arrow
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) && movementSound == true)
            {
                audioHandler.PlayOneShot(movement1);
                movementSound = false;
            }
            else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) && movementSound == false)
            {
                audioHandler.PlayOneShot(movement2);
                movementSound = true;
            }

            //Left-arrow
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) && movementSound == true)
            {
                audioHandler.PlayOneShot(movement1);
                movementSound = false;
            }
            else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) && movementSound == false)
            {
                audioHandler.PlayOneShot(movement2);
                movementSound = true;
            }

            //Right-arrow
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) && movementSound == true)
            {
                audioHandler.PlayOneShot(movement1);
                movementSound = false;
            }
            else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) && movementSound == false)
            {
                audioHandler.PlayOneShot(movement2);
                movementSound = true;
            }
        }
    }
}
