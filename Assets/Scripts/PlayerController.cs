using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    private Animator anim;
    private BoxCollider2D bcol;
    private Rigidbody2D rb;

    private int playerHealth;
    private int playerSpeed;
    private int playerDamage;

    private bool onGround;
    private bool vulnerable = true;
    private bool doubleJumped = false;
    private bool dodgeRolling = false;

	void Start () {
        anim = GetComponent<Animator>();
        bcol = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();

        //playerStats should be pulled from PlayerPrefs
	}

	void Update () {

	}

    private void FixedUpdate() {
        
    }

    void PlayerFlinch () {

    }

    void PlayerStun () {

    }

    void PlayerDeath () {

    }
}
