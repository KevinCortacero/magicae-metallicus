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
        Physics2D.IgnoreLayerCollision(8, 9);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Focus(float deltaTime) {
        this.focusTime += deltaTime;
    }

    public void Shoot(float x, float y) {
        

        //Debug.Log("ratio = " + ratio);
        //Debug.Log("velocity = " + new Vector2(x, y) * ratio * maxSpeed);

        GetComponent<Rigidbody2D>().velocity = new Vector2(x, y) * GetRatio() *maxSpeed;

        
    }

    private float GetRatio() {
        float ratio = this.focusTime / this.maxFocusTime;

        ratio = Mathf.Max(0.2f, ratio);
        ratio = Mathf.Min(1, ratio);

        return ratio;
    }

    void OnCollisionEnter2D(Collision2D col) {
        Debug.Log("OnCollisionEnter2D");
        Debug.Log(col.gameObject.tag);

        if (col.gameObject.tag == "Rock") {

            RockScript rock = col.gameObject.GetComponent<RockScript>();
            rock.pv -= GetRatio();
        }
        else if (col.gameObject.tag == "Player") {

            Player player = col.gameObject.GetComponent<Player>();
            player.pv -= GetRatio();
        }

        Destroy(gameObject);
    }
}
