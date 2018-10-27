using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockScript : MonoBehaviour {

    public int pv;
	// Use this for initialization
	void Start () {
        this.pv = 2;
	}
	
	// Update is called once per frame
	void Update () {
        if (this.pv <= 0)
        {
            Destroy(gameObject);
        }
	}

	public int getNbObj () {
		return 1;
	}
}
