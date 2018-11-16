using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockScript : MonoBehaviour {

    public float pv;
	private Vector3 start;
	private Vector3 end;

    private int sens;
    private int direction;
	private float speed;
	private int nbTurnOfBuffer;

    public int Sens {
        get {
            return sens;
        }

        set {
            sens = value;
        }
    }

    // Use this for initialization
    void Start () {
       // Init rock positions
		start = transform.position;
		end = transform.position;

		speed = 1f;
		nbTurnOfBuffer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		this.start = transform.position;
		if (direction == 1) {
			if (start[0] < end[0]) {
				transform.position += new Vector3 (speed * Time.deltaTime, 0.0f, 0.0f);
			}
		}
		else {
			if (start[0] > end[0]) {
				transform.position -= new Vector3 (speed * Time.deltaTime, 0.0f, 0.0f);
			}
		}
	}

	public void setParams(int direction) {
		this.direction = direction;
        this.sens = direction;
	}

	public Vector3 getLocation() {
		return transform.position;
	}

	public void move(Vector3 d, int direction) {
		this.direction = direction;
		this.end += d;
	}

	public void moveDirectly(Vector3 d) {
		transform.position += d;
		start = transform.position;
		end = transform.position;
	}

	public float getPv() {
		return this.pv;
	}

	public void resetPv() {
		this.pv = 2;
	}

	public void increaseBufferNumberOfTurn() {
		this.nbTurnOfBuffer += 1;
	}

	public int getBufferNumberOfTurn() {
		return this.nbTurnOfBuffer;
	}
}
