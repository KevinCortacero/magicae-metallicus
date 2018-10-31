using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : Projectile {

    public override bool IsBurning {
        get {
            return true;
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
