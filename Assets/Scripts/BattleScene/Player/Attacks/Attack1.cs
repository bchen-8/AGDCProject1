﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack1 : MonoBehaviour {

    private BoxCollider2D bcol;
    private SpriteRenderer sr;

    // Use this for initialization
    void Start () {
        bcol = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            //get enemy's script component and call the function for dealing damage
            //other.Flinch(10);
        }
    }

    void AttackFinished()
    {
        Destroy(this.gameObject);
    }
}
