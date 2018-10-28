using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHolder {

    public GameObject projectile;
    public float remaining;

    public ProjectileHolder(GameObject projectile, float remaining) {
        this.projectile = projectile;
        this.remaining = remaining;
    }
}
