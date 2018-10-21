using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    private float focusTime = 0;
    private float maxFocusTime = 3;
    private float maxSpeed = 20;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Focus(float deltaTime) {
        this.focusTime += deltaTime;
    }

    public void Shoot(float x, float y) {
        float ratio = this.focusTime / this.maxFocusTime;

        GetComponent<Rigidbody2D>().velocity = new Vector2(x, y) * ratio*maxSpeed;
    }
}
