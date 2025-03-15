using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    public TextMeshProUGUI healthText;

    public Image damageImage;
    public Image healImage;

    public AudioSource audioSource; // 🎵 ตัวเล่นเสียง
    public AudioClip damageSound;   // 🔊 เสียงตอนโดนดาเมจ
    public AudioClip healSound;     // 🔊 เสียงตอนฮีล

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        UpdateHealthUI();
        ShowEffect(damageImage);

        // ✅ เล่นเสียงตอนโดนดาเมจ
        if (audioSource != null && damageSound != null)
        {
            audioSource.PlayOneShot(damageSound);
        }

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
        ShowEffect(healImage);

        // ✅ เล่นเสียงตอนได้รับการฮีล
        if (audioSource != null && healSound != null)
        {
            audioSource.PlayOneShot(healSound);
        }
    }

    void ShowEffect(Image effectImage)
    {
        StartCoroutine(ShowAndHideEffect(effectImage, 0.5f));
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
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Debug.Log("Player Died!");
        SceneManager.LoadScene("Failed");
    }
}
