using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour {
    public float MoveSpeed;
    public Animator anim;
    public BoxCollider2D bcol;
    public float XCoordinate1;
    public float XCoordinate2;
    public float YCoordinate1;
    public float YCoordinate2;
    public int actionPoint;

    void Start ()
    {
        actionPoint = 20;
        MoveSpeed = 5;
        anim= GetComponent<Animator>();
        bcol = GetComponent<BoxCollider2D>();
        if (PlayerPrefs.GetFloat("playerX") != 0f)
        {
            transform.Translate(new Vector2(PlayerPrefs.GetFloat("playerX") + 5, PlayerPrefs.GetFloat("playerY") + 3));
        }
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(new Vector2(Input.GetAxis("Horizontal") * Time.deltaTime * MoveSpeed, Input.GetAxis("Vertical") * Time.deltaTime * MoveSpeed));
         if (Input.GetAxis("Vertical") > 0)
         {
             anim.SetInteger("AnimState", 1);

         }

         else if (Input.GetAxis("Vertical") < 0)
         {
             anim.SetInteger("AnimState", -1);
         }



         else
         {
             anim.SetInteger("AnimState", 0);

         }

         if (Input.GetAxis("Horizontal") > 0)
         {
             anim.SetInteger("AnimState", 2);
         }
           //else if(Input.GetAxis("))
        XCoordinate2 = XCoordinate1;
        YCoordinate2 = YCoordinate1;
        YCoordinate1 = transform.position.y;
        XCoordinate1 = transform.position.x;
        if (YCoordinate2 < YCoordinate1)
        {
            anim.SetInteger("AnimState", 1);
       
        }
        if (YCoordinate2 > YCoordinate1)
        {
            anim.SetInteger("AnimState", -1);
        }
        

        if (XCoordinate2 < XCoordinate1)
        {
            anim.SetInteger("AnimState", 2);

        }
        if(XCoordinate2 > XCoordinate1)
        {
            anim.SetInteger("AnimState", -2);

        }
        if (XCoordinate2==XCoordinate1 && YCoordinate2 == YCoordinate1)
        {
            anim.SetInteger("AnimState", 0);
        }

        //Reset Code
        if (Input.GetKey(KeyCode.R))
        {
            PlayerPrefs.SetFloat("playerX", 0);
            PlayerPrefs.SetFloat("playerY", 0);
            PlayerPrefs.SetInt("Enemy1", 0);
            SceneManager.LoadScene("Working proto");
        }
	}
    
    /*void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && PlayerPrefs.GetInt("Enemy1") != 1)
        {
            Debug.Log("Contact");
            PlayerPrefs.SetFloat("playerX", transform.position.x);
            PlayerPrefs.SetFloat("playerY", transform.position.y);
            SceneManager.LoadScene("Battle");
        }
        if(other.CompareTag("Collider")){
            SceneManager.LoadScene("Working proto");

            } 
    }*/
}
