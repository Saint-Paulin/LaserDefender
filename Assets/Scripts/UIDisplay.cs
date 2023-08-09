using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] Slider healthBar;
    Health health;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        health = FindObjectOfType<Health>();
        healthBar.maxValue = health.GetHealth();
        healthBar.value = health.GetHealth();
    }

    public void UpdateHealthBar()
    {
        healthBar.value = health.GetHealth();
    }

    public void UpdateScoreUI()
    {
        scoreText.text = scoreKeeper.GetScore().ToString("000000000");
    }
}