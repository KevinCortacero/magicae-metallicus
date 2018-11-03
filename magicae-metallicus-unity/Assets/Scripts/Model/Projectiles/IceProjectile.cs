using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceProjectile : Projectile {

    public override bool IsBurning {
        get {
            return false;
        }
    }


    /*protected override void InteractWithPlayer(Collision2D col) {
        Player player = col.gameObject.GetComponent<Player>();
        player.Damage(GetRatio()*5);
    }


    protected override void InteractWithRock(Collision2D col) {
        //Do nothing
    }*/
}
