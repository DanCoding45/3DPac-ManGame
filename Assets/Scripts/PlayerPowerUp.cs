using UnityEngine;
using UnityEngine.UI;

public class PlayerPowerUp : MonoBehaviour
{
    public Text timerText;
    public int requiredEnergyBars = 3;
    public int requiredTreasureBoxes = 3;
    public GuardPatrol[] guards;

    private int collectedEnergyBars = 0;
    private int collectedTreasureBoxes = 0;
    private bool isPoweredUp = false;
    private bool isVulnerable = true;

    public bool IsPoweredUp
    {
        get { return isPoweredUp; }
    }

    private float powerUpDuration = 10.0f;
    private float timer = 0.0f;
    private bool displayTimer = false;

    public int GetCollectedTreasureBoxesCount()
    {
        return collectedTreasureBoxes;
    }

    void Update()
    {
        if (collectedEnergyBars >= requiredEnergyBars && !isPoweredUp)
        {
            ActivatePowerUp("Powered Up");
        }
        else if (collectedTreasureBoxes >= requiredTreasureBoxes && !isPoweredUp)
        {
            ActivatePowerUp("Powerful");
        }

        if (isPoweredUp)
        {
            if (displayTimer && timer < powerUpDuration)
            {
                timer += Time.deltaTime;
                timerText.text = "Timer: " + (powerUpDuration - timer).ToString("0:00");
            }
            else if (timer >= powerUpDuration)
            {
                DeactivatePowerUp();
                MakePlayerVulnerable();
            }
        }
    }

    void ActivatePowerUp(string message = "Powered Up")
    {
        isPoweredUp = true;
        collectedEnergyBars -= requiredEnergyBars;
        collectedTreasureBoxes -= requiredTreasureBoxes;
        timer = 0.0f;
        isVulnerable = false;
        displayTimer = true;

        foreach (GuardPatrol guard in guards)
        {
            guard.TryKillGuard();
        }

        if (!displayTimer)
        {
            timerText.text = message;
        }

        Debug.Log("Power-Up Activated. Energy Bars: " + collectedEnergyBars + ", Treasure Boxes: " + collectedTreasureBoxes);
    }

    void DeactivatePowerUp()
    {
        isPoweredUp = false;
        displayTimer = false;

        foreach (GuardPatrol guard in guards)
        {
            guard.TryFlee();
        }

        Debug.Log("Power-Up Deactivated.");
    }

    void MakePlayerVulnerable()
    {
        isVulnerable = true;

        Debug.Log("Player Vulnerable.");
    }

    public void CollectEnergyBar()
    {
        collectedEnergyBars++;

        Debug.Log("Energy Bar Collected. Energy Bars: " + collectedEnergyBars);
    }

    public void CollectTreasureBox()
    {
        collectedTreasureBoxes++;

        if (collectedTreasureBoxes == requiredTreasureBoxes)
        {
            ActivatePowerUp("Powerful");
            displayTimer = false;
        }

        Debug.Log("Treasure Box Collected. Treasure Boxes: " + collectedTreasureBoxes);
    }

    public void GuardKilled()
    {
        // Implement what happens when the player kills the guard.
        // For example, you can deactivate or destroy the guard object.

        Debug.Log("Guard Killed.");
    }

    public bool IsPlayerVulnerable()
    {
        return isVulnerable;
    }
}