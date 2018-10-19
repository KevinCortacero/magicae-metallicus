using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public SpriteRenderer renderer;

    public Sprite left;
    public Sprite right;
    public Sprite top;
    public Sprite bottom;

    public float speed;

    // Use this for initialization
    void Start () {
        this.renderer.sprite = right;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Z)) {
            this.renderer.sprite = top;
            this.transform.Translate(0, speed*Time.deltaTime, 0);
            Debug.Log(speed * Time.deltaTime);
        }else if (Input.GetKey(KeyCode.Q)) {
            this.renderer.sprite = left;
            this.transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
        else if (Input.GetKey(KeyCode.S)) {
            this.renderer.sprite = bottom;
            this.transform.Translate(0, -speed * Time.deltaTime, 0);
        }
        else if (Input.GetKey(KeyCode.D)) {
            this.renderer.sprite = right;
            this.transform.Translate(speed * Time.deltaTime, 0, 0);
        }
    }
}
