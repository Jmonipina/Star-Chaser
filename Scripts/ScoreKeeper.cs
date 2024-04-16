using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour, IDataPersistence
{
    int currentScore;
    static ScoreKeeper instance;

    void Awake()
    {
        ManageSingleton();
    }
    
    void ManageSingleton()
    {
        if (instance != null)
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

    public int GetCurrentScore()
    {
        return this.currentScore;
    } 

    public void ModifyScore(int value)
    {
        currentScore += value;
        Mathf.Clamp(currentScore, 0, int.MaxValue);
    }

    public void ResetScore()
    {
        currentScore = 0;
    }

    public void LoadData(GameData data){
        this.currentScore = data.currentScore;
    }

    public void SaveData(ref GameData data)
    {
        data.currentScore = this.currentScore;
    }
}
