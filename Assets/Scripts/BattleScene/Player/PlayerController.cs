using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	
	//Components
	private Animator anim;
	private BoxCollider2D bcol;
	private Rigidbody2D rb;
	private SpriteRenderer sr;
	public Attack1 attack1;

	//Numbers
	private int playerHealth;
	private float playerSpeed;

	private float playerHopForce;
	private float playerJumpForce;
	private float playerJumpCD;

	private float xCoord1;
	private float xCoord2;
	private float yCoord1;
	private float yCoord2;

	private float attack1CD;

	//Bools
	private bool onGround;
	private bool isFalling;
	private bool vulnerable = true;
	private bool doubleJumped = false;
	private bool dodgeRolling = false;

	void Start () {
		//Component Getters
		anim = GetComponent<Animator>();
		bcol = GetComponent<BoxCollider2D>();
		rb = GetComponent<Rigidbody2D>();
		sr = GetComponent<SpriteRenderer>();

		//playerStats should be pulled from PlayerPrefs. Using temporary stats for now.
		playerSpeed = 1.5f;
		playerHopForce = -60f;
		playerJumpForce = 180f;
	}

	void Update () {

		//Moving left and right
		var move = new Vector3(Input.GetAxis("Horizontal") * playerSpeed, 0, 0);
		transform.position += move * playerSpeed * Time.deltaTime;
		if (Input.GetKeyDown(KeyCode.A) && onGround == true)
		{
			rb.AddForce(new Vector3(-0.0001f, 0, 0));
			anim.SetInteger("animState", 1);
			sr.flipX = true;
		}
		if (Input.GetKeyDown(KeyCode.D) && onGround == true)
		{
			rb.AddForce(new Vector3(0.0001f, 0, 0));
			anim.SetInteger("animState", 1);
			sr.flipX = false;
		}

		//Jumping/Hopping
		if (Input.GetKeyDown(KeyCode.W) && onGround == true)
		{
			playerJumpCD = Time.time + 0.1f;
			rb.AddForce(new Vector3(0, playerJumpForce, 0));
			anim.SetInteger("animState", 2);
		}
		if (Input.GetKeyUp(KeyCode.W) && playerJumpCD >= Time.time)
		{
			rb.AddForce(new Vector3(0, playerHopForce, 0));
		}

		//Detect falling status
		xCoord2 = transform.position.x;
		yCoord2 = transform.position.y;
		if (xCoord2 == xCoord1 && yCoord2 == yCoord1 && onGround == true)
		{
			anim.SetInteger("animState", 0);
		}
		if (yCoord2 - yCoord1 < 0 && onGround == false)
		{
			isFalling = true;
			rb.AddForce(new Vector3(0, -0.0001f, 0));
			anim.SetInteger("animState", 3);
		}
		if (yCoord2 - yCoord1 >= 0)
		{
			isFalling = false;
		}
		xCoord1 = xCoord2;
		yCoord1 = yCoord2;

		//One attack
		if (Input.GetKeyDown(KeyCode.J) && attack1CD <= Time.time)
		{
			attack1CD = Time.time + 0.17f;
			SpriteRenderer attack1sr = attack1.GetComponent<SpriteRenderer>();
			attack1sr.flipX = sr.flipX;
			if (sr.flipX == true)
			{
				var Attack1 = Instantiate(attack1, transform);
				Attack1.transform.parent = transform;
				Attack1.transform.position = new Vector3 (transform.position.x - 0.18f, transform.position.y, transform.position.z);
			} else
			{
				var Attack1 = Instantiate(attack1, transform);
				Attack1.transform.parent = transform;
				Attack1.transform.position = new Vector3 (transform.position.x + 0.18f, transform.position.y, transform.position.z);
			}
		}

		//Player Health check
		if (playerHealth <= 0)
		{
			PlayerDeath();
		}
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Ground")
			onGround = true;
	}

	private void OnCollisionExit2D(Collision2D other)
	{
		if (other.gameObject.tag == "Ground")
			onGround = false;
	}

	public void PlayerFlinch (int damage) {

	}

	public void PlayerStun (int duration) {

	}

	void PlayerDeath () {

	}
}
