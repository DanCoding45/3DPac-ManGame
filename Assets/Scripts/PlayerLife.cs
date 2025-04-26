using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    public int maxLives = 3; // Maximum number of lives
    public Text livesText; // Text to display remaining lives
    public float respawnDelay = 0.5f; // Delay before respawning
    public GameObject gameOverText; // Text to display "Game Over"

    private int currentLives; // Current number of lives
    private bool isDead = false; // Flag to track if the player is dead
    private PlayerPowerUp playerPowerUp;
    private Vector3 respawnPosition; // Initial respawn position

    [SerializeField] private AudioSource playerDeathAudio; // Serialized field for the death audio

    private void Start()
    {
        currentLives = maxLives;
        playerPowerUp = GetComponent<PlayerPowerUp>();
        respawnPosition = transform.position;

        UpdateLivesText();
        HideGameOverText();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy Body"))
        {
            if (!isDead)
            {
                if (playerPowerUp != null && playerPowerUp.IsPoweredUp)
                {
                    KillGuard(collision.gameObject);
                }
                else
                {
                    LoseLife();
                }
            }
        }
    }

    private void LoseLife()
    {
        currentLives--;

        if (currentLives <= 0)
        {
            GameOver();
        }
        else
        {
            Respawn();
        }

        UpdateLivesText();

        // Play the death audio when the player loses a life
        if (playerDeathAudio != null)
        {
            playerDeathAudio.Play();
        }
    }

    private void Respawn()
    {
        StartCoroutine(RespawnWithDelay());
    }

    private IEnumerator RespawnWithDelay()
    {
        isDead = true; // Set the player as dead to avoid multiple collisions
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<PlayerMovement>().enabled = false;

        yield return new WaitForSeconds(respawnDelay);

        transform.position = respawnPosition;
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<PlayerMovement>().enabled = true;
        isDead = false; // Player is no longer dead after respawning
    }

    private void UpdateLivesText()
    {
        if (livesText != null)
        {
            string heart = "❤️";
            string livesDisplay = "Life: ";

            for (int i = 0; i < currentLives; i++)
            {
                livesDisplay += heart;
            }

            livesText.text = livesDisplay;
        }
    }

    private void GameOver()
    {
        currentLives = maxLives; // Reset the player's lives
        Respawn(); // Respawn the player
        UpdateLivesText(); // Update the UI text
        HideGameOverText(); // Hide the "Game Over" text if it's displayed
    }

    private void ShowGameOverText()
    {
        if (gameOverText != null)
        {
            gameOverText.SetActive(true);
        }
    }

    private void HideGameOverText()
    {
        if (gameOverText != null)
        {
            gameOverText.SetActive(false);
        }
    }

    private void KillGuard(GameObject guard)
    {
        // Implement the logic for killing the guard (e.g., deactivating or destroying the guard)
        // Example: Destroy the guard game object
        Destroy(guard);
    }
}
