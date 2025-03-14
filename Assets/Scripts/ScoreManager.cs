using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int score = 0;
    public int scoreToUnlockItem = 50;
    public TextMeshProUGUI scoreText;
    public GameObject specialItem;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        UpdateScoreUI();
        specialItem.SetActive(false);
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();

        if (score >= scoreToUnlockItem)
        {
            UnlockItem();
        }
    }

    void UnlockItem()
    {
        specialItem.SetActive(true);
        Debug.Log("‰Õ‡∑¡ª≈¥≈ÁÕ°·≈È«!");
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Eliminate: " + score;
    }
}

