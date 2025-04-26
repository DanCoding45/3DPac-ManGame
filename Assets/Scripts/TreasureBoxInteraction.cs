using UnityEngine;
using UnityEngine.UI;

public class TreasureBoxInteraction : MonoBehaviour
{
    public Text treasureBoxCountText; // Reference to the UI Text component for displaying the count.

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPowerUp playerPowerUp = other.GetComponent<PlayerPowerUp>();
            if (playerPowerUp != null && !playerPowerUp.IsPoweredUp)
            {
                playerPowerUp.CollectTreasureBox();

                // Update the UI Text to display "Collected" when the player collects a treasure box.
                if (treasureBoxCountText != null)
                {
                    treasureBoxCountText.text = "Collected";
                }
            }
        }
    }
}
