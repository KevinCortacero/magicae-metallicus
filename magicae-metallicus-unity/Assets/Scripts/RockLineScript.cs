using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockLineScript : MonoBehaviour {

	public RockScript[] rocks;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public int getNumberOfRock() {
		int numberOfRock = 0;
		for(int i=0; i<rocks.Length; ++i) {
			if(rocks[i] != null) {
				numberOfRock++;
			}
		}
		return numberOfRock;
	}

}
