using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MovementSpeed;
    private Rigidbody2D MyRigidbody2D;

    public GameObject Bullet1;
    public GameObject Bullet2;
    public GameObject Bullet3;
    public Transform ShotSpawn;

    private int EquippedWeapon;
    private float FireRate;
    public float FireRate1;
    public float FireRate2;
    public float FireRate3;
    private float NextFire;

    // Use this for initialization
    void Start ()
    {
        MyRigidbody2D = GetComponent<Rigidbody2D>();  //Store a reference to the Rigidbody2D component required to use 2D Physics.
        MyRigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;   //No player rotation from physics interactions.
        EquippedWeapon = 1; //Default weapon to pistol.
        FireRate = FireRate1; //Pistol fire rate.
    }

    private void Update()   //Called just before every frame.
    {
        //Player Movement.
        var MovementX = Input.GetAxis("Horizontal") * Time.deltaTime * MovementSpeed;
        var MovementY = Input.GetAxis("Vertical") * Time.deltaTime * MovementSpeed;
        Vector3 Movement = new Vector3(MovementX, MovementY, 0);
        transform.Translate(Movement, Space.World);

        //Player Rotation (Mouse controlled).
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 dir = Input.mousePosition - pos;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1));

        //Instantiating bullets
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquippedWeapon = 1;
            FireRate = FireRate1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquippedWeapon = 2;
            FireRate = FireRate2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            EquippedWeapon = 3;
            FireRate = FireRate3;
        }
        if (Input.GetButtonDown("Fire1") && Time.time > NextFire)
        {
            NextFire = Time.time + FireRate;
            switch (EquippedWeapon)
            {
                case 1:
                    Instantiate(Bullet1, ShotSpawn.position, ShotSpawn.rotation);
                    break;
                case 2:
                    Instantiate(Bullet2, ShotSpawn.position, ShotSpawn.rotation);
                    break;
                case 3:
                    Instantiate(Bullet3, ShotSpawn.position, ShotSpawn.rotation);
                    break;
                default:
                    break;
            }
        }
    }
    void FixedUpdate ()     // FixedUpdate is called at a fixed interval, independent to the frame rate. Used for Physics code.
    { 

        
    }
}
// Ctrl + K + C = Block comment. Ctrl + K + U = Block un-comment.