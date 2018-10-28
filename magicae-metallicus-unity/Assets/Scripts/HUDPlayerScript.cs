using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HUDPlayerScript : MonoBehaviour {


    public Slider slider;
    public Player player;
	// Use this for initialization
	void Start () {
        this.slider.value = player.PV;
	}
	
	// Update is called once per frame
	void Update () {
        this.slider.value = player.PV;
    }
}
