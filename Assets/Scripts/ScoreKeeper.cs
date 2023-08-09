using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int currentScore;
    static ScoreKeeper instance;

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

    void Awake()
    {
        ManageSingleton();
    }

    public int GetScore()
    {
        return currentScore;
    }

    public void ModifyScore(int score)
    {
        currentScore += score;
        Mathf.Clamp(currentScore, 0, int.MaxValue); // Empecher la valeur de descendre en dessous de 0
    }

    public void ResetScore()
    {
        currentScore = 0;
    }
}
