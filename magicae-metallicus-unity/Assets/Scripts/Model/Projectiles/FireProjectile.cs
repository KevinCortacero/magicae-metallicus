using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : Projectile {

    public override bool IsBurning {
        get {
            return true;
        }
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Projectile") {
            if (col.gameObject.GetComponent<Projectile>().GetType().Equals(this.GetType()) || col.gameObject.GetComponent<Projectile>() is IceProjectile) {
                ApplyCollision();
                col.gameObject.GetComponent<Projectile>().ApplyCollision();
            }
            else if(!(col.gameObject.GetComponent<Projectile>() is RocketProjectile)) {
                col.gameObject.GetComponent<Projectile>().ApplyCollision();
            }
        }
        else if (col.gameObject.tag == "Player") {
            ApplyCollision();
            base.InteractWithPlayer(col.gameObject.GetComponent<Player>());

        }
        else if (col.gameObject.tag == "Rock") {
            ApplyCollision();
            base.InteractWithRock(col.gameObject.GetComponent<RockScript>());
        }
        else if (col.gameObject.tag == "Item") {

            Destroy(col.gameObject);

        }
        else {
            Destroy(gameObject);
            GetComponent<PixelArsenalProjectileScript>().Collided();
        }
    }

    /*protected override void InteractWithPlayer(Collision2D col) {
        Player player = col.gameObject.GetComponent<Player>();
        player.Damage(GetRatio()*2);
    }


    protected override void InteractWithRock(Collision2D col) {
        RockScript rock = col.gameObject.GetComponent<RockScript>();
        rock.pv -= GetRatio()*2;
    }*/
}
