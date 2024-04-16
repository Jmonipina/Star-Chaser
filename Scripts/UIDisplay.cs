using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIDisplay : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] Slider healthSlider;
    [SerializeField] HealthScript playerHealth;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("Pop-up level name")]
    [SerializeField] TextMeshProUGUI levelName;
    [SerializeField] public float timeWhenDisappear = 2f;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    void Start()
    {
        healthSlider.maxValue = playerHealth.GetPlayerHealth();
        Invoke("DisappearText", timeWhenDisappear);
    }

    void Update()
    {
        healthSlider.value = playerHealth.GetPlayerHealth();
        scoreText.text = scoreKeeper.GetCurrentScore().ToString("000000");
    }
    void DisappearText(){
        levelName.enabled = false;
    }

}
