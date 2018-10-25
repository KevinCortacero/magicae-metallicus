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

        ratio = Mathf.Max(0.2f, ratio);
        ratio = Mathf.Min(1, ratio);

        //Debug.Log("ratio = " + ratio);
        //Debug.Log("velocity = " + new Vector2(x, y) * ratio * maxSpeed);

        GetComponent<Rigidbody2D>().velocity = new Vector2(x, y) * ratio*maxSpeed;

        
    }

    void OnCollisionEnter2D(Collision2D col) {
        Debug.Log("OnCollisionEnter2D");
        Debug.Log(col.gameObject.tag);

        if(col.gameObject.tag == "Destructible") {

            Destroy(col.gameObject);
            Destroy(gameObject);
        }
        else if (col.gameObject.tag == "Player") {

            Destroy(col.gameObject);
            Destroy(gameObject);
        }
        else if (col.gameObject.tag == "Arena")
        {
            Physics2D.IgnoreCollision(col.gameObject.GetComponent<Collider2D>(), this.gameObject.GetComponent<Collider2D>());
        }

        
    }
}
