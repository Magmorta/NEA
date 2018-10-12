using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    
    public GameObject Enemy1;
    public GameObject Enemy2;
    public Transform TopLeft;
    public Transform TopRight;
    public Transform BottomRight;
    public Transform BottomLeft;
    public Transform BossSpawn;
    public float StartingTotalEnemies;
    
    private int Location;
    private float CurrentTime;
    private float CurrentTimeMOD;
    private float RoundTotalEnemies;
    protected int RoundNumber;
    private int SpawnedEnemies;
    private int BossesToSpawn;
    private int BossesSpawned;
    private bool BossesCalculated;
    private int NextWave;


    // Use this for initialization
    void Start ()
    {
        SpawnedEnemies = 0;
        RoundNumber = 1;
        NextWave = 0;
        BossesCalculated = false;
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        CurrentTime = Time.time;

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            NextWave = 10000;
        }

        switch (RoundNumber)
        {
            case 1:
                Debug.Log("Round 1 begin.");
                CreatePrefab(StartingTotalEnemies);

                NextWave += 15;
                RoundNumber += 1;
                break;
            default:
                if (CurrentTime > NextWave)
                {
                    RoundNumber += 1;
                    Debug.Log("Round Number increased, currently: " + RoundNumber);
                    SpawnedEnemies = 0;

                    RoundTotalEnemies = Mathf.Round(Mathf.Pow(StartingTotalEnemies, 1.1f));
                    CreatePrefab(RoundTotalEnemies);
                    Debug.Log("This rounds enemies = " + RoundTotalEnemies);
                    Debug.Log("CURRENT ROUND = " + RoundNumber);

                    NextWave += 15;
                    BossesCalculated = false;
                }
                break;
                
        }
        
        if (RoundNumber % 3 == 0 & BossesCalculated == false)
        {
            BossesToSpawn += 1;
            BossesCalculated = true;
        }
    }

    void CreatePrefab(float EnemiesToSpawn)
    {
        Debug.Log("Enemies to spawn: " + EnemiesToSpawn);
        while (SpawnedEnemies < EnemiesToSpawn)    //Spawn normal enemies.
        {
            CreateLocation();
            switch (Location)
            {
                case 0:
                    Instantiate(Enemy1, TopLeft.position, TopLeft.rotation);
                    break;
                case 1:
                    Instantiate(Enemy1, TopRight.position, TopRight.rotation);
                    break;
                case 2:
                    Instantiate(Enemy1, BottomRight.position, BottomRight.rotation);
                    break;
                case 3:
                    Instantiate(Enemy1, BottomLeft.position, BottomLeft.rotation);
                    break;
            }
            SpawnedEnemies += 1;
            Debug.Log("Enemy Spawned.");
        }
        while (BossesSpawned < BossesToSpawn)  //Spawn bosses.
        {
            CreateLocation();
            switch (Location)
            {
                case 0:
                    Instantiate(Enemy2, TopLeft.position, TopLeft.rotation);
                    break;
                case 1:
                    Instantiate(Enemy2, TopRight.position, TopRight.rotation);
                    break;
                case 2:
                    Instantiate(Enemy2, BottomRight.position, BottomRight.rotation);
                    break;
                case 3:
                    Instantiate(Enemy2, BottomLeft.position, BottomLeft.rotation);
                    break;
            }
            BossesSpawned += 1;
            Debug.Log("Boss spawned.");
        }
    }
    void CreateLocation()
    {
        Location = Random.Range(0, 4);
        Debug.Log("Location Created");
    }
}
