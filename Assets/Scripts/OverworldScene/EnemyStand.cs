using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStand : MonoBehaviour {

    private BoxCollider2D bcol;

	// Use this for initialization
	void Start () {
        if (PlayerPrefs.GetInt("Enemy1") == 1)
        {
            Debug.Log("DESTROY");
            Destroy(this.gameObject);
        }
        bcol = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
        
	}
}
