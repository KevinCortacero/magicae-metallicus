﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    public GameObject projectile;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Player") {

            //DO SMTH


        }

        Destroy(gameObject);
    }
}