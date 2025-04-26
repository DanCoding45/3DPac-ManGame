using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    public bool HasTreasureBox { get; internal set; }

    [SerializeField] AudioSource treasureBoxAudioSource; // Serialize this field for the audio source.

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("TreasureBox"))
        {
            // Play the treasure box sound
            if (treasureBoxAudioSource != null)
            {
                treasureBoxAudioSource.Play();
            }

            // Destroy the treasure box object
            Destroy(other.gameObject);
        }
    }
}
