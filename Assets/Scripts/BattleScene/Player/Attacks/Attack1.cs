using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack1 : MonoBehaviour {

    private BoxCollider2D bcol;
    private SpriteRenderer sr;

    // Use this for initialization
    void Start () {
        bcol = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        if (sr.flipX == true)
        {
            bcol.offset = new Vector2(-0.06110305f, -0.02836919f);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            TestEnemyController otherScript = other.GetComponent<TestEnemyController>();
            otherScript.Flinch(10, sr.flipX, new Vector2 (80, 80));
        }
    }

    void AttackFinished()
    {
        Destroy(this.gameObject);
    }
}
