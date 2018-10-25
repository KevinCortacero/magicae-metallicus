using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockAreaScript : MonoBehaviour {

	public RockLineScript[] lines;
	public float speed = 1f;
	public int direction = 0;
	Vector3 start;
	Vector3 end;
	int flag = 0;
	int lineInd = 0;

	// Use this for initialization
	void Start () {
		start = transform.position;
		end = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if((lines[lineInd].getNumberOfRock() == 0) && (flag == 0)) {
			start = transform.position;
			if(direction == 0) {
				end = start + new Vector3(0.5f, 0f, 0f);
			}
			else {
				end = start + new Vector3(-0.5f, 0f, 0f);
			}
			lineInd++;
			
			if(lineInd == 6) {
				flag = 1;
			}
		}

		if(direction == 0) {
			if(start[0] < end[0]) {
				transform.position += new Vector3 (speed * Time.deltaTime, 0.0f, 0.0f);
				start = transform.position;
			}
		}
		else {
			if(start[0] > end[0]) {
				transform.position += new Vector3 (speed * Time.deltaTime, 0.0f, 0.0f);
				start = transform.position;
			}
		}
		
		
		

		if (Input.GetKey(KeyCode.M)) {
			print("Number of rocks: " + getNumberOfRock());
		}

		//transform.position += new Vector3 (speed * Time.deltaTime, 0.0f, 0.0f);
		//transform.Translate(Time.deltaTime, 0, 0, Space.World);
	}

	public int getNumberOfRock() {
		int nbObj = 0;
		for(int i = 0; i < lines.Length; ++i)
		{
			nbObj = nbObj + lines[i].getNumberOfRock();
		}
		return nbObj;
	}
}
