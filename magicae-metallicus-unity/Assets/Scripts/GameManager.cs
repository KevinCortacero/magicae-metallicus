using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour {

    public GameObject player1HUD;
    public GameObject player2HUD;
    public GameObject rockAreaLeft;
    public GameObject rockAreaRight;
    public GameObject rockArea;

    public static GameManager instance = null;

    // Use this for initialization
    void Start() {
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

    }

    // Update is called once per frame
    void Update() {

    }

    public void SetPlayer1(Player player) {
        this.player1HUD.GetComponentInChildren<HUDPlayerScript>().player = player;
        this.player1HUD.GetComponentInChildren<SpellRemainingScript>().player = player;
    }

    public void SetPlayer2(Player player) {
        this.player2HUD.GetComponentInChildren<HUDPlayerScript>().player = player;
        this.player2HUD.GetComponentInChildren<SpellRemainingScript>().player = player;
    }

    public void BuildArena() {

        Debug.Log("building");

        GameObject left = Instantiate(this.rockArea, rockAreaLeft.transform.position, Quaternion.identity);

        Debug.Log(left);
        left.GetComponent<RockAreaScript>().direction = -1;
        GameObject right = Instantiate(this.rockArea, rockAreaRight.transform.position, Quaternion.identity);
        right.GetComponent<RockAreaScript>().direction = 1;

        left.transform.parent = rockAreaLeft.transform;
        right.transform.parent = rockAreaRight.transform;
    }

    public override void OnStartServer() {
        Debug.Log("OnStartServer");
        this.BuildArena();
    }
}
