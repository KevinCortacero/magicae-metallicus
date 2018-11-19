using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketProjectile : Projectile {

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
            else {
                Destroy(col.gameObject);
                col.gameObject.GetComponent<PixelArsenalProjectileScript>().Collided();
            }
        }
        else if (col.gameObject.tag == "Player") {
            Destroy(gameObject);
            GetComponent<PixelArsenalProjectileScript>().Collided();
            base.InteractWithPlayer(col.gameObject.GetComponent<Player>());

        }
        else if (col.gameObject.tag == "Rock") {
            if (col.gameObject.GetComponent<RockScript>() is IceRock) {
                ApplyCollision();
            }
            else {
                //Destroy(col.gameObject);
            }
            col.gameObject.GetComponent<RockScript>().pv = 0;
        }
        else if (col.gameObject.tag == "Item") {

            Destroy(col.gameObject);

        }
        else {
            ApplyCollision();
        }
    }

    /*protected void ApplyCollision(Collision2D col) {

        if (col.gameObject.tag == "Projectile") {
            if (col.gameObject.GetComponent<Projectile>().GetType().Equals(this.GetType())) {
                base.ApplyCollision(col);
            }
            else {
                Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), col.gameObject.GetComponent<Collider2D>());
            }
        }
        else if (col.gameObject.tag == "Player") {
            base.ApplyCollision(col);
        }
        else if (col.gameObject.tag == "Rock") {
            if (col.gameObject.GetComponent<RockScript>() is IceRock) {
                base.ApplyCollision(col);
            }
            else {
                Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), col.gameObject.GetComponent<Collider2D>());
            }
        }*/
}


/*protected override void InteractWithPlayer(Collision2D col) {
    Player player = col.gameObject.GetComponent<Player>();
    player.Damage(GetRatio());
}


protected override void InteractWithRock(Collision2D col) {
    RockScript rock = col.gameObject.GetComponent<RockScript>();
    rock.pv -= GetRatio();
}*/

