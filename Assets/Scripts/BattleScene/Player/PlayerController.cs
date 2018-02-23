using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    private Animator anim;
    private BoxCollider2D bcol;
    private Rigidbody2D rb;

    private int playerHealth;
    private float playerSpeed;
    private float playerHopForce;
    private float playerJumpForce;
    private float playerJumpCD;
    private float playerDamage;

    private bool onGround;
    private bool vulnerable = true;
    private bool doubleJumped = false;
    private bool dodgeRolling = false;

	void Start () {
        anim = GetComponent<Animator>();
        bcol = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();

        //playerStats should be pulled from PlayerPrefs. Using temporary stats for now.
        playerSpeed = 1.5f;
        playerHopForce = -50f;
        playerJumpForce = 150f;
	}

	void Update () {

        //Moving left and right
        var move = new Vector3(Input.GetAxis("Horizontal") * playerSpeed, 0, 0);
        transform.position += move * playerSpeed * Time.deltaTime;

        //Jumping
        /*if (Input.GetKeyDown(KeyCode.W) && onGround == true)
        {
            rb.AddForce(new Vector3(0, playerJumpForce, 0));
            anim.SetInteger("animState", 2);
        }*/
        if (Input.GetKeyDown(KeyCode.W) && onGround == true)
        {
            playerJumpCD = Time.time + 0.1f;
            rb.AddForce(new Vector3(0, playerJumpForce, 0));
            anim.SetInteger("animState", 2);
        }
        if (Input.GetKeyUp(KeyCode.W) && playerJumpCD >= Time.time)
        {
            rb.AddForce(new Vector3(0, playerHopForce, 0));
            Debug.Log("Hop");
        }
    }

    private void FixedUpdate() {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            onGround = true;
        }
    }

    void PlayerFlinch () {

    }

    void PlayerStun () {

    }

    void PlayerDeath () {

    }
}
