using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController: MonoBehaviour {

    public float BulletSpeed;
    private Rigidbody2D RB;
    public GameObject Player;

    private string BulletTag;
    public float BulletDamage;
    private string EnemyTag;

	// Use this for initialization
	void Start ()
    {
        RB = GetComponent<Rigidbody2D>();
        Vector3 ScreenPoint = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 Direction = (Input.mousePosition - ScreenPoint).normalized;
        RB.velocity = Direction * BulletSpeed;

        BulletTag = gameObject.tag;
        switch (BulletTag)
        {
            case "Bullet1":
                BulletDamage = 1;
                //Debug.Log("Bullet with 1 damage created.");
                break;
            case "Bullet2":
                BulletDamage = 0.25f;
                //Debug.Log("Bullet with 0.5 damage created.");
                break;
            case "Bullet3":
                BulletDamage = 5;
                //Debug.Log("Bullet with 4 damage created.");
                break;
            default:
                Debug.Log("No bullet found.");
                break;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
