using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDAndHealth : MonoBehaviour
{

    private int StartingHealth = 100;
    public int CurrentHealth;
    public int Score;

    public Slider HealthSlider;
    public Image DeathImage;
    public Image DamageImage;
    public Text DeathText;
    public Text ScoreText;
    public Text TimeText;
    public PlayerController PlayerControllerScript;
    public EnemySpawner EnemySpawnerScript;
    public EnemyController EnemyControllerScript;

    // Use this for initialization
    void Start()
    {
        CurrentHealth = StartingHealth;
        DeathImage = GameObject.Find("DeathImage").GetComponent<Image>();
        DamageImage = GameObject.Find("DamageImage").GetComponent<Image>();
        PlayerControllerScript = GetComponent<PlayerController>();
        EnemySpawnerScript = GetComponent<EnemySpawner>();
        Score = 0;
        TimeText.text = "Time: " + 0;
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = "Score: " + Score;

        float CurrentTime = Time.time;
        CurrentTime = Mathf.Round(CurrentTime * 10f) / 10f;
        TimeText.text = "Time: " + CurrentTime;
        if (Input.GetKeyDown(KeyCode.Q))    //Testing purposes.
        {
            TakeDamage(100);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TakeDamage(10);            
        }
    }
    public void TakeDamage(int amount)
    {
        CurrentHealth -= amount;
        HealthSlider.value = CurrentHealth;
        if (CurrentHealth <= 0)
        {
            Death();
        }
        
    }
    void Death()
    {
        Color TempColour = DeathImage.color;
        TempColour.a = 1f;
        DeathImage.color = TempColour;

        Color DT = DeathText.color;
        DT.a = 1f;
        DeathText.color = DT;

        PlayerControllerScript.enabled = false;
        EnemySpawnerScript.enabled = false;
        Debug.Log("You died.");
    }
}
