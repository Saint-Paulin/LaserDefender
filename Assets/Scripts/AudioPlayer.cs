using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] List<AudioClip> shootingClip;
    [SerializeField] [Range(0f, 1f)] float shootingVolume = 1f;

    [Header("Hit")]
    [SerializeField] AudioClip hitClip;
    [SerializeField] [Range(0f, 1f)] float hitVolume = 1f;

    static AudioPlayer instance;

    public AudioPlayer GetInstance()
    {
        return instance;
    }


    void Awake()
    {
        ManageSingleton();
    }

    void ManageSingleton()
    {
        // int instanceCount = FindObjectsOfType(GetType()).Length;
        // if(instanceCount > 1)
        if(instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayerShootingClip()
    {
        int index = Random.Range(0, shootingClip.Count);
        //int CurrentAudioClip = index;
        if (shootingClip != null)
        {
            //AudioSource.PlayClipAtPoint(shootingClip[index], Camera.main.transform.position, shootingVolume);
        }
        PlayClip(shootingClip[index], shootingVolume);
    }

    public void PlayHitClip()
    {
        if (hitClip != null)
        {
            //AudioSource.PlayClipAtPoint(hitClip, Camera.main.transform.position, hitVolume);
        }
        PlayClip(hitClip, hitVolume);
    }

    void PlayClip(AudioClip audioClip, float clipVolume)
    {
        if (audioClip != null)
        {
            Vector3 cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(audioClip, cameraPos, clipVolume);
        }
    }

}
