using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour
{
    private string BulletTag;
    void OnTriggerEnter2D(Collider2D other)
    {
        BulletTag = other.tag;

        switch (BulletTag)
        {
            case "Bullet1":
            case "Bullet2":
            case "Bullet3":
                Destroy(other.gameObject);
                break;
            case "Player":
                break;
            default:
                Debug.Log("No bullet found. Boundary.");
                break;
        }
    }
}