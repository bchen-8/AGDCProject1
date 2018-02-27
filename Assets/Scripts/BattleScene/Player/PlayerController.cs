using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	
	//Components
	private Animator anim;
	private BoxCollider2D bcol;
	private Rigidbody2D rb;
	private SpriteRenderer sr;
    private GameObject playerObj;
	public GameObject attack1;
    public SpriteRenderer attack1sr;

	//Numbers
	public int playerHealth;
	public float playerSpeed;
    private float dodgeRollTimer;
    public float dodgeRollCD;
    public float dodgeRollStrength;

	public float playerHopForce;
	public float playerJumpForce;
	private float playerJumpCD;

	private float xCoord1;
	private float xCoord2;
	private float yCoord1;
	private float yCoord2;

	private float attack1CD;

	//Bools
	private bool onGround;
	private bool isFalling;
    private bool isFlinching = false;
	public bool vulnerable = true;
	private bool doubleJumped = false;

	void Start () {
		//Component Getters
		anim = GetComponent<Animator>();
		bcol = GetComponent<BoxCollider2D>();
		rb = GetComponent<Rigidbody2D>();
		sr = GetComponent<SpriteRenderer>();
        playerObj = this.gameObject;

        //playerStats should be pulled from PlayerPrefs. Using temporary stats for now.
        playerHealth = 100;
		playerSpeed = 1.5f;
        dodgeRollCD = 1f;
        dodgeRollStrength = 125f;
		playerHopForce = -60f;
		playerJumpForce = 180f;
        //playerStats should be pulled from PlayerPrefs. Using temporary stats for now.
    }

    void Update () {
        if (!isFlinching && playerHealth > 0)
        {
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

            //One attack
            if (Input.GetKeyDown(KeyCode.J) && attack1CD <= Time.time)
            {
                attack1CD = Time.time + 0.17f;
                attack1sr = attack1.GetComponent<SpriteRenderer>();
                attack1sr.flipX = sr.flipX;
                if (sr.flipX == true)
                {
                    GameObject Attack1 = Instantiate(attack1, transform);
                    Attack1.transform.parent = transform;
                    Attack1.transform.position = new Vector3(transform.position.x - 0.18f, transform.position.y, transform.position.z);
                }
                else
                {
                    GameObject Attack1 = Instantiate(attack1, transform);
                    Attack1.transform.parent = transform;
                    Attack1.transform.position = new Vector3(transform.position.x + 0.18f, transform.position.y, transform.position.z);
                }
            }

            //Dodge-roll, needs animation/animstates
            if (Input.GetKeyDown(KeyCode.Space) && dodgeRollTimer <= Time.time)
            {
                dodgeRollTimer = Time.time + dodgeRollCD;
                StartCoroutine(Invuln(0.35f));
                if (sr.flipX == true)
                {
                    rb.AddForce(new Vector3(-dodgeRollStrength, 0, 0));
                }
                else
                {
                    rb.AddForce(new Vector3(dodgeRollStrength, 0, 0));
                }
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
        {
            onGround = true;
            isFlinching = false;
        }
    }

	private void OnCollisionExit2D(Collision2D other)
	{
		if (other.gameObject.tag == "Ground")
			onGround = false;
	}

    IEnumerator Invuln(float duration)
    {
        vulnerable = false;
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.5f);
        yield return new WaitForSeconds(duration);
        vulnerable = true;
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
    }

    public void PlayerFlinch(int damage, bool direction, Vector2 launch)
    {
        if (vulnerable == true)
        {
            StartCoroutine(Invuln(2.5f));
            playerHealth -= damage;
            Debug.Log("Player Health: "+playerHealth);
            if (playerHealth > 0)
            {
                isFlinching = true;
                anim.SetInteger("animState", 4);
                if (direction == true)
                {
                    launch.x = -launch.x;
                    rb.AddForce(launch);
                }
                else
                {
                    rb.AddForce(launch);
                }
            }
        }
    }

    public void PlayerStun (int duration) {

	}

	void PlayerDeath () {
        anim.SetInteger("animState", 9);
	}

    void FinishedDying()
    {
        playerObj.SetActive(false);
    }
}
