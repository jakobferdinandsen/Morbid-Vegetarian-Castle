﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.CompareTag("Enemy") || coll.gameObject.CompareTag("RangedEnemy") || coll.gameObject.CompareTag("BigGuy")) {
            Destroy(coll.gameObject);
        }
    }
}
