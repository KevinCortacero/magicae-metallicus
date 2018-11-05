using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IceProjectile : Projectile {

    public GameObject ice;

    private Vector2 lastPosition;
    private float time = 0;

    public override bool IsBurning {
        get {
            return false;
        }
    }

    private void SpawnIce() {
        GameObject go = Instantiate(this.ice, transform.position, transform.rotation) as GameObject;
        //Debug.Log("I AM THE ONLY ONE ");
    }

    protected override void ApplyCollision() {

        this.SpawnIce();
        base.ApplyCollision();
    }

    void OnTriggerEnter2D(Collider2D coll) {
        

        Debug.Log("delta is " + (Time.time - this.time)+" vs delta time : "+Time.deltaTime);
        //if (Time.time - this.time > 0.01f) {
            Debug.Log("TRIGGER FOR OLD");
            this.lastPosition = new Vector2(transform.position.x, transform.position.y);
        //}
        this.time = Time.time;

    }


    /*protected override void InteractWithPlayer(Collision2D col) {
        Player player = col.gameObject.GetComponent<Player>();
        player.Damage(GetRatio()*5);
    }


    protected override void InteractWithRock(Collision2D col) {
        //Do nothing
    }*/
}
