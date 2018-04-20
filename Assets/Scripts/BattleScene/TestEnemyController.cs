using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestEnemyController : MonoBehaviour {

    private Animator anim;
    private BoxCollider2D bcol;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    public GameObject playerObj;

    private int health;
    private int damage;

    private bool onGround;
    private bool vulnerable = true;

    void Start () {

        anim = GetComponent<Animator>();
        bcol = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        playerObj = GameObject.FindWithTag("Player");

        anim.SetInteger("animState", 0);
        health = 40;
        StartCoroutine(HopTowardsPlayer());
    }

    void Update ()
    {
        if (health <= 0)
        {
            StartDying();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerController otherScript = other.gameObject.GetComponent<PlayerController>();
            if (otherScript.vulnerable == true)
                otherScript.PlayerFlinch(20, sr.flipX, new Vector2(60, 60));
        }
        if (other.gameObject.tag == "Ground")
        {
            anim.SetInteger("animState", 0);
        }
    }

    IEnumerator HopTowardsPlayer()
    {
        yield return new WaitForSeconds(Random.Range(1.5f, 2.0f));
        if (playerObj.transform.position.x <= this.transform.position.x)
        {
            rb.AddForce(new Vector2(Random.Range(-60, -100), Random.Range(60, 150)));
            sr.flipX = false;
        }
        if (playerObj.transform.position.x > this.transform.position.x)
        {
            rb.AddForce(new Vector2(Random.Range(60, 100), Random.Range(60, 150)));
            sr.flipX = true;
        }
        StartCoroutine(HopTowardsPlayer());
    }

    public void Flinch(int damage, bool direction, Vector2 launch)
    {
        anim.SetInteger("animState", 1);
        health -= damage;
        if (direction == true)
        {
            launch.x = -launch.x;
            rb.AddForce(launch);
        } else
        {
            rb.AddForce(launch);
        }
    }

    void Stun()
    {

    }

    void StartDying()
    {
        anim.SetInteger("animState", 9);
    }

    void FinishedDying()
    {
        PlayerPrefs.SetInt("Enemy1", 1);
        SceneManager.LoadScene("Working proto");
        Destroy(this.gameObject);
    }
}
