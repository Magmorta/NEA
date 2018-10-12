using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarFollow : MonoBehaviour {

    public Transform Enemy;

    Vector3 Offset;
    RectTransform BarRectTransform;

	// Use this for initialization
	void Start ()
    {
        Offset = transform.position - Enemy.position;
        BarRectTransform = GetComponent<RectTransform>();
        
	}
	
	void LateUpdate ()
    {
        transform.position = Enemy.position + Offset;
        BarRectTransform.rotation =Quaternion.Euler(0,0,0);
	}
}
