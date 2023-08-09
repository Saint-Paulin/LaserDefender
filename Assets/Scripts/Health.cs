using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] bool isPlayer;
    [SerializeField] int health = 50;
    [SerializeField] int scoreToAdd = 50;
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] bool applyCameraShake;

    DamageDealer damageDealer;
    CameraShake cameraShake;
    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;
    UIDisplay uIDisplay;
    LevelManager levelManager;


    void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        uIDisplay = FindObjectOfType<UIDisplay>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        damageDealer = other.GetComponent<DamageDealer>();

        if (damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
            PlayHitEffect();
            ShakeCamera();
            audioPlayer.PlayHitClip();
            damageDealer.Hit();
        }
    }

    void ShakeCamera()
    {
        if (cameraShake != null && applyCameraShake)
        {
            cameraShake.Play();
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        uIDisplay.UpdateHealthBar();

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (!isPlayer && gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            scoreKeeper.ModifyScore(scoreToAdd);
            //Debug.Log(scoreKeeper.GetScore());
            uIDisplay.UpdateScoreUI();
        }
        else
        {
            levelManager.LoadGameOver();
        }
        Destroy(gameObject);
    }

    void PlayHitEffect()
    {
        ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(instance, instance.main.duration + instance.main.startLifetime.constantMax);
    }

    public int GetHealth()
    {
        return health;
    }

}
