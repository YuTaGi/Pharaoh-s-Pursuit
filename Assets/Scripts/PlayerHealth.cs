using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    public TextMeshProUGUI healthText;

    public Image damageImage;  // ✅ รูปภาพแสดงเมื่อโดนดาเมจ
    public Image healImage;    // ✅ รูปภาพแสดงเมื่อเก็บเลือด

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        UpdateHealthUI();
        ShowEffect(damageImage); // ✅ แสดงภาพเมื่อโดนดาเมจ

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;

        UpdateHealthUI();
        ShowEffect(healImage); // ✅ แสดงภาพเมื่อเก็บเลือด
    }

    void ShowEffect(Image effectImage)
    {
        StartCoroutine(ShowAndHideEffect(effectImage, 0.5f)); // แสดง 0.5 วิ
    }

    IEnumerator ShowAndHideEffect(Image effectImage, float duration)
    {
        effectImage.color = new Color(effectImage.color.r, effectImage.color.g, effectImage.color.b, 1);
        yield return new WaitForSeconds(duration);
        effectImage.color = new Color(effectImage.color.r, effectImage.color.g, effectImage.color.b, 0);
    }

    void UpdateHealthUI()
    {
        healthText.text = "Hp: " + currentHealth + " / " + maxHealth;
    }

    void Die()
    {
        Debug.Log("Player Died!");
    }
}
