using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    protected float Health;
    public Slider EnemyHealthSlider;
    protected int Score;
    public HUDAndHealth HUDAndHealthScript;

    private Transform Player;
    public int EnemyMoveSpeed;
    public float MinDistance;

    private float MaxHealth;
    private float CurrentHealth;
    private float HitRange;
    private int HitDamage;
    private float HitRate;
    private float NextFire;

    private string EnemyTag;
    private string OtherTag;
	// Use this for initialization
	void Start ()
    {
        Player = GameObject.Find("Player").transform;
        EnemyTag = gameObject.tag;
        switch (EnemyTag)   //Allow enemies to have different amounts of Health and other stats.
        {
            case "Enemy1":
                MaxHealth = 2;
                CurrentHealth = MaxHealth;
                HitRange = 3.5f;
                HitDamage = 10;
                HitRate = 1.5f;
                EnemyHealthSlider.maxValue = MaxHealth;
                EnemyHealthSlider.value = MaxHealth;
                break;
            case "Enemy2":
                MaxHealth = 20;
                CurrentHealth = MaxHealth;
                HitRange = 5f;
                HitDamage = 25;
                HitRate = 2f;
                EnemyHealthSlider.maxValue = MaxHealth;
                EnemyHealthSlider.value = MaxHealth;
                Debug.Log("BOSS HP: " + CurrentHealth);
                break;
            default:
                Debug.Log("No enemy found.");
                break;
        }
        HUDAndHealthScript = GameObject.Find("Player").GetComponent<HUDAndHealth>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 PlayerPosition = Player.position;
        Vector3 EnemyDirection = transform.position - PlayerPosition;
        float EnemyAngle = Mathf.Atan2(EnemyDirection.y, EnemyDirection.x) * Mathf.Rad2Deg + 90;
        this.transform.rotation = Quaternion.AngleAxis(EnemyAngle, new Vector3(0, 0, 1));

        if (Vector3.Distance(transform.position, Player.position) >= MinDistance)
        {
            transform.position += transform.up * EnemyMoveSpeed * Time.deltaTime;

        }

        if (Vector3.Distance(transform.position, Player.position) <= HitRange && Time.time > NextFire)
        {
            NextFire = Time.time + HitRate;
            HUDAndHealthScript.TakeDamage(HitDamage);
        }
    }

    void OnTriggerEnter2D(Collider2D other) //Figure out what object entered the Enemy's trigger. 
    {
        OtherTag = other.tag;
        switch (OtherTag)
        {
            case "Bullet1":
                TakeDamage(GameObject.Find("Bullet1(Clone)").GetComponent<BulletController>().BulletDamage);
                Destroy(other.gameObject);
                break;
            case "Bullet2":
                TakeDamage(GameObject.Find("Bullet2(Clone)").GetComponent<BulletController>().BulletDamage);
                Destroy(other.gameObject);
                break;
            case "Bullet3":
                TakeDamage(GameObject.Find("Bullet3(Clone)").GetComponent<BulletController>().BulletDamage);
                Destroy(other.gameObject);
                break;
            case "Enemy1":
            case "Enemy2":
                break;
            default:
                Debug.Log("No bullet found.");
                break;
        }
    }

    void TakeDamage(float Amount)   //Allows the enemy to take damage to their Health.
    {
        CurrentHealth -= Amount;
        EnemyHealthSlider.value = CurrentHealth;
        if (CurrentHealth <= 0)
        {
            switch (EnemyTag)
            {
                case "Enemy1":
                    HUDAndHealthScript.Score += 1;
                    Debug.Log("Score increased.");
                    Destroy(gameObject);
                    break;
                case "Enemy2":
                    HUDAndHealthScript.Score += 5;
                    Debug.Log("Score increased.");
                    Destroy(gameObject);
                    break;
                default:
                    Debug.Log("No enemy found. TakeDamage.");
                    break;
            }
        }
    }
}