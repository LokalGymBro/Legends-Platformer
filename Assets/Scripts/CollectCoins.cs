using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoins : MonoBehaviour
{
    [Tooltip("The particles that appear after the player collects a coin.")]
    public GameObject coinParticles;

    PlayerMovement playerMovementScript;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerMovementScript = other.GetComponent<PlayerMovement>();
            playerMovementScript.soundManager.PlayCoinSound();
            ScoreManager.score += 10;
            GameObject particles = Instantiate(coinParticles, transform.position, new Quaternion());
            
            
            // Check if the current score is a multiple of 10
            if (ScoreManager.score % 100 == 0)
            {
                // Check if the player's health is not at maximum
                if (playerMovementScript.playerStats.health < playerMovementScript.playerStats.maxHealth)
                {
                    // Increase the player's health by one
                    playerMovementScript.playerStats.ChangeHealth(1);
                }
            }

            Destroy(gameObject);
        }
    }
}