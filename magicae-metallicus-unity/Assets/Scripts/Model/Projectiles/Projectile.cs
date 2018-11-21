using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class Projectile : NetworkBehaviour {

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

    [SyncVar]
    public NetworkInstanceId spawnedBy;
    // Set collider for all clients.
    /*public override void OnStartClient() {
        this.IgnoreSpawner();
    }*/

    private void IgnoreSpawner() {
        GameObject obj = ClientScene.FindLocalObject(spawnedBy);
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), obj.GetComponent<Collider2D>());
    }



    public abstract bool IsBurning { get; }

    // Use this for initialization
    void Start() {
        //Physics2D.IgnoreLayerCollision(8, 9);
        this.IgnoreSpawner();
    }

    // Update is called once per frame
    void Update() {

    }

    public void SetFocus(float deltaTime) {
        this.focusTime = deltaTime;
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

            this.InteractWithRock(col.gameObject.GetComponent<RockScript>());


        }
        else if (col.gameObject.tag == "Player") {

            this.InteractWithPlayer(col.gameObject.GetComponent<Player>());


        }

        /*if (col.gameObject.tag == "Arena") {

            Debug.Log(col.collider + " ignore " + GetComponent<Collider2D>());
            Physics2D.IgnoreCollision(col.collider, GetComponent<Collider2D>());
            Debug.Log("ignored");
            return;

        }*/

        ApplyCollision();
    }

    public virtual void ApplyCollision() {
        //Debug.Log("Mother");
        Destroy(gameObject);
        GetComponent<PixelArsenalProjectileScript>().Collided();
    }

    protected void InteractWithRock(RockScript rock) {
        float ratio = 1;
        if ((this.owner == 1 && rock.Sens == -1) || (this.owner == 0 && rock.Sens == 1)) {
            ratio = 0.5f;
        }

        rock.pv -= GetRatio() * this.maxDamage * this.ratioToRocks * ratio;
    }

    protected void InteractWithPlayer(Player player) {
        player.Damage(GetRatio() * this.maxDamage * this.ratioToPlayers);
    }

    internal void SetOwner(int owner) {
        this.owner = owner;
    }
}
