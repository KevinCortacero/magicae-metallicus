using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour {

    protected float focusTime = 0;
    protected float maxFocusTime = 3;
    protected float maxSpeed = 20;

    public abstract bool IsBurning { get; }

    // Use this for initialization
    void Start() {
        //Physics2D.IgnoreLayerCollision(8, 9);
    }

    // Update is called once per frame
    void Update() {

    }

    public void Focus(float deltaTime) {
        this.focusTime += deltaTime;
    }

    public void Shoot(float x, float y) {


        //Debug.Log("ratio = " + ratio);
        //Debug.Log("velocity = " + new Vector2(x, y) * ratio * maxSpeed);

        GetComponent<Rigidbody2D>().velocity = new Vector2(x, y) * GetRatio() * maxSpeed;


    }

    protected float GetRatio() {
        float ratio = this.focusTime / this.maxFocusTime;

        ratio = Mathf.Max(0.2f, ratio);
        ratio = Mathf.Min(1, ratio);

        return ratio;
    }

    void OnCollisionEnter2D(Collision2D col) {
        Debug.Log("OnCollisionEnter2D");
        Debug.Log(col.gameObject.tag);





        if (col.gameObject.tag == "Rock") {

            this.InteractWithRock(col);


        }
        else if (col.gameObject.tag == "Player") {

            this.InteractWithPlayer(col);


        }
        /*if (col.gameObject.tag == "Arena") {

            Debug.Log(col.collider + " ignore " + GetComponent<Collider2D>());
            Physics2D.IgnoreCollision(col.collider, GetComponent<Collider2D>());
            Debug.Log("ignored");
            return;

        }*/

        ApplyCollision();
    }

    protected virtual void ApplyCollision() {
        Debug.Log("Mother");
        Destroy(gameObject);
        GetComponent<PixelArsenalProjectileScript>().Collided();
    }

    protected abstract void InteractWithRock(Collision2D col);
    protected abstract void InteractWithPlayer(Collision2D col);
}
