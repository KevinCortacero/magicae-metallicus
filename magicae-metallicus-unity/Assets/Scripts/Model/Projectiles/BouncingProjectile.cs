using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingProjectile : Projectile {

    [SerializeField]
    private float ratioAfterBouncing;
    [SerializeField]
    private float additiveDamage;
    [SerializeField]
    private int maxCollision = 10;
    private int counter = 0;

    public override bool IsBurning {
        get {
            return false;
        }
    }


    /*protected override void InteractWithPlayer(Collision2D col) {
        Player player = col.gameObject.GetComponent<Player>();
        player.Damage(GetRatio());
    }


    protected override void InteractWithRock(Collision2D col) {
        RockScript rock = col.gameObject.GetComponent<RockScript>();
        rock.pv -= GetRatio();
    }*/

    protected override void ApplyCollision() {

        if (this.counter > this.maxCollision) {
            base.ApplyCollision();
        }
        GetComponent<Rigidbody2D>().velocity *= ratioAfterBouncing;
        this.maxDamage += this.additiveDamage;
        this.counter += 1;
        isColliding = false;
        //Debug.Log(counter);
    }
}
