using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HUDPlayerScript : MonoBehaviour {


    public Slider slider;
    public Player player;
	// Use this for initialization
	void Start () {
        if(player == null) {
            Debug.Log("player not loaded");
            return;
        }
        this.slider.value = player.PV;
	}
	
	// Update is called once per frame
	void Update () {
        if (player == null) {
            //Debug.Log("player not loaded");
            return;
        }
        this.slider.value = player.PV;
    }
}
