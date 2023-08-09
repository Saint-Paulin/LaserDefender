using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefabs;
    [SerializeField] Transform gun;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float firingRate = 0.2f;

    [Header("AI")]
    [SerializeField] bool useAI;
    [SerializeField] float timeBetweenFiringAI = 4f;
    [SerializeField] float firingRateVariance = 0f;
    [SerializeField] float minimumFiringRate = 1f;


    [HideInInspector] public bool isFiring;

    Coroutine firingCoroutine;
    AudioPlayer audioPlayer;

    // CircleCollider2D circleCollider2D;
    Health health;

    private void Awake() {
        audioPlayer = FindObjectOfType<AudioPlayer>();
        health = FindObjectOfType<Health>();
    }

    void Start()
    {
        if(useAI)
        {
            isFiring = true;
        }
    }

    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        else if (!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator FireContinuously()
    {
        while(true)
        {
            GameObject proj = Instantiate(projectilePrefabs, new Vector2 (gun.position.x, gun.position.y), Quaternion.identity);

            Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // rb.velocity += new Vector2 (0f, projectileSpeed);
                rb.velocity = transform.up * projectileSpeed;
            }
            
            Destroy(proj, projectileLifetime);


            if(useAI)
            {
                float firingRateAI = Random.Range(timeBetweenFiringAI - firingRateVariance,
                                                timeBetweenFiringAI + firingRateVariance);
                firingRateAI = Mathf.Clamp(firingRateAI, minimumFiringRate, float.MaxValue);
                firingRate = firingRateAI;
            }

            audioPlayer.PlayerShootingClip();

            yield return new WaitForSeconds(firingRate);
        }
    }
}
