using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceRock : RockScript {

    private double startTime;
    private float maxTime = 25f;

    // Use this for initialization
    void Start() {
        this.startTime = Time.time;
    }

    // Update is called once per frame
    void Update() {
        if (this.pv <= 0) {
            Destroy(gameObject);
        }
        double elapsed = Time.time - this.startTime;
        float ratio = 1f - (float)(elapsed / this.maxTime);

        Debug.Log("elapsed : " + elapsed + " ratio = " + ratio + " my scale = " + GetComponent<Transform>().localScale.x + "= " + GetComponent<Transform>().localScale.x * ratio);

        GetComponent<Transform>().localScale = new Vector3(ratio, ratio, 0);
        Debug.Log("scale = " + new Vector3(ratio, ratio, 0));
        if (elapsed > 20) {
            Debug.Log("destroy");
            Destroy(gameObject);
        }
    }
}
