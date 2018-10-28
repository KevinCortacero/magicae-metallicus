using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    public GameObject projectile;
    public float utilization;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    void OnTriggerEnter2D(Collider2D col) {
        

        if (col.gameObject.tag == "Player") {


            col.gameObject.GetComponent<Player>().PickUpItem(this);


            Destroy(gameObject);


        }
        else if (col.gameObject.tag == "Projectile") {

           
            if (col.gameObject.GetComponent<Projectile>().IsBurning) {

                Destroy(gameObject);
            }




        }

    }
}