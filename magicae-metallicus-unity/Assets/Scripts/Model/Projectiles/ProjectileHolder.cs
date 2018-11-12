using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProjectileHolder {

    [SerializeField]
    public GameObject projectile;
    [SerializeField]
    public float remaining;

    public ProjectileHolder(GameObject projectile, float remaining) {
        this.projectile = projectile;
        this.remaining = remaining;
    }
}
