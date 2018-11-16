using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour {

    protected float focusTime = 0;
    [SerializeField]
    protected float maxFocusTime = 3;
    [SerializeField]
    protected float maxDamage;
    [SerializeField]
    protected float ratioToPlayers;
    [SerializeField]
    protected float ratioToRocks;
    [SerializeField]
    protected float maxSpeed;
    protected bool isColliding = false;
    private int owner;

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
        //Debug.Log("OnCollisionEnter2D");
        //Debug.Log(col.gameObject.tag);

        if (isColliding) return;
        isColliding = true;



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
        //Debug.Log("Mother");
        Destroy(gameObject);
        GetComponent<PixelArsenalProjectileScript>().Collided();
    }

    protected void InteractWithRock(Collision2D col) {
        RockScript rock = col.gameObject.GetComponent<RockScript>();

        float ratio = 1;
        if ((this.owner == 1 && rock.Direction == -1) || (this.owner == 0 && rock.Direction == 1)) {
            ratio = 0.5f;
        }

        rock.pv -= GetRatio() * this.maxDamage * this.ratioToRocks * ratio;
    }

    protected void InteractWithPlayer(Collision2D col) {
        Player player = col.gameObject.GetComponent<Player>();
        player.Damage(GetRatio() * this.maxDamage * this.ratioToPlayers);
    }

    internal void SetOwner(int owner) {
        this.owner = owner;
    }
}
