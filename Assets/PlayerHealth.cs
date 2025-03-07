using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    public TextMeshProUGUI healthText; // แสดงเลือดของผู้เล่น

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthUI()
    {
        healthText.text = "Health: " + currentHealth + " / " + maxHealth;
    }

    void Die()
    {
        Debug.Log("Player Died!");
        // สามารถเพิ่มระบบรีเซ็ตหรือจบเกมที่นี่
    }
}
