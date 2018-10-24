using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockAreaScript : MonoBehaviour {

	int numberOfLines = 10;

	// Use this for initialization
	void Start () {
		RockLineScript[] lines = new RockLineScript[numberOfLines];
		for(int i=0; i<numberOfLines; ++i) {
			lines[i] = new RockLineScript();
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
