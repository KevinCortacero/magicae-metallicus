using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RockScript : NetworkBehaviour {

    [SyncVar]
    public float pv;
    [SyncVar]
    private Vector3 start;
    [SyncVar]
    private Vector3 end;

    private int sens;
    [SyncVar]
    private int direction;
    [SyncVar]
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
                Debug.Log("truc chelou 1");
				transform.position += new Vector3 (speed * Time.deltaTime, 0.0f, 0.0f);
			}
		}
		else {
			if (start[0] > end[0]) {
                Debug.Log("truc chelou 2");
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
        Debug.Log("Moving");
        this.direction = direction;
		this.end += d;
	}

	public void moveDirectly(Vector3 d) {
        Debug.Log("Moving directly");
		transform.position = end + d;
		start = transform.position;
		end = transform.position;
	}

	public float getPv() {
		return this.pv;
	}

	public void resetPv() {
        Debug.Log("reset");
        this.pv = 2;
	}

	public void increaseBufferNumberOfTurn() {
		this.nbTurnOfBuffer += 1;
	}

	public int getBufferNumberOfTurn() {
		return this.nbTurnOfBuffer;
	}
}
