using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBar : MonoBehaviour
{
    [SerializeField] AudioSource energyBarAudioSource; // Add this line to create a serialized field for the AudioSource.

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Get the CharacterPowerUp script from the player.
            PlayerPowerUp characterPowerUp = other.GetComponent<PlayerPowerUp>();

            if (characterPowerUp != null)
            {
                // Play the audio clip when the player collects an energy bar.
                if (energyBarAudioSource != null)
                {
                    energyBarAudioSource.Play();
                }

                // Collect the energy bar and check if the character is now powered up.
                characterPowerUp.CollectEnergyBar();

                // Disable the energy bar.
                gameObject.SetActive(false);
            }
        }
    }
}
