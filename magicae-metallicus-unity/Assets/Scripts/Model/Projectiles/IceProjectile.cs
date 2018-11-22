using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class IceProjectile : Projectile {

    public GameObject ice;

    private Vector2 lastPosition;
    private float time = 0;

    public override bool IsBurning {
        get {
            return false;
        }
    }

    [Command]
    private void CmdSpawnIce() {
        GameObject go = Instantiate(this.ice, transform.position, transform.rotation) as GameObject;
        NetworkServer.Spawn(go);
        //Debug.Log("I AM THE ONLY ONE ");
    }

    public override void ApplyCollision() {

        this.CmdSpawnIce();
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
