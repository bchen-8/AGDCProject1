using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainBattle : MonoBehaviour {
    
    private PlayerController playerScript;

    void Start () {
        playerScript = GetComponent<PlayerController>();

        /*
        playerScript.playerHealth = PlayerPrefs.GetInt("Player Health");
        playerScript.playerSpeed = PlayerPrefs.GetFloat("Player Speed");
        playerScript.dodgeRollCD = PlayerPrefs.GetFloat("Dodgeroll Cooldown");
        playerScript.dodgeRollStrength = PlayerPrefs.GetFloat("Dodgeroll Strength");
        playerScript.playerHopForce = PlayerPrefs.GetFloat("Player Hop");
        playerScript.playerJumpForce = PlayerPrefs.GetFloat("Player Jump");
        */
    }

	void Update () {
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene("Battle");
	}

    private void FixedUpdate()
    {
        
    }
}
